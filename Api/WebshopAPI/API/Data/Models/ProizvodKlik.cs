using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data.Models
{
    public class ProizvodKlik
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Proizvod))]
        public int ProizvodId { get; set; }
        public Proizvod Proizvod { get; set; }
        public DateTime Datum { get; set; }
    }
}
