using AutoMapper;
using Covid.Business.Dto;
using Covid.Data.Entities;

namespace Covid.Business.MappingProfiles
{
    public class EntityToDtoProfiles : Profile
    {
        public EntityToDtoProfiles()
        {
            CreateMap<ImportStatistics, ImportResponse>()
                .ForSourceMember(src=>src.Id, opt=>opt.DoNotValidate());

            CreateMap<QuestionaireStatistics, QuestionResponse>()
                .ForSourceMember(src=>src.Type, opt=>opt.DoNotValidate())
                .ForSourceMember(src=>src.IntValue, opt=>opt.DoNotValidate())
                .ForMember(dst => dst.Question, opt => opt.MapFrom(src => src.TextValue))
                .ForMember(dst => dst.Count, opt => opt.MapFrom(src=>src.Count));

            CreateMap<QuestionaireStatistics, CategoryResponse>()
                .ForSourceMember(src=>src.Type, opt=>opt.DoNotValidate())
                .ForSourceMember(src=>src.TextValue, opt=>opt.DoNotValidate())
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.IntValue))
                .ForMember(dst => dst.Count, opt => opt.MapFrom(src=>src.Count));

            CreateMap<ImportStatistics, ImportStatisticsResponse>()
                .ForSourceMember(src=>src.Id, opt=>opt.DoNotValidate())
                .ForMember(dst => dst.StartDateTime, opt => opt.MapFrom(src => src.StartDateTime))
                .ForMember(dst => dst.EndDateTime, opt => opt.MapFrom(src=>src.EndDateTime))
                .ForMember(dst => dst.ImportedFiles, opt => opt.MapFrom(src=>src.ImportedFiles))
                .ForMember(dst => dst.LinesImported, opt => opt.MapFrom(src=>src.LinesImported))
                .ForMember(dst => dst.TotalFiles, opt => opt.MapFrom(src=>src.TotalFiles));
        }
    }
}
