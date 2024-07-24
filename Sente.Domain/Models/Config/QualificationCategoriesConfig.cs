using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Domain.Models.Config
{
    public class QualificationCategoriesConfig
    {
        public List<string> Productive { get; set; } = new();
        public List<string> Supportive { get; set; } = new();
        public List<string> Development { get; set; } = new();
        public List<string> NonProductive { get; set; } = new();
    }
}
