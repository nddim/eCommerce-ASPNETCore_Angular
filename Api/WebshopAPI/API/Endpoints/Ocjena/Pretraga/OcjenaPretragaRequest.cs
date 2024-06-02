using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Data.Models;

namespace WebAPI.Endpoints.Ocjena.Pretraga
{
    public class OcjenaPretragaRequest
    {
        public int ProizvodId { get; set; }
    }
}
