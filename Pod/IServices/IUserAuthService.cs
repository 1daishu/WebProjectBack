using Pod.Dto;
using Pod.Models.ServiceResponses;

namespace Pod.IServices
{
    public interface IUserAuthService
    {
        Task<ServiceResponse<bool>> Register(RegisterFormDto regform);

        Task<ServiceResponse<int>> Login(string email, string password);
    }
}
