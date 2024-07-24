using Sente.Application.Interfaces;
using Sente.Domain.Common;
using Sente.Domain.Interfaces;
using Sente.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Application.Services
{
    public class WorklogService : IWorklogService
    {
        private readonly IWorklogRepository _repository;
        private readonly IConfigurationService _configurationService;

        public WorklogService(IWorklogRepository repository, IConfigurationService configurationService)
        {
            _repository = repository;
            _configurationService = configurationService;
        }

        public async Task<IEnumerable<WorklogEntry>> GetWorklogsAsync(DateTime fromDate, DateTime toDate)
        {
            var worklogs =  await _repository.GetWorklogsAsync(fromDate, toDate);
            var categories = _configurationService.GetQualificationCategoriesList();
            var result =  worklogs.Where(w => categories.Any(e => e == w.Qualification )).ToList();
            return result;
        }

        public WorklogAnalysisResult AnalyzeWorklogs(IEnumerable<WorklogEntry> worklogs)
        {
            var result = new WorklogAnalysisResult();

            var categories = _configurationService.GetQualificationCategories();

            result.AllHoursWithTypes = worklogs.Select(w => new Hours(new Qualification( categories.Categories.FirstOrDefault(c => c.Items.Contains(w.Qualification))?.Name ?? "none" , w.Qualification), w.TimeSpent)).ToList(); 

            foreach (var author in worklogs.Select(w => w.Author).Distinct())
            {
                var authorWorklogs = worklogs.Where(w => w.Author == author);

                var hours = new List<Hours>();

                foreach(var t in result.Types)
                {
                    hours.Add(new Hours(new Qualification(categories.Categories.FirstOrDefault(c => c.Items.Contains(t))?.Name ?? "none", t), authorWorklogs.Where(w => w.Qualification == t).Sum(w => w.TimeSpent)));
                }

                result.AllIndyvidualHoursWithTypes.Add( author, hours );  
            }



            return result;
        }
    }
}
