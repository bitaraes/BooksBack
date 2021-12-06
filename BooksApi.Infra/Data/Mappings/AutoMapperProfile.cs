using AutoMapper;
using BooksApi.Domain.Dtos;
using BooksApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApi.Infraestructure.Data.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<BookEntity, BookDto>().ReverseMap();
            CreateMap<UserEntity, UserDto>().ReverseMap();
        }
    }
}
