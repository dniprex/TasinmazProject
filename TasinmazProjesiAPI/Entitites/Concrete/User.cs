using System.Security.Cryptography;
using System.Text;
using System;

namespace TasinmazProjesiAPI.Entitites.Concrete
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; }
        public string HashedPassword { get; set; }
        public string UserRole { get; set; }
        public string Adres {  get; set; }
    }
}
