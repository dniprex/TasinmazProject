using System.Security.Cryptography;
using System.Text;
using System;
using System.Linq;

namespace TasinmazProjesiAPI.Business.Concrete
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Verilen şifreyi SHA-256 algoritması kullanarak hashler ve byte[] olarak döner.
        /// </summary>
        /// <param name="password">Hashlenecek şifre</param>
        /// <returns>Byte array formatında hashlenmiş şifre</returns>
        public static byte[] HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Şifre boş olamaz.");

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password); // Şifreyi byte[] formatına çevir
                return sha256.ComputeHash(bytes); // SHA-256 hash işlemi
            }
        }

        /// <summary>
        /// Hashlenmiş şifreyi kontrol etmek için kullanılır.
        /// </summary>
        /// <param name="password">Kullanıcı tarafından girilen şifre</param>
        /// <param name="hashedPassword">Veritabanında saklanan hashlenmiş şifre (byte[])</param>
        /// <returns>Eşleşme sonucu</returns>
        public static bool VerifyPassword(string password, byte[] hashedPassword)
        {
            var hashOfInput = HashPassword(password); // Giriş şifresini hashle
            return hashOfInput.SequenceEqual(hashedPassword); // Byte[] karşılaştırması
        }

        /// <summary>
        /// Hashlenmiş byte[] verisini Base64 string formatına çevirir.
        /// </summary>
        /// <param name="hashedPassword">Hashlenmiş şifre (byte[])</param>
        /// <returns>Base64 formatında string</returns>
        public static string HashToBase64(byte[] hashedPassword)
        {
            return Convert.ToBase64String(hashedPassword);
        }

        /// <summary>
        /// Base64 formatındaki hashlenmiş şifreyi byte[] olarak döner.
        /// </summary>
        /// <param name="base64Hash">Base64 formatında hashlenmiş şifre</param>
        /// <returns>Byte array formatında hash</returns>
        public static byte[] Base64ToHash(string base64Hash)
        {
            return Convert.FromBase64String(base64Hash);
        }
    }
}
