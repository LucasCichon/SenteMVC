using Sente.Application.ViewModels;
using Sente.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Application.Interfaces
{
    public interface IWorklogService
    {
        WorklogAnalysisResult AnalyzeWorklogs(IEnumerable<WorklogEntry> worklogs);
        Task<IEnumerable<WorklogEntry>> GetWorklogsAsync(DateTime fromDate, DateTime toDate);
    }
}
