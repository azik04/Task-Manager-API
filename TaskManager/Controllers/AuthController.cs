using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Dto.AUTH;
using TaskManager.Core.Interfaces;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        public AuthController(IAuthService service)
        {
            _service = service;
        }
        

        [HttpPost("Auth")]
        public async Task<IActionResult> Auth(AuthDto dto)
        {
            var res = await _service.LogIn(dto);
            if (res.Success)
                return Ok(res);

            return BadRequest();
        }
    }
}
