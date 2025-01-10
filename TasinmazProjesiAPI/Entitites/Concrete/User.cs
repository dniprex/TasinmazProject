using System.Security.Cryptography;
using System.Text;
using System;

namespace TasinmazProjesiAPI.Entitites.Concrete
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
    }
}
public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
