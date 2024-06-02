namespace WebAPI.Endpoints.Analitika.ProizvodiKlik
{
    public class ProizvodKlikAnalitikaBrojDana
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public DateTime DatumPocetka { get; set; }

        public static List<ProizvodKlikAnalitikaBrojDana> Dani = new List<ProizvodKlikAnalitikaBrojDana>
        {
            new ProizvodKlikAnalitikaBrojDana {Id = 1, Naziv = "Zadnja sedmica", DatumPocetka=DateTime.Now.AddDays(-7)},
            new ProizvodKlikAnalitikaBrojDana {Id = 2, Naziv = "Zadnjih mjesec dana", DatumPocetka=DateTime.Now.AddDays(-30)},
            new ProizvodKlikAnalitikaBrojDana {Id = 3, Naziv = "Zadnje tromjesečje", DatumPocetka= DateTime.Now.AddDays(-90)},
            new ProizvodKlikAnalitikaBrojDana {Id = 4, Naziv = "Zadnjih pola godine", DatumPocetka=DateTime.Now.AddDays(-180)},
            new ProizvodKlikAnalitikaBrojDana {Id = 5, Naziv = "Kompletna historija", DatumPocetka=DateTime.MinValue},
        };
    }
}
