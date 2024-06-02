using MailKit.Net.Smtp;
using MimeKit;
using System.Text;

namespace WebAPI.Helpers.Auth.EmailSlanje
{
    public class EmailSenderService:IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailSenderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        public async Task PosaljiEmail(string subject, string body, string receiver)
        {
            string fromMail = _configuration.GetValue<string>("EmailSettings:Email");
            string fromPassword = _configuration.GetValue<string>("EmailSettings:Password");
            string host = _configuration.GetValue<string>("EmailSettings:Host");
            
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Webshop api", fromMail));
            email.To.Add(new MailboxAddress("Receiver Name", receiver));

            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(host, 465, true);

                // Note: only needed if the SMTP server requires authentication
                await smtp.AuthenticateAsync(fromMail, fromPassword);

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }

            //var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse(fromMail));
            //email.To.Add(MailboxAddress.Parse(receiver));
            //email.Subject = subject;
            //email.Body = new TextPart(TextFormat.Plain) { Text = body };

            //using var smtp = new SmtpClient();
            //smtp.Connect("smtp.gmail.email", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate(fromMail, fromPassword);
            //smtp.Send(email);
            //smtp.Disconnect(true);

            //return Ok();
        }
        public async Task PosaljiEmailObj(EmailPoruka obj)
        {
            string fromMail = _configuration.GetValue<string>("EmailSettings:Email");
            string fromPassword = _configuration.GetValue<string>("EmailSettings:Password");
            string host = _configuration.GetValue<string>("EmailSettings:Host");

            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("Webshop Fit", fromMail));
            email.To.AddRange(obj.EmailAdrese);
            
            email.Subject = obj.Naslov;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = obj.Body
            };

            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(host, 465, true);

                // Note: only needed if the SMTP server requires authentication
                await smtp.AuthenticateAsync(fromMail, fromPassword);

                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
            }

            //var email = new MimeMessage();
            //email.From.Add(MailboxAddress.Parse(fromMail));
            //email.To.Add(MailboxAddress.Parse(receiver));
            //email.Subject = subject;
            //email.Body = new TextPart(TextFormat.Plain) { Text = body };

            //using var smtp = new SmtpClient();
            //smtp.Connect("smtp.gmail.email", 587, SecureSocketOptions.StartTls);
            //smtp.Authenticate(fromMail, fromPassword);
            //smtp.Send(email);
            //smtp.Disconnect(true);

            //return Ok();
        }

        //public void Posalji(string to, string messageSubject, string messageBody, bool isBodyHtml = false)
        //{
        //    if (to == "")
        //        return;

        //    string host = this._configuration.GetValue<string>("MojEmailServer:Host");
        //    int port = this._configuration.GetValue<int>("MojEmailServer:Port");
        //    string from = this._configuration.GetValue<string>("MojEmailServer:From");
        //    string lozinka = this._configuration.GetValue<string>("MojEmailServer:Lozinka");

        //    SmtpClient SmtpServer = new SmtpClient(host, port);
        //    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        //    MailMessage email = new MailMessage();
        //    // START
        //    email.From = new MailAddress(from);
        //    email.To.Add(to);
        //    //  email.CC.Add(SendMailFrom);
        //    email.Subject = messageSubject;
        //    email.Body = messageBody;
        //    email.IsBodyHtml = isBodyHtml;
        //    //END
        //    SmtpServer.Timeout = 5000;
        //    SmtpServer.EnableSsl = true;
        //    SmtpServer.UseDefaultCredentials = false;
        //    SmtpServer.Credentials = new NetworkCredential(from, lozinka);
        //    SmtpServer.Send(email);
        //}
    }

    public class EmailPoruka
    {
        public string Naslov { get; set; }
        public string Body { get; set; }
        public InternetAddressList EmailAdrese { get; set; }
        public EmailPoruka(string Naslov, string Body, List<PrimalacEmail> adrese)
        {
            this.Naslov = Naslov;
            this.Body=Body;
            EmailAdrese = new InternetAddressList();
            foreach (var adresa in adrese)
            {
                EmailAdrese.Add(new MailboxAddress(adresa.ImeKorisnika, adresa.Email));
            }
        }
    }
    public class PrimalacEmail
    {
        public string ImeKorisnika { get; set; }
        public string Email { get; set; }
    }


   
}

