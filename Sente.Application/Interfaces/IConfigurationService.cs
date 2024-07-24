using Sente.Domain.Common;
using Sente.Domain.Models.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Application.Interfaces
{
    public interface IConfigurationService
    {
        //List<EmployeeConfig> GetEmployees();
        CategoriesConfig GetQualificationCategories();
        List<string> GetQualificationCategoriesList();
    }
}
