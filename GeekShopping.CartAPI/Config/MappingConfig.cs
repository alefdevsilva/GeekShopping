using AutoMapper;
using GeekShopping.CartAPI.Model;
using GeekShopping.Data.ViewModels;
using GeekShopping.ProductCart.Model;

namespace GeekShopping.CartAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<ProductViewModel, Product>().ReverseMap();
                config.CreateMap<CartHeaderViewModel, CartHeader>().ReverseMap();
                config.CreateMap<CartDetailViewModel, CartDetail>().ReverseMap();
                config.CreateMap<CartViewModel, Cart>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
