namespace WebAPI.Helpers.Auth.EmailSlanje
{
    public interface IEmailService
    {
        Task PosaljiEmail(string subject, string body, string receiver);
        Task PosaljiEmailObj(EmailPoruka obj);
    }
}
