using System.ComponentModel.DataAnnotations.Schema;

namespace TasinmazProjesiAPI.Entitites.Concrete
{
    public class Tasinmaz
    {
        public int Id { get; set; }
        public string TasinmazIsim { get; set; }
        public int TasinmazParsel { get; set; }
        public string TasinmazNitelik { get; set; }
        public string TasinmazAdres { get; set; }

        [ForeignKey("MahalleId")]
        public int MahalleId { get; set; }

        public Mahalle Mahalle { get; set; }

        public string Ada { get; set; }
        public string KoordinatBilgisi { get; set; }
    }
}
