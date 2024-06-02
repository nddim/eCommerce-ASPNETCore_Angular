using System.Security.Claims;
using Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;
using static System.Net.Mime.MediaTypeNames;

namespace WebAPI.Helpers.SignalR
{
    public class SignalRHub:Hub
    {

    }
}
