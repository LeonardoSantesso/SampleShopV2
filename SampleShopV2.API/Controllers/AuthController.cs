using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Models;
using Security.Services.Interfaces;
using System.Security.Claims;

namespace SampleShopV2.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> Signin([FromBody] Login login)
        {
            if (login == null)
                return BadRequest("Ivalid client request");

            var token = await _loginService.AuthenticateAsync(login);
            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] Token token)
        {
            if (token is null)
                return BadRequest("Ivalid client request");

            var tokenResult = await _loginService.RefreshTokenAsync(token);

            if (tokenResult == null)
                return BadRequest("Ivalid client request");

            return Ok(tokenResult);
        }


        [HttpGet]
        [Route("revoke")]
        [Authorize("Bearer")]
        public async Task<IActionResult> Revoke()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(userName))
            {
                return BadRequest("Invalid client request: User not found.");
            }

            var result = await _loginService.RevokeTokenAsync(userName);

            if (!result)
            {
                return BadRequest("Invalid client request");
            }

            return NoContent();
        }
    }
}
