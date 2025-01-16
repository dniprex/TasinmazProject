using System.Security.Cryptography;
using System.Text;
using System;

namespace TasinmazProjesiAPI.Business.Concrete
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Verilen şifreyi SHA-256 algoritması kullanarak hashler.
        /// </summary>
        /// <param name="password">Hashlenecek şifre</param>
        /// <returns>Base64 formatında hashlenmiş şifre</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Şifre boş olamaz.");

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        /// <summary>
        /// Hashlenmiş şifreyi kontrol etmek için kullanılır.
        /// </summary>
        /// <param name="password">Kullanıcı tarafından girilen şifre</param>
        /// <param name="hashedPassword">Veritabanında saklanan hashlenmiş şifre</param>
        /// <returns>Eşleşme sonucu</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hashedPassword;
        }
    }
}
