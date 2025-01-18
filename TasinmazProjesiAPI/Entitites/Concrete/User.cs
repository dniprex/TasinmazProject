using System.Security.Cryptography;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;

namespace TasinmazProjesiAPI.Entitites.Concrete
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string userEmail { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string UserRole { get; set; }
    }
}
