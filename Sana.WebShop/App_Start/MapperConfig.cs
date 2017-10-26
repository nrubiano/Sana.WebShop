using AutoMapper;
using Sana.WebShop.Dto;
using Sana.WebShop.Models;

namespace Sana.WebShop.App_Start
{
    public static class MapperConfig
    {
        public static IMapper Build()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Product, ProductDto>();
                cfg.CreateMap<ProductDto, Product>();
            });

            return config.CreateMapper();
        }
    }
}