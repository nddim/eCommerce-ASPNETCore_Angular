﻿namespace WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models
{
    public class RefreshModel
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
