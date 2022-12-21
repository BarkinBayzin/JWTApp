using AutoMapper;
using JWTAppBackOffice.Core.Application.DTOs;
using JWTAppBackOffice.Core.Domain;

namespace JWTAppBackOffice.Core.Application.Mappings
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryListDto>().ReverseMap();
        }
    }
}
