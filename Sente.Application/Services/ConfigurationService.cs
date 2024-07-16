using Microsoft.Extensions.Configuration;
using Sente.Application.Interfaces;
using Sente.Domain.Models.Config;

namespace Sente.Application.Services
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<EmployeeConfig> GetEmployees()
        {
            //Get the Employees section from appsettings.json
            var employeesSection = _configuration.GetSection("Employees");

            // Deserialize the Employees section into a List<EmployeeConfig>
            var employees = new List<EmployeeConfig>();
            employeesSection.Bind(employees);

            return employees;
        }

        public QualificationCategoriesConfig GetQualificationCategories()
        {
            // Get the QualificationCategories section from appsettings.json
        var qualificationCategoriesSection = _configuration.GetSection("QualificationCategories");

        // Deserialize the QualificationCategories section into QualificationCategoriesConfig
        var qualificationCategories = new QualificationCategoriesConfig();
        qualificationCategoriesSection.Bind(qualificationCategories);

        return qualificationCategories;
        }
    }
}
