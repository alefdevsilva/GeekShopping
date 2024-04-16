using GeekShopping.Web.Models;

namespace GeekShopping.Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> FindAllProducts();
        Task<ProductViewModel> FindProductById(long id);
        Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel);
        Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel);
        Task<bool> DeleteProductById(long id);
    }
}
