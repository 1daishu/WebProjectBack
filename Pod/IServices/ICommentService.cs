using Pod.Dto;
using Pod.Models;
using Pod.Models.ServiceResponses;

namespace Pod.IServices
{
    public interface ICommentService
    {
        Task<ServiceResponse<List<ResponseDto>>> GetAllResponse();

        Task<ServiceResponse<bool>> AddResponse(ResponseFormDto formdto);
    }
}
