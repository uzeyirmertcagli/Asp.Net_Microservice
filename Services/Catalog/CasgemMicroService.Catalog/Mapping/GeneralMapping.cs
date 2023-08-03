using AutoMapper;
using CasgemMicroService.Catalog.DTOS.CategoryDtos;
using CasgemMicroService.Catalog.DTOS.ProductDtos;
using CasgemMicroService.Catalog.Models;

namespace CasgemMicroService.Catalog.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category , ResultCategoryDto>().ReverseMap();
            CreateMap<Category , CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();

            CreateMap<Product , ResultProductDto>().ReverseMap();  
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
