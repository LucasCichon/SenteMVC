
using Sente.Domain.Common;

namespace Sente.Domain.Models
{
    public class WorklogEntry
    {
        public int Ref { get; set; }
        public string Author { get; set; }
        public string Project { get; set; }
        public string Issue { get; set; }
        public string IssueSummary { get; set; }
        public HourCategory Qualification { get; set; }
        public double TimeSpent { get; set; }
        public DateTime WorklogDate { get; set; }
        public DateTime WorklogStartTimestamp { get; set; }
        public DateTime RegTimestamp { get; set; }
        public string Descrip { get; set; }
        public string Components { get; set; }
    }
}
