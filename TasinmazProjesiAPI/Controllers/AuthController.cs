using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TasinmazProjesiAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class AuthController
    {
        //private readonly IAuthService _authService;

        //public AuthController(IAuthService authService)
        //{
        //    _authService = authService;
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        //{
        //    var result = await _authService.LoginAsync(loginDto.Email, loginDto.Password);
        //    if (result == null)
        //        return Unauthorized("Invalid username or password.");
        //    return Ok(result);
        //}

        //[HttpPost("register")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        //{
        //    var result = await _authService.RegisterAsync(registerDto);
        //    return Ok(result);
        //}
    }
}
