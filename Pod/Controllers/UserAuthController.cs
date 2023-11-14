using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pod.Dto;
using Pod.IServices;
using Pod.Models.ServiceResponses;

namespace Pod.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAuthController : ControllerBase
    {
        private readonly IUserAuthService _user;
        public UserAuthController(IUserAuthService _user)
        {
            this._user = _user;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<bool>>> Register(RegisterFormDto regform)
        {
            var response = await _user.Register(regform);
            return Ok(response);
        }

        [HttpGet("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(string email, string password)
        {
            var response = await _user.Login(email, password);
            return Ok(response);
        }

    }
}
