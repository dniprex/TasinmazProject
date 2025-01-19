using System.Security.Cryptography;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

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

        public ICollection<Log> Logs { get; set; }
    }
}
