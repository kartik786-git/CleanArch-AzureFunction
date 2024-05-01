using Application.Features;
using AutoMapper;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // required mapping here

            CreateMap<Blog, BlogDto>().
                 //.ForMember(des => des.Title, opt => opt.MapFrom(src => src.Name))
                 ReverseMap();
            CreateMap<Author,AuthorDto>().ReverseMap();
        }
    }
}
