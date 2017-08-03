using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace OnlineForum.Core.Mappings
{
    public class ThreadServiceProfile : Profile
    {
        public ThreadServiceProfile()
        {
            CreateMap<DAL.Entities.Thread, Core.Models.Thread>().ReverseMap();
        }
    }
}
