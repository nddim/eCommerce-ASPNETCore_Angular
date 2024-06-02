using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Models;

namespace WebAPI.Endpoints.Ocjena.Pretraga
{
    public class OcjenaPretragaResponse
    {
        public int Id { get; set; }
        public int Vrijednost { get; set; }
        public int ProizvodId { get; set; }
        public string KupacId { get; set; }
    }
}
