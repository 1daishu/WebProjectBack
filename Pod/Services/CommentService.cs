using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Pod.Data;
using Pod.Dto;
using Pod.IServices;
using Pod.Models;
using Pod.Models.ServiceResponses;

namespace Pod.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        public CommentService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ServiceResponse<bool>> AddResponse(ResponseFormDto formdto)
        {
            var service = new ServiceResponse<bool>();
            try
            {
                var userauth = await db.UserAuths.FirstOrDefaultAsync(x => x.Id == formdto.userId);
                await db.Entry(userauth).Reference(x => x.User).LoadAsync();
                var user = userauth.User;

                Response response = new Response()
                {
                    Message = formdto.comment,
                    Name = user.Name,
                    Email = userauth.Email,
                    User = user
                    
                };
                await db.Responses.AddAsync(response);
                await db.SaveChangesAsync();

                service.Description = "Отзыв добавлен";
                service.StatusCode = Models.Enums.StatusCode.OK;

            }
            catch(Exception ex) 
            {
                service.Description = $"[AddResponse] : {ex.Message}";
                service.StatusCode = Models.Enums.StatusCode.InternalServerError;
            }
            return service;
        }

        public async Task<ServiceResponse<List<ResponseDto>>> GetAllResponse()
        {
           var service = new ServiceResponse<List<ResponseDto>>();
            try
            {
                var responses = await db.Responses.ToListAsync();

                service.Description = "Все отзывы выведены";
                service.Data = mapper.Map<List<ResponseDto>>(responses);
                service.StatusCode = Models.Enums.StatusCode.OK;
                

            }
            catch (Exception ex)
            {
                service.Description = $"[GetAllResponse] : {ex.Message}";
                service.StatusCode = Models.Enums.StatusCode.InternalServerError;
            }
            return service;
        }
    }
}
