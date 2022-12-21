using AutoMapper;
using JWTAppBackOffice.Core.Application.DTOs;
using JWTAppBackOffice.Core.Domain;

namespace JWTAppBackOffice.Core.Application.Mappings
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductListDto>().ReverseMap();
        }
    }
}
