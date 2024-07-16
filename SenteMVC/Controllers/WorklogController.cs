using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sente.Application.Interfaces;
using Sente.Application.Services;
using Sente.Application.ViewModels;

namespace SenteMVC.Controllers
{
    public class WorklogController : Controller
    {
        private readonly IWorklogService _worklogService;
        private readonly IMapper _mapper;

        public WorklogController(IWorklogService service, IMapper mapper)
        {
            _worklogService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            if (!fromDate.HasValue || !toDate.HasValue)
            {
                fromDate = DateTime.Now.AddMonths(-1); // Default to last month
                toDate = DateTime.Now;
            }

            var worklogs = await _worklogService.GetWorklogsAsync(fromDate.Value, toDate.Value);
            var analysisResult = _worklogService.AnalyzeWorklogs(worklogs);
            var viewModel = _mapper.Map<WorklogAnalysisResultViewModel>(analysisResult);

            ViewData["FromDate"] = fromDate.Value.ToString("yyyy-MM-dd");
            ViewData["ToDate"] = toDate.Value.ToString("yyyy-MM-dd");

            return View("Index", viewModel);
        }
    }
}
