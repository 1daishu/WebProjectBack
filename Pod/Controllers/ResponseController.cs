using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pod.Dto;
using Pod.IServices;
using Pod.Models.ServiceResponses;

namespace Pod.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponseController : ControllerBase
    {
        private readonly ICommentService _comment;
        public ResponseController(ICommentService _comment)
        {
            this._comment = _comment;
        }




        [HttpGet("GetAllResponse")]
        public async Task<ActionResult<ServiceResponse<List<ResponseDto>>>> GetAllResponse()
        {
            var response = await _comment.GetAllResponse();
            return Ok(response);
        }

        [HttpPost("AddResponse")]
        public async Task<ActionResult<ServiceResponse<bool>>> AddResponse(ResponseFormDto formdto)
        {
            var response = await _comment.AddResponse(formdto);
            return Ok(response);
        }
    }
}
