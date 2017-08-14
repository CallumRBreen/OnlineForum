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
            CreateMap<DAL.Entities.Thread, Core.Models.Thread>().ReverseMap();

            CreateMap<DAL.Entities.User, Core.Models.User>().ForMember(
                dest => dest.Password,
                opt => opt.MapFrom(src => src.PasswordHash)
            ).ReverseMap();
        }
    }
}
