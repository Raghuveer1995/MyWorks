
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthDeptApp.Models
{
    public class Establishment
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool HasLiquorLicense { get; set; }
        public int EmployeeCount { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public double Latitute { get; set; }
        public double Longitude { get; set; }

        // RELATIONSHIPS
        public ICollection<Inspection> Inspections { get; set; }
        public int CategoryId { get; set; }
        public EstablishmentCategory Category { get; set; }

        // READONLY PROPERTIES
        [NotMapped]
        public bool IsOpen
        {
            //RETURN TRUE/FALSE DPENDING ON IF THE ESTABLISHMENT HAS A CLOSE DATE
            get { return ClosedDate == null; }
        }

        [NotMapped]
        public Inspection FirstInspection
        {
            // SORT THE INSPECTIONS BY DATE ASSIGNED AND RETURN THE FIRST ONE
            get
            {
                return Inspections?.OrderBy(x => x.DateAssigned).FirstOrDefault();
            }
        }

        [NotMapped]
        public Inspection LastCompletedInspection
        {
            get
            {
                // FILTER FOR ONLY INSPECTIONS THAT HAVE A COMPLETED DATE
                // SORT THE BY COMPLETED DATE DESCENDING
                // TAKE THE FIRST RECORD (WHICH WILL BE THE MOST RECENT)
                return Inspections?.Where(x => x.DateCompleted != null)
                                  .OrderByDescending(x => x.DateCompleted)
                                  .FirstOrDefault();
            }
        }

        [NotMapped]
        public Inspection LastInspectionAssigned
        {
            get
            {
                // GET THE MOST RECENTLY ASSIGNED INSPECTION
                return Inspections?.OrderByDescending(x => x.DateAssigned)
                                  .FirstOrDefault();
            }
        }

        [NotMapped]
        public bool IsInspectionUnderWay
        {
            get
            {
                // TRUE/FALSE: IS THE LATEST INSPECTION STILL IN PROGRESS?
                return LastInspectionAssigned?.DateCompleted == null;
            }
        }

        [NotMapped]
        public string Grade
        {
            get
            {
                // GET THE LAST COMPLETED INSPECTION
                Inspection lastInspection = LastCompletedInspection;

                // ALWAYS CHECK FOR VALID DATA FIRST IF THERE
                // IS NO COMPLETED INSPECTION THEN RETURN "NG"
                // FOR "NO GRADE"
                if (lastInspection == null)
                {
                    return "NG";
                }

                // OTHERWISE, RETREIEVE THE GRADE FROM THE INSPECITON
                return lastInspection.Grade;
            }
        }

        [NotMapped]
        public string GradeUiHelper
        {
            get
            {
                // THIS IS A UI HELPER METHOD TO HELP COLOR THE GRADES
                // AND WORKS WITH THE BOOTSTRAP 'SUCCESS','WARNING','INFO,
                // ETC, COLOR SCHEME
                Inspection lastInspection = LastCompletedInspection;
                // ALWAYS CHECK FOR VALID DATA FIRST
                if (lastInspection == null)
                {
                    return "default";
                }

                return lastInspection.GradeUiHelper;
            }
        }

        [NotMapped]
        public string NextInspection
        {
            get
            {
                // CALCULATE THE NEXT INSPECTION

                // IF THE ESTABLISHMENT IS CLOSED THERE
                // IS NO FUTURE INSPECTIONS
                if (IsOpen == false || Category == null)
                {
                    return "N/A";
                }

                // START THE CALCULATION FROM THE OPEN DATE OF THE ESTABLISHMENT
                DateTime startDate = OpenDate;

                // GRAB THE LAST COMPLETED INSPECTION AND IF IT EXISTS
                // THEn UPDATE THE START DATE TO THE INSPECTION DATE
                if (LastCompletedInspection != null)
                {
                    startDate = LastCompletedInspection.DateAssigned;
                }

                // GET THE INSPECTION FREQUENCY FROM THE CATEGORY RELATIONSHIP
                int inspectionFrequencyInDays = Category.InspectionFrequency;

                // CALCULATE THE NEXT INSPECITON BY ADDING THE FREQ. DAYS TO THE STARTDATE
                DateTime nextDate = startDate.AddDays(inspectionFrequencyInDays);

                // RETURN THE DATE IN THE FORMAT PROVIDED (Month name date, year)
                return nextDate.ToString("MMMM dd, yyyy");
            }
        }

        [NotMapped]
        public IEnumerable<Inspection> RecentInspections
        {
            get
            {
                // SORT THE INSPECTIONS BY DESCENDING DATE
                // AND TAKE THE FIRST 10 RECORDS (I.E. THE MOST RECENT)
                return Inspections?.OrderByDescending(x => x.DateAssigned)
                                  .Take(10)
                                  .ToList() ?? new List<Inspection>();
            }
        }
    }
}       