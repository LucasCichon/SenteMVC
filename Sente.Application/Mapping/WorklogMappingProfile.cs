using AutoMapper;
using Sente.Application.ViewModels;
using Sente.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sente.Application.Mapping
{
    public class WorklogMappingProfile : Profile
    {
        public WorklogMappingProfile()
        {
            CreateMap<WorklogEntry, WorklogEntryViewModel>().ReverseMap();
            CreateMap<WorklogAnalysisResult, WorklogAnalysisResultViewModel>();            // Add more mappings as needed
        }
    }
}
