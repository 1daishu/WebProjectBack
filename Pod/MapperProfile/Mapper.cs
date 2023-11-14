using AutoMapper;
using Pod.Controllers;
using Pod.Dto;
using Pod.Models;

namespace Pod.MapperProfile
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Response, ResponseDto>();
            
        }
    }
}
