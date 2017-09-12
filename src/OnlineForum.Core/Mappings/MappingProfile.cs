using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace OnlineForum.Core.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DAL.Entities.Thread, Core.Models.Thread>()
                .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ReverseMap();

            CreateMap<DAL.Entities.ThreadVote, Core.Models.ThreadVote>()
                .ForMember(dest => dest.VoteBy, opt => opt.MapFrom(src => src.VoteBy))
                .ForMember(dest => dest.Thread, opt => opt.MapFrom(src => src.Thread))
                .ReverseMap();

            CreateMap<DAL.Entities.User, Core.Models.User>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash))
                .ReverseMap();

            CreateMap<DAL.Entities.Comment, Core.Models.Comment>()
                .ForMember(dest => dest.Thread, opt => opt.MapFrom(src => src.Thread))
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Parent, opt => opt.MapFrom(src => src.Parent))
                .ReverseMap();
        }
    }
}
