using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Domain.Models
{
    public class WorklogAnalysisResult
    {
        public string Author { get; set; }
        public double ProductiveHours_DeveloperWork { get; set; }
        public double ProductiveHours_Compliances{ get; set; }
        public double SupportiveHours { get; set; }
        public double DevelopmentHours { get; set; }
        public double NonProductiveHours { get; set; }

        public double RP_Hours { get; set; }
        public double R_Hours { get; set; }
        public double HD_Hours { get; set; }
        public double SZ_Hours { get; set; }
        public double W_Hours { get; set; }

        // Add dictionaries for detailed breakdown
        public Dictionary<string, double> IndividualProductiveHours_DeveloperWork { get; set; } = new();
        public Dictionary<string, double> IndividualProductiveHours_Compliances { get; set; } = new();
        public Dictionary<string, double> IndividualSupportiveHours { get; set; } = new();
        public Dictionary<string, double> IndividualDevelopmentHours { get; set; } = new();
        public Dictionary<string, double> IndividualNonProductiveHours { get; set; } = new();
    }
}
