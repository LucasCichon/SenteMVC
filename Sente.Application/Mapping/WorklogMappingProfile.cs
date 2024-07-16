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
            CreateMap<WorklogAnalysisResult, WorklogAnalysisResultViewModel>()
                            .ForMember(dest => dest.RP_Hours, opt => opt.MapFrom(src => src.RP_Hours))
                            .ForMember(dest => dest.R_Hours, opt => opt.MapFrom(src => src.R_Hours))
                            .ForMember(dest => dest.HD_Hours, opt => opt.MapFrom(src => src.HD_Hours))
                            .ForMember(dest => dest.SZ_Hours, opt => opt.MapFrom(src => src.SZ_Hours))
                            .ForMember(dest => dest.W_Hours, opt => opt.MapFrom(src => src.W_Hours));            // Add more mappings as needed
        }
    }
}
