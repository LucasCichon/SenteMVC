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
            var test = worklogs.Where(w => w.Qualification == HourCategory.HD).ToList();
            var test1 = worklogs.Where(w => w.Qualification == HourCategory.SZ).ToList();
            var test2 = worklogs.Where(w => w.Qualification == HourCategory.W).ToList();
            var categories = _configurationService.GetQualificationCategoriesList();
            var result =  worklogs.Where(w => categories.Any(e => e == w.Qualification )).ToList();
            return result;
        }

        public WorklogAnalysisResult AnalyzeWorklogs(IEnumerable<WorklogEntry> worklogs)
        {
            var result = new WorklogAnalysisResult();

            var categories = _configurationService.GetQualificationCategories();

            // Initialize dictionaries
            result.IndividualProductiveHours_DeveloperWork = new Dictionary<string, double>();
            result.IndividualProductiveHours_Compliances = new Dictionary<string, double>();
            result.IndividualSupportiveHours = new Dictionary<string, double>();
            result.IndividualDevelopmentHours = new Dictionary<string, double>();
            result.IndividualNonProductiveHours = new Dictionary<string, double>();

            result.ProductiveHours_DeveloperWork = worklogs.Where(w => categories.Productive.Any(c => c == HourCategory.RP.ToString()) && w.Qualification == HourCategory.RP).Sum(w => w.TimeSpent);
            result.ProductiveHours_Compliances = worklogs.Where(w => categories.Productive.Any(c => c == HourCategory.R.ToString()) && w.Qualification == HourCategory.R).Sum(w => w.TimeSpent);
            result.SupportiveHours = worklogs.Where(w => categories.Supportive.Any(c => c == HourCategory.HD.ToString()) && w.Qualification == HourCategory.HD).Sum(w => w.TimeSpent);
            result.DevelopmentHours = worklogs.Where(w => categories.Development.Any(c => c == HourCategory.SZ.ToString()) && w.Qualification == HourCategory.SZ).Sum(w => w.TimeSpent);
            result.NonProductiveHours = worklogs.Where(w => categories.NonProductive.Any(c => c == HourCategory.W.ToString()) && w.Qualification == HourCategory.W).Sum(w => w.TimeSpent);


            foreach(var author in worklogs.Select(w => w.Author).Distinct())
            {
                result.IndividualProductiveHours_DeveloperWork[author] = worklogs.Where(w => categories.Productive.Any(c => c == HourCategory.RP.ToString()) && w.Qualification == HourCategory.RP && w.Author == author).Sum(w => w.TimeSpent);
                result.IndividualProductiveHours_Compliances[author] = worklogs.Where(w => categories.Productive.Any(c => c == HourCategory.R.ToString()) && w.Qualification == HourCategory.R && w.Author == author).Sum(w => w.TimeSpent);
                result.IndividualSupportiveHours[author] = worklogs.Where(w => categories.Supportive.Any(c => c == HourCategory.HD.ToString()) && w.Qualification == HourCategory.HD && w.Author == author).Sum(w => w.TimeSpent);
                result.IndividualDevelopmentHours[author] = worklogs.Where(w => categories.Development.Any(c => c == HourCategory.SZ.ToString()) && w.Qualification == HourCategory.SZ && w.Author == author).Sum(w => w.TimeSpent);
                result.IndividualNonProductiveHours[author] = worklogs.Where(w => categories.NonProductive.Any(c => c == HourCategory.W.ToString()) && w.Qualification == HourCategory.W && w.Author == author).Sum(w => w.TimeSpent);

            }
            
            //foreach (var worklog in worklogs)
            //{
            //    // Aggregate hours based on qualification
            //    switch (worklog.Qualification)
            //    {
            //        case "RP":
            //            result.ProductiveHours_DeveloperWork += worklog.TimeSpent;
            //            if (!result.IndividualProductiveHours_DeveloperWork.ContainsKey(worklog.Author))
            //                result.IndividualProductiveHours_DeveloperWork[worklog.Author] = 0;
            //            result.IndividualProductiveHours_DeveloperWork[worklog.Author] += worklog.TimeSpent;
            //            break;
            //        case "R":
            //            result.ProductiveHours_Compliances += worklog.TimeSpent;
            //            if (!result.IndividualProductiveHours_Compliances.ContainsKey(worklog.Author))
            //                result.IndividualProductiveHours_Compliances[worklog.Author] = 0;
            //            result.IndividualProductiveHours_Compliances[worklog.Author] += worklog.TimeSpent;
            //            break;
            //        case "HD":
            //            result.SupportiveHours += worklog.TimeSpent;
            //            if (!result.IndividualSupportiveHours.ContainsKey(worklog.Author))
            //                result.IndividualSupportiveHours[worklog.Author] = 0;
            //            result.IndividualSupportiveHours[worklog.Author] += worklog.TimeSpent;
            //            break;
            //        case "SZ":
            //            result.DevelopmentHours += worklog.TimeSpent;
            //            if (!result.IndividualDevelopmentHours.ContainsKey(worklog.Author))
            //                result.IndividualDevelopmentHours[worklog.Author] = 0;
            //            result.IndividualDevelopmentHours[worklog.Author] += worklog.TimeSpent;
            //            break;
            //        case "W":
            //            result.NonProductiveHours += worklog.TimeSpent;
            //            if (!result.IndividualNonProductiveHours.ContainsKey(worklog.Author))
            //                result.IndividualNonProductiveHours[worklog.Author] = 0;
            //            result.IndividualNonProductiveHours[worklog.Author] += worklog.TimeSpent;
            //            break;
            //    }
            //}

            return result;
        }
    }
}
