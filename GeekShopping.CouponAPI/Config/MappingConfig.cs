using AutoMapper;

namespace GeekShopping.CouponAPI.Config
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                //config.CreateMap<ProductViewModel, Product>().ReverseMap();
               
            });
            return mappingConfig;
        }
    }
}
