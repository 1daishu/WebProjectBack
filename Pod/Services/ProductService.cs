using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pod.Data;
using Pod.Dto;
using Pod.IServices;
using Pod.Models;
using Pod.Models.ServiceResponses;

namespace Pod.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
       public ProductService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;   
            this.mapper = mapper;
        }

        public async Task AddProduct(ProductFormDto productdto)
        {
            var product = new Product()
            {
                Name = productdto.Name,
                Description = productdto.Description,
                ShortDescription = productdto.ShortDescription,
                Type = productdto.Type,
                Price = productdto.Price,
                ImageUrl1 = productdto.ImageUrl1,
                ImageUrl2 = productdto.ImageUrl2,
                ImageUrl3 = productdto.ImageUrl3
            };

            await db.Products.AddAsync(product);
            await db.SaveChangesAsync();
            
        }

        public async Task<ServiceResponse<bool>> AddProductToCart(int userId, int productId)
        {
            var service = new ServiceResponse<bool>();
            try
            {
                var userauth = await db.UserAuths.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == userId);
                var user = userauth.User;
               await  db.Entry(user).Reference(x => x.Cart).LoadAsync();
                var product = await db.Products.FirstOrDefaultAsync(x => x.Id == productId);

                var cartproduct = new CartProduct()
                {
                    Cart = user.Cart,
                    Product = product

                };

               await db.CartProducts.AddAsync(cartproduct);
                await db.SaveChangesAsync();

                service.Description = "Продукт добавлен в корзину";
                service.StatusCode = Models.Enums.StatusCode.OK;

            }
            catch(Exception ex)
            {
                service.Description = $"[AddPRoductToCart] : {ex.Message}";
                service.StatusCode = Models.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<bool>> DeleteProductToCart(int userId, int productId)
        {
            var service = new ServiceResponse<bool>();
            try
            {
                var userauth = await db.UserAuths.Include(x => x.User).FirstOrDefaultAsync(x => x.Id == userId);
                var user = userauth.User;
                await db.Entry(user).Reference(x => x.Cart).LoadAsync();

               var productcart = await  db.CartProducts.FirstOrDefaultAsync(x=>x.Cart.Id == user.Cart.Id && x.ProductId == productId);

                db.CartProducts.Remove(productcart);
               await db.SaveChangesAsync();




                service.Description = "Товар удален из вашей корзины";
                service.StatusCode = Models.Enums.StatusCode.OK;
               

            }
            catch (Exception ex)
            {
                service.Description = $"[AddPRoductToCart] : {ex.Message}";
                service.StatusCode = Models.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<List<ProductDto>>> GetAllProduct()
        {
           var service = new ServiceResponse<List<ProductDto>>();
            try
            {
                var products = await db.Products.ToListAsync();

                var totalsum = products.Select(x => x.Price).Sum();

                service.Data = mapper.Map<List<ProductDto>>(products);
                service.Description = "Все продукты выведены";
                service.StatusCode = Models.Enums.StatusCode.OK;
                //service.TotalSum = totalsum;

            }
            catch (Exception ex)
            {
                service.Description = $"[GetAllProduct] : {ex.Message}";
                service.StatusCode = Models.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponseSum<List<ProductDto>>> GetProductByCart(int userId)
        {
            var service = new ServiceResponseSum<List<ProductDto>>();
            try
            {
                var cart = db.Carts.FirstOrDefault(x => x.UserId == userId);

                var productcart = await db.CartProducts.Where(x => x.Cart == cart).Include(x=>x.Product).ToListAsync();

                

                var products = productcart.Select(x => x.Product).ToList();


                var totalsum = products.Select(x => x.Price).Sum();



                if (products.Count == 0)
                {
                    service.Description = "У вас нет товаров в корзине";
                    service.StatusCode = Models.Enums.StatusCode.NotFound;
                }
                else
                {


                    service.Data = mapper.Map<List<ProductDto>>(products);
                    service.Description = "Ваши товары";
                    service.StatusCode = Models.Enums.StatusCode.OK;
                    service.TotalSum = totalsum;
                }
               



            }
            catch(Exception ex)
            {
                service.Description = $"[GetProductByCart] : {ex.Message}";
                service.StatusCode = Models.Enums.StatusCode.InternalServerError;
            }
            return service;
        }
    }
}
