using Pod.Dto;
using Pod.Models.ServiceResponses;

namespace Pod.IServices
{
    public interface IProductService
    {
        Task<ServiceResponse<List<ProductDto>>> GetAllProduct();

        Task<ServiceResponse<bool>> AddProductToCart(int userId, int productId);

        Task<ServiceResponse<bool>> DeleteProductToCart(int userId, int productId);

        Task AddProduct(ProductFormDto productdto);

        Task<ServiceResponseSum<List<ProductDto>>> GetProductByCart(int userId);


    }
}
