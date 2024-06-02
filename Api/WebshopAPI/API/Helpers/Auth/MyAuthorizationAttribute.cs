using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Helpers.Auth
{
    public class MyAuthorizationAttribute : TypeFilterAttribute
    {
        public string Roles { get; }
        public MyAuthorizationAttribute(string roles = "") : base(typeof(MyAuthorizationAsyncActionFilter)) //string roles=""
        {
            Roles = roles;
            Arguments = new object[] { roles }; // Dodajte Roles kao argument prilikom instanciranja filtera

        }
    }
    public class MyAuthorizationAsyncActionFilter : IAsyncActionFilter
    {
        private readonly string _role;

        public MyAuthorizationAsyncActionFilter(string role) //string role
        {
            _role=role;
        }
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var authService = context.HttpContext.RequestServices.GetService<MyAuthService>()!;

            if (!authService.IsLogiran())
            {
                context.Result = new UnauthorizedObjectResult("niste logirani na sistem");
                return;
            }

            var rola = _role.ToLower();

            var niz = rola.Split(',');

            //if (niz.Length>0 || rola == "" || rola == null)
            //{
            //    context.Result = new UnauthorizedObjectResult("Nema role");
            //    return;
            //}
            if (niz.Length==0 || niz.Contains("everybody"))
            {
                await next();
                return;
            }

            var isAdmin = await authService.IsAdmin();
            if (!niz.Contains("admin") &&  isAdmin)
            {
                context.Result = new UnauthorizedObjectResult("Nemate privilegije");
                return;
            }
            var isKupac=await authService.IsKupac();
            if (!niz.Contains("kupac") && isKupac)
            {
                context.Result = new UnauthorizedObjectResult("Nemate privilegije");
                return;
            }


            MyAuthInfo myAuthInfo = authService.GetAuthInfo();

            // Provera rola
            //if (!string.IsNullOrEmpty(_role) && !_role.Split(',').Any(role => authService.IsInRole(role)))
            //{
            //    context.Result = new ForbidResult();
            //    return;
            //}

            //if (myAuthInfo.korisnickiRacun.Is2FActive && !myAuthInfo.autentifikacijaToken.Is2FOtkljucano)
            //{
            //    context.Result = new UnauthorizedObjectResult("niste otkljucali 2f");
            //    return;
            //}



            await next();
        }
    }

    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    //public class RolesAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    //{
    //    //private readonly string _role;

    //    //public RolesAuthorizeAttribute(string role)
    //    //{
    //    //    _role = role;
    //    //}

    //    public void OnAuthorization(AuthorizationFilterContext context)
    //    {
    //        var authService = context.HttpContext.RequestServices.GetService<MyAuthService>()!;
    //        var hasRole = _role switch
    //        {
    //            "Admin" => authService.IsAdmin(),
    //            "Kupac" => authService.IsKupac(),
    //            _ => false
    //        };

    //        if (!hasRole)
    //        {
    //            context.Result = new ForbidResult();
    //        }
    //    }
    //}
}
