using AutoMapper;
using Sente.Application.ViewModels;
using Sente.Domain.Models;

namespace Sente.Application.Mapping
{
    public class WorklogMappingProfile : Profile
    {
        public WorklogMappingProfile()
        {
            CreateMap<WorklogEntry, WorklogEntryViewModel>().ReverseMap();
            CreateMap<WorklogAnalysisResult, WorklogAnalysisResultViewModel>();
            CreateMap<Sente.Domain.Models.Qualification, Sente.Application.ViewModels.Qualification>();// Add more mappings as needed
            CreateMap<Sente.Domain.Models.Hours, Sente.Application.ViewModels.Hours>();// Add more mappings as needed
        }
    }
}
