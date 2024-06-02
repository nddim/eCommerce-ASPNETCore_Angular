using WebAPI.Data.Models;
using WebAPI.Helpers;

namespace WebAPI.Endpoints.Narudzba.Get
{
    public class GetUserNarudzbaResponseObj:PagedListBaseClass
    {
        public List<GetUserNarudzbaResponse> Narudzbe { get; set; }
    }
    public class GetUserNarudzbaResponse
    {
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string Dostava { get; set; }
        public string StatusNarudzbe { get; set; }
        public int UkupnaCijena { get; set; }
    }

   
}

