using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthDeptApp.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // IF THERE IS DATA IN THE DB THEN SKIP THE SEEDING
                if (context.Establishments.Any()) {
                    return;
                }

                // POPULATE HEALTH CODES
                var healthCodes = HealthCodeGenerator.GetList().Select(x => new HealthCode {
                    Position = x.Position,
                    Regulation =  x.Regulation,
                    Category = x.Category,
                    CategoryPosition = x.CategoryId
                }).ToArray();
                context.HealthCodes.AddRange(healthCodes);

                var faker = new Faker();
                var earliestDate = new DateTime(1990, 1, 1);

                // POPULATE ESTABLISHMENT CATEGORIES
                var sourceData = faker.Random.ArrayElements(EstablistmentGenerator.GetArray(), 500);
                var categories = new List<EstablishmentCategory>();

                foreach (var item in sourceData.Select(x => x.Category).Distinct())
                {
                    var ec = new EstablishmentCategory { Name = item, InspectionFrequency = 90 };
                    
                    if (ec.Name == "Food Processing") ec.InspectionFrequency = 30;
                    if (ec.Name == "Special Event") { ec.InspectionFrequency = 365; ec.CanHaveLiquorLicense = true; }
                    if (ec.Name == "Restaurant") { ec.InspectionFrequency = 60; ec.CanHaveLiquorLicense = true; }
                    if (ec.Name == "Bar") { ec.CanHaveLiquorLicense = true; }
                    if (ec.Name == "Education") ec.InspectionFrequency = 60;

                    categories.Add(ec);
                    context.EstablishmentCategories.Add(ec);
                }
                
                // POPULATE AGENTS
                var agentPool = new List<Agent>();
                for (int i = 0; i < 50; i++)
                {
                    var hireDate = faker.Date.Between(earliestDate, DateTime.Now.AddDays(-60));
                    var onLongTermLeave = faker.Random.Bool(.1f);
                    var pic = faker.Image.Business();
                    var newAgent = new Agent { 
                        FirstName = faker.Name.FirstName(),
                        LastName = faker.Name.LastName(),
                        SeparationDate = onLongTermLeave || faker.Random.Bool(.75f) ? (DateTime?)null : faker.Date.Between(hireDate.AddDays(30), DateTime.Now.AddDays(-30)),
                        OnLongTermLeave = onLongTermLeave,
                        HireDate = hireDate
                    };
                    agentPool.Add(newAgent);
                    context.Agents.Add(newAgent);
                }

                // ENSURE A FEW AGENTS EXISTED AROUND START DATE
                var oldestAgents = agentPool.OrderByDescending(x => x.HireDate).Take(3).ToList();
                oldestAgents.ForEach(x => x.HireDate = earliestDate.AddDays(-30));

                // ENSURE A FEW AGENTS ARE STILL EMPLOYEED
                if (!agentPool.Any(x => x.OnLongTermLeave == false && x.SeparationDate == null)) {
                    var agentsToRetain = faker.Random.ArrayElements(agentPool.ToArray(), 3).ToList();
                    agentsToRetain.ForEach(x => {x.SeparationDate = null; x.OnLongTermLeave = false; });
                }

                // POPULATE ESTABLISHMENTS
                var establishments = new List<Establishment>();                
                foreach (var item in sourceData)
                {
                    var openDate = faker.Date.Between(earliestDate, DateTime.Now.AddMonths(-3));
                    var category = categories.FirstOrDefault(x => x.Name == item.Category) ?? categories[0];
                    var newEstablishment = new Establishment {
                        Name = item.Name,
                        EmployeeCount = faker.Random.Int(1, 100),
                        OpenDate = openDate,
                        Category = category,
                        HasLiquorLicense = category.CanHaveLiquorLicense && faker.Random.Bool(0.9f),
                        Address = item.Address,
                        City = "Chicago",
                        State = "IL",
                        Zip = item.Zip,
                        Latitute = item.Lat,
                        Longitude = item.Longitude,
                        ClosedDate = faker.Random.Bool(.5f) ? (DateTime?)null : faker.Date.Between(openDate.AddDays(20), DateTime.Now.AddDays(-7))
                    };
                    establishments.Add(newEstablishment);
                    context.Establishments.Add(newEstablishment);
                }

                // POPULATE INSPECTIONS
                foreach (var est in establishments)
                {
                    var inspectionDate = faker.Date.Between(est.OpenDate.AddDays(-14), est.OpenDate.AddDays(-1));
                    var endDate = est.ClosedDate ?? DateTime.Today.AddDays(-1);

                    while(inspectionDate <= endDate) {
                        var availableAgents = (from a in agentPool
                                              where a.HireDate <= inspectionDate
                                              where a.SeparationDate == null || a.SeparationDate.Value >= inspectionDate
                                              select a).ToArray();

                        if (availableAgents.Length == 0) {
                            System.Console.WriteLine("HERE");
                        }

                        Agent agent = faker.Random.ArrayElement(availableAgents);

                        int score = faker.Random.Int(12,100);
                        List<HealthCode> voilations = new List<HealthCode>();

                        switch(score) {
                            case var _ when score < 20:
                                voilations.AddRange(faker.Random.ArrayElements(healthCodes, faker.Random.Int(15,25)));
                                break;
                            case var _ when score < 40:
                                voilations.AddRange(faker.Random.ArrayElements(healthCodes, faker.Random.Int(10,20)));
                                break;
                            case var _ when score < 60:
                                voilations.AddRange(faker.Random.ArrayElements(healthCodes, faker.Random.Int(5,15)));
                                break;
                            case var _ when score < 80:
                                voilations.AddRange(faker.Random.ArrayElements(healthCodes, faker.Random.Int(1,5)));
                                break;
                            default:
                                break;                            
                        }

                        var inspection = new Inspection {
                            Agent = agent,
                            Establishment = est,
                            DateInspected = inspectionDate,
                            DateAssigned = faker.Date.Between(inspectionDate.AddDays(-21), inspectionDate.AddDays(-7)),
                            DateCompleted =faker.Date.Between(inspectionDate.AddDays(2), inspectionDate.AddDays(7)),
                            Score = score,
                            Report = string.Join(Environment.NewLine, voilations.Select(x => $"{x.Position}. {x.Regulation} "))
                        };

                        context.Inspections.Add(inspection);

                        var earliestNextDate = inspectionDate.AddDays(est.Category.InspectionFrequency);
                        inspectionDate = faker.Date.Between(earliestNextDate, earliestNextDate.AddDays(14));
                    }
                }

                context.SaveChanges();
            }
        }
    }
}