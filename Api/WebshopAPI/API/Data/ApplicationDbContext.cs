using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data.Models;
using WebAPI.Data.Models.Kategorije;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Middleware;


namespace WebshopApi.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<Proizvod> Proizvod { get; set; }
        public DbSet<Brend> Brend { get; set; }
        public DbSet<Kategorija> Kategorija{ get; set; }
        public DbSet<GlavnaKategorija> GlavnaKategorija{ get; set; }
        public DbSet<Potkategorija> Potkategorija { get; set; }
        public DbSet<Artikl> Artikl { get; set; }
        public DbSet<ProizvodSlika> ProizvodSlika { get; set; }

        public DbSet<KorisnickiRacun> KorisnickiRacun { get; set; }
        public DbSet<Kupac> Kupac { get; set; }
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<AutentifikacijaToken> AutentifikacijaToken { get; set; }
        public DbSet<RacunAktivacija> RacunAktivacija { get; set; }
        public DbSet<LogPrijava> LogPrijava { get; set; }
        public DbSet<LogOdjava> LogOdjava { get; set; }
        public DbSet<UpozorenjeKorisnickiRacun> UpozorenjeKorisnickiRacun { get; set; }
        public DbSet<BlokiranaPrijavaRačun> BlokiranaPrijavaRačun { get; set; }
        public DbSet<Korpa> Korpa { get; set; }
        public DbSet<Ocjena> Ocjena { get; set; }
        public DbSet<StatusNarudzbe> StatusNarudzbe { get; set; }
        public DbSet<Narudzba> Narudzba { get; set; }
        public DbSet<StavkeNarudzbe> StavkeNarudzbe { get; set; }
        public DbSet<CustomResponse> ExceptionLogovi { get; set; }
        public DbSet<Korisnik> Korisnik { get; set; }
        public DbSet<Otp2fa> OtpKljucevi { get; set; }
        public DbSet<ProizvodKlik> ProizvodKlik { get; set; }
        public DbSet<Vijest> Vijest { get; set; }
        public DbSet<UserKonekcija> UserKonekcija { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<KorisnickiRacun>().UseTptMappingStrategy();
        }
    }
}
