namespace WebAPI.Endpoints.AuthEndpoints.ChangePassword
{
    public class AuthChangePasswordResponse
    {
        public bool PogresanMail { get; set; } = false;
        public bool PogresnaStaraLozinka { get; set; } = false;
        public bool NeispravnaNovaLozinka { get; set; } = false;

    }
}
