namespace WebAPI.Services.Sms
{
    public interface ISMSService
    {
        Task PosaljiSMSPotvrdaNarudzbe(string brojTelefona, string Korisnik);
    }
}
