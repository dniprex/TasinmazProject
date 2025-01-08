using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasinmazProjesiAPI.Entitites.Concrete
{
    public class Mahalle
    {
        public int Id { get; set; }
        public string MahalleAdi { get; set; }

        [ForeignKey("IlceId")]
        public int IlceId { get; set; }

        public Ilce Ilce { get; set; }
    }
}
