using static TasinmazProjesiAPI.Controllers.AuthController;
using System.Threading.Tasks;
using System;
using TasinmazProjesiAPI.DataAccess;
using TasinmazProjesiAPI.Entitites.Concrete;
using Microsoft.EntityFrameworkCore;

namespace TasinmazProjesiAPI.Business.Abstract
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        public AuthRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> Login(string userEmail, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.userEmail == userEmail);

            if (user == null)
            {
                throw new UserNotFoundException("Kullanıcı bulunamadı!");
            }

            if (string.IsNullOrEmpty(user.UserRole))
            {
                throw new Exception("Role değeri null veya boş!");
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                throw new InvalidPasswordException("Şifre yanlış!");
            }

            return user;
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

        public async Task<User> Register(User user, string password)
        {
            if (await UserExists(user.userEmail))
            {
                throw new UserAlreadyExistsException("Bu e-posta adresi zaten kayıtlı!");
            }

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string userEmail)
        {
            Console.WriteLine($"Checking if user exists: {userEmail}");
            var exists = await _context.Users
                .AnyAsync(x => x.userEmail.ToLower() == userEmail.ToLower());
            Console.WriteLine($"User exists result: {exists}");
            return exists;
        }




    }
}
