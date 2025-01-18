using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using TasinmazProjesiAPI.DataAccess;
using Microsoft.EntityFrameworkCore;
using TasinmazProjesiAPI.Entitites.Concrete;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TasinmazProjesiAPI.Business.Concrete;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.Dtos;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
namespace TasinmazProjesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IAuthRepository _authRepository;
        private ILogService _logService;

        private IConfiguration _configuration;
        public AuthController(IAuthRepository authRepository,
                        IConfiguration configuration,
                        ILogService logService)
        {
            _authRepository = authRepository;
            _configuration = configuration;
            _logService = logService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDTO userForRegister)
        {
            try
            {
                var userToCreate = new User
                {
                    userEmail = userForRegister.UserEmail,
                    UserRole = userForRegister.UserRole,
                };

                var createdUser = await _authRepository.Register(userToCreate, userForRegister.Password);
                return StatusCode(201, new { message = "Kullanıcı başarıyla oluşturuldu." });
            }
            catch (UserAlreadyExistsException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}, Inner: {ex.InnerException?.Message}");
                return StatusCode(500, $"Bir hata oluştu: {ex.Message}, Inner: {ex.InnerException?.Message}");
            }


        }
        public class UserAlreadyExistsException : Exception
        {
            public UserAlreadyExistsException(string message) : base(message) { }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] UserForLoginDTO userForLogin)
        {
            try
            {
                if (string.IsNullOrEmpty(userForLogin.email) || string.IsNullOrEmpty(userForLogin.password))
                {
                    return BadRequest(new { Message = "E-posta ve şifre boş olamaz." });
                }

                var user = await _authRepository.Login(userForLogin.email, userForLogin.password);

                var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Token"]);
                if (key == null || key.Length < 16)
                {
                    throw new Exception("AppSettings:Token geçersiz. Lütfen geçerli bir JWT anahtarı tanımlayın.");
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.userEmail),
                new Claim(ClaimTypes.Role, user.UserRole)
                    }),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { token = tokenString });
            }
            catch (UserNotFoundException ex)
            {
                Console.WriteLine($"Kullanıcı bulunamadı. Email: {userForLogin.email}, Hata: {ex.Message}");
                return NotFound(new { Message = "Kullanıcı bulunamadı." });
            }
            catch (InvalidPasswordException ex)
            {
                Console.WriteLine($"Geçersiz şifre denemesi. Email: {userForLogin.email}, Hata: {ex.Message}");
                return BadRequest(new { Message = "Geçersiz şifre." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Bilinmeyen bir hata oluştu: {ex.Message}, Inner: {ex.InnerException?.Message}");
                return StatusCode(500, "Sunucu tarafında bir hata oluştu.");
            }
        }

        public class UserNotFoundException : Exception
        {
            public UserNotFoundException(string message) : base(message) { }
        }

        public class InvalidPasswordException : Exception
        {
            public InvalidPasswordException(string message) : base(message) { }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }

                }
                return true;

            }
        }
    }
}
