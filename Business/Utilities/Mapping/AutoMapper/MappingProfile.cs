using AutoMapper;
using Core.Managers.Auth.Commands.Register;
using Core.Entities.Concrete;
using Core.Utilities.Hashing;
using Core.Utilities.Security.Abstract;
using Core.Utilities.Security.Jwt;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Mapping.AutoMapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            CreateMap<RegisterDto, User>();
            CreateMap<User, LoginInfoDto>();
            CreateMap<User, UserDto>();
            CreateMap<ProductDto,Product>().ReverseMap();
            CreateMap<InsertProductDto,Product>();

        }
    }
}
