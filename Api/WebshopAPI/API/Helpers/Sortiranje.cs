namespace WebAPI.Helpers
{
    public class Sortiranje
    {
        public int Id { get; set; }
        public string Naziv { get; set; }


        public static List<Sortiranje> Sortiranja = new List<Sortiranje>
        {
            new Sortiranje {Id = 1, Naziv = "Pozicija"},
            new Sortiranje {Id = 2, Naziv = "Cijena - rastuća"},
            new Sortiranje {Id = 3, Naziv = "Cijena - opadajuća"},
            new Sortiranje {Id = 4, Naziv = "Naziv - a do ž"},
            new Sortiranje {Id = 5, Naziv = "Naziv - ž do a"},
            new Sortiranje {Id = 6, Naziv = "Datum - prvo najnoviji"},
            new Sortiranje {Id = 7, Naziv = "Datum - prvo najstariji"}
        };
    }
}
