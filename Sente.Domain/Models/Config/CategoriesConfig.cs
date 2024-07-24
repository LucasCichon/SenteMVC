using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Domain.Models.Config
{
    public class CategoriesConfig
    {
        public List<CategoryConfig> Categories { get; set; }
    }
    public class CategoryConfig
    {
        public string Name { get; set; }
        public List<string> Items { get; set; }
    }
}
