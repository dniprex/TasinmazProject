using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasinmazProjesiAPI.Entitites.Concrete
{
    public class Ilce
    {
        public int Id { get; set; }
        public string IlceAdi { get; set; }
        
        [ForeignKey("IlId")]
        public int IlId { get; set; }

        public Il Il { get; set; }
    }
}
