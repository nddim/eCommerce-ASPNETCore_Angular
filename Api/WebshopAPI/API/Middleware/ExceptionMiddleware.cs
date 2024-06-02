
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Net.Mime;
using System.Text.Json;
using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers.Auth;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly IJwtHeaderService _jwtHeaderService;
        public ApplicationDbContext db { get; }
        public ExceptionMiddleware(ApplicationDbContext dbContext, IJwtHeaderService jwtHeaderService)
        {
            db = dbContext;
            _jwtHeaderService = jwtHeaderService;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;

            //var authService = context.RequestServices.GetService<MyAuthService>()!;
            var queryString = context.Request.GetEncodedPathAndQuery();
            var metoda = context.Request.Method;
            
            var response = new CustomResponse()
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message,
                Details = ex.StackTrace?.ToString() ?? "Nema stack trace",
                QueryPath = queryString,
                Vrijeme = DateTime.Now,
                Metoda = metoda,
            };

            var user = await _jwtHeaderService.GetUser();
            response.Korisnik = user;

            //ApplicationDbContext db = context.Request.HttpContext.RequestServices.GetService<ApplicationDbContext>();

            db.ExceptionLogovi.Add(response);
            await db.SaveChangesAsync();

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(new {StatusCode=response.StatusCode, Message=ex.Message, Metoda=metoda}, options);
            await context.Response.WriteAsync(json);
        }
    }
    public class CustomResponse
    {
        [Key]
        public int Id { get; set; }
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? Details { get; set; }
        public string? QueryPath { get; set; }
        public DateTime Vrijeme { get; set; }
        public string? Metoda { get; set; }

        [ForeignKey(nameof(Korisnik))]
        public string? KorisnikID { get; set; }
        public Korisnik? Korisnik { get; set; }


    }
}
