using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;

namespace WebAPI.Endpoints.Korpa.Add
{
    public class AddKorpaArtikalRequest
    {
        public int ProizvodId { get; set; }
        public int Kolicina { get; set; }

    }
}
