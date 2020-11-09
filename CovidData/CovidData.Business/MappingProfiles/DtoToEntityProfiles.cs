using System;
using AutoMapper;
using Covid.Business.Dto;

namespace Covid.Business.MappingProfiles
{
    public class DtoToEntityProfiles : Profile
    {
        public DtoToEntityProfiles()
        {

            CreateMap<CovidData, Data.Entities.QuestionaireData>()
                .ForMember(dst => dst.Question, opt => opt.MapFrom(src => src.Question))
                .ForMember(dst => dst.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
                .ForMember(dst => dst.Datetime, opt => opt.MapFrom(src => src.Datetime))
                .ForMember(dst => dst.Answer, opt => opt.MapFrom(src => src.Answer))
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
                .ForMember(dst => dst.ImportDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dst => dst.Id, opt => opt.Ignore());
        }
    }
}
