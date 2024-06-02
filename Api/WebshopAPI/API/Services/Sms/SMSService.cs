using Vonage;
using Vonage.Request;

namespace WebAPI.Services.Sms
{
    public class SMSService:ISMSService
    {
        private readonly IConfiguration _configuration;

        public SMSService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task PosaljiSMSPotvrdaNarudzbe(string brojTelefona, string Korisnik)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
                "key1",
                "key2"
            );
            var vonageClient = new VonageClient(credentials);

            var response = await vonageClient.SmsClient.SendAnSmsAsync(new Vonage.Messaging.SendSmsRequest()
            {
                To = brojTelefona,
                From = "eWebShop",
                Text = $"Postovani {Korisnik}, vasa narudzba je poslata. "
            });
        }
    }
}
