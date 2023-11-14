using Microsoft.EntityFrameworkCore;
using Pod.Data;
using Pod.Dto;
using Pod.IServices;
using Pod.Models;
using Pod.Models.ServiceResponses;

namespace Pod.Services
{
    public class UserAuthService : IUserAuthService
    {
        private readonly ApplicationDbContext db;
        public UserAuthService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<ServiceResponse<int>> Login(string email, string password)
        {
            var service = new ServiceResponse<int>();
            try
            {
                var userauth = await db.UserAuths.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
                await db.Entry(userauth).Reference(x => x.User).LoadAsync();
                if(userauth == null)
                {
                    service.Description = "Неверное введенные данные";
                    service.StatusCode = Models.Enums.StatusCode.BadRequest;
                }
                else
                {
                    service.Data = userauth.User.Id;
                    service.Description = "Вы вошли";
                    service.StatusCode = Models.Enums.StatusCode.OK;
                }

            }
            catch(Exception ex)
            {
                service.Description = $"[Login] : {ex.Message}";
                service.StatusCode = Models.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async  Task<ServiceResponse<bool>> Register(RegisterFormDto regform)
        {
            var service = new ServiceResponse<bool>();
            try
            {
                var newUserAuth = new UserAuth()
                {
                    Email = regform.Email,
                    Password = regform.Password,
                    User = new User()
                    {
                        Name = regform.Name,
                        Cart = new Cart()
                        {
                            CartProducts = new List<CartProduct>()
                        }
                        
                    }

                };

               await db.UserAuths.AddAsync(newUserAuth);
                await db.SaveChangesAsync();

                service.Description = "Пользователь зарегистрировался";
                service.StatusCode = Models.Enums.StatusCode.OK;

            }
            catch(Exception ex)
            {
                service.Description = $"[Register] : {ex.Message}";
                service.StatusCode = Models.Enums.StatusCode.InternalServerError;
            }

            return service;
        }
    }
}
