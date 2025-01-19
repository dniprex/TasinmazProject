using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TasinmazProjesiAPI.Business.Abstract;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Dtos;
using TasinmazProjesiAPI.Entitites.Concrete;

namespace TasinmazProjesiAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthRepository _authRepository;

        public UserController(ApplicationDbContext context, IAuthRepository authRepository)
        {
            _context = context;
            _authRepository = authRepository ?? throw new ArgumentNullException(nameof(authRepository));
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users
                .Select(u => new
                {
                    Id = u.Id,
                    Email = u.userEmail,
                    Role = u.UserRole
                })
                .ToListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            return Ok(new
            {
                Id = user.Id,
                Email = user.userEmail,
                Role = user.UserRole
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegisterDTO registerUserDto)
        {
            if (registerUserDto == null || string.IsNullOrWhiteSpace(registerUserDto.UserEmail) || string.IsNullOrWhiteSpace(registerUserDto.Password))
            {
                return BadRequest(new { message = "E-posta ve şifre bilgisi eksik." });
            }

            if (await _authRepository.UserExists(registerUserDto.UserEmail))
            {
                return BadRequest(new { message = "Bu e-posta adresi zaten kayıtlı." });
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(registerUserDto.Password, out passwordHash, out passwordSalt);

            var newUser = new User
            {
                userEmail = registerUserDto.UserEmail.Trim().ToLower(),
                UserRole = registerUserDto.UserRole,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, new
            {
                Id = newUser.Id,
                Email = newUser.userEmail,
                Role = newUser.UserRole
            });
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key; 
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); // Hash
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDTO updatedUserDto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            user.userEmail = updatedUserDto.UserEmail ?? user.userEmail;
            user.UserRole = updatedUserDto.UserRole ?? user.UserRole;

            if (!string.IsNullOrWhiteSpace(updatedUserDto.Password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(updatedUserDto.Password, out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Kullanıcı başarıyla güncellendi." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(new { message = "Kullanıcı bulunamadı." });
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Kullanıcı başarıyla silindi." });
        }

        [HttpGet("GetUserIdByEmail")]
        public IActionResult GetUserIdByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.userEmail == email);
            if (user != null)
            {
                return Ok(user.Id);
            }
            else
            {
                return NotFound(); 
            }
        }
    }
}
