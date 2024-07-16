using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Application.ViewModels
{
    public class WorklogEntryViewModel
    {
        public int Ref { get; set; }
        public string Author { get; set; }
        public string Project { get; set; }
        public string Issue { get; set; }
        public string IssueSummary { get; set; }
        public string Qualification { get; set; }
        public double TimeSpent { get; set; }
        public DateTime WorklogDate { get; set; }
        public DateTime WorklogStartTimestamp { get; set; }
        public DateTime RegTimestamp { get; set; }
        public string Descrip { get; set; }
        public string Components { get; set; }
    }
}
