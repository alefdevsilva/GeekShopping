using GeekShopping.ProductAPI.Data.DTOs;

namespace GeekShopping.ProductAPI.Model.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDTO>> FindAll();
        Task<ProductDTO> FindById(long id);
        Task<ProductDTO> Create(ProductDTO productDTO);
        Task<ProductDTO> Update(ProductDTO productDTO);
        Task<bool> Delete(long id);
    }
}
