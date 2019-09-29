
using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace HealthDeptApp.Models
{
    public class Inspection
    {
        [Key]
        public int Id { get; set; }
        public int Score { get; set; }
        public DateTime DateAssigned { get; set; }
        public DateTime? DateInspected { get; set; }
        public DateTime? DateCompleted { get; set; }
        public int AgentId { get; set; }
        public Agent Agent { get; set; }
        public int EstablishmentId { get; set; }
        public Establishment Establishment { get; set; }
        public string Report { get; set; }


        [NotMapped]
        public bool IsCompleted
        {
            // TRUE/FALSE: TRUE IF THE COMPLETED DATE IS POPULATED
            get { return DateCompleted != null; }
        }

        [NotMapped]
        public string Grade
        {
            get
            {
                // IF INSPECTION NOT COMPLETED THEN
                // RETURN NG - "NO GRADE"
                if (IsCompleted == false)
                {
                    return "NG";
                }
                // RETURN A IF SCORE GREATER THAN 85
                if (Score > 85)
                {
                    return "A";
                }
                // RETURN B IF SCORE GREATER THAN 75
                if (Score > 75)
                {
                    return "B";
                }
                // RETURN C IF SCORE GREATER THAN 50
                if (Score > 50)
                {
                    return "C";
                }
                // OTHERWISE RETURN F 
                return "F";
            }
        }
        [NotMapped]
        public DateTime EffectiveDate
        {
            // THE ?? SYMBOL IS CALLED THE NULL COALESCING OPERATOR
            // IT EVALUATES EACH PROPERTY AND RETURNS THE FIRST NON-NULL VALUE
            // E.G. IF DATECOMPLETED IS NULL THEN IT WILL USE THE DATEINSPECTED
            // VALUE. IF THAT IS NULL THEN IT FALLS BACK TO THE DATEASSIGNED 
            // VALUE.
            get { return DateCompleted ?? DateInspected ?? DateAssigned; }
        }
        [NotMapped]
        public string GradeUiHelper
        {
            get
            {
                switch (Grade)
                {
                    case "A":
                        return "success";
                    case "B":
                        return "info";
                    case "C":
                        return "warning";
                    case "F":
                        return "danger";
                    default:
                        return "default";
                }
            }
        }
        [NotMapped]
        public string ReportUiHelper
        {
            get
            {
                // THIS FUNCTION ATTEMPTS TO CLEAN UP THE REPORT DATA
                // IT CONVERTS IT TO TITLE CASE AND INSERTS BREAKS
                TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
                var output = textInfo.ToTitleCase(Report.ToLowerInvariant());
                return output.Replace(Environment.NewLine, "<br />");
            }
        }
        [NotMapped]
        public bool HasReport
        {
            get
            {
                // TRUE/FALSE: TRUE IF THERE IS ANY REPORT TEXT CONTENT
                if (string.IsNullOrWhiteSpace(Report))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
            