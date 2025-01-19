using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasinmazProjesiAPI.Entitites.Concrete
{
    public class Log
    {
        public int LogId { get; set; }
        public string UserMail { get; set; }
        public string Durum { get; set; }
        public string IslemTip { get; set; }
        public string Aciklama { get; set; }
        public DateTime TarihSaat { get; set; }

        public int? UserId { get; set; }

        public User User { get; set; }
    }
}
