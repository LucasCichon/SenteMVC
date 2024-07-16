using AutoMapper;
using Sente.Application.Interfaces;
using Sente.Application.ViewModels;
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
            var employees = _configurationService.GetEmployees();
            var worklogs =  await _repository.GetWorklogsAsync(fromDate, toDate);
            return worklogs.Where(w => employees.Any(e => e.Name == w.Author && e.Qualifications.Contains(w.Qualification))); 
                //&& employees.Any(e =>  e.Qualifications.Contains(w.Qualification)));
        }

        public WorklogAnalysisResult AnalyzeWorklogs(IEnumerable<WorklogEntry> worklogs)
        {
            var result = new WorklogAnalysisResult();

            // Initialize dictionaries
            result.IndividualProductiveHours = new Dictionary<string, double>();
            result.IndividualSupportiveHours = new Dictionary<string, double>();
            result.IndividualDevelopmentHours = new Dictionary<string, double>();
            result.IndividualNonProductiveHours = new Dictionary<string, double>();

            foreach (var worklog in worklogs)
            {
                // Aggregate hours based on qualification
                switch (worklog.Qualification)
                {
                    case "RP":
                    case "R":
                        result.ProductiveHours += worklog.TimeSpent;
                        if (!result.IndividualProductiveHours.ContainsKey(worklog.Author))
                            result.IndividualProductiveHours[worklog.Author] = 0;
                        result.IndividualProductiveHours[worklog.Author] += worklog.TimeSpent;
                        break;
                    case "HD":
                        result.SupportiveHours += worklog.TimeSpent;
                        if (!result.IndividualSupportiveHours.ContainsKey(worklog.Author))
                            result.IndividualSupportiveHours[worklog.Author] = 0;
                        result.IndividualSupportiveHours[worklog.Author] += worklog.TimeSpent;
                        break;
                    case "SZ":
                        result.DevelopmentHours += worklog.TimeSpent;
                        if (!result.IndividualDevelopmentHours.ContainsKey(worklog.Author))
                            result.IndividualDevelopmentHours[worklog.Author] = 0;
                        result.IndividualDevelopmentHours[worklog.Author] += worklog.TimeSpent;
                        break;
                    case "W":
                        result.NonProductiveHours += worklog.TimeSpent;
                        if (!result.IndividualNonProductiveHours.ContainsKey(worklog.Author))
                            result.IndividualNonProductiveHours[worklog.Author] = 0;
                        result.IndividualNonProductiveHours[worklog.Author] += worklog.TimeSpent;
                        break;
                }
            }

            return result;
        }
    }
}
