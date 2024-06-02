using Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebAPI.Data.Models;
using WebAPI.Endpoints.AuthEndpoints.MicrosoftIdentity.Models;
using WebAPI.Helpers;
using WebAPI.Helpers.SignalR;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.SignalR.AddConnection
{
    [Route("signal-r")]
    public class SignalRAddConnectionEndpoint:MyBaseEndpoint<SignalRAddConnectionRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IJwtHeaderService _jwtHeaderService;
        private readonly IHubContext<SignalRHub> _hubContext;
        public SignalRAddConnectionEndpoint(ApplicationDbContext applicationDbContext, IJwtHeaderService jwtHeaderService, IHubContext<SignalRHub> hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _jwtHeaderService = jwtHeaderService;
            _hubContext = hubContext;
        }
        [HttpPost("add-konekciju")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromBody]SignalRAddConnectionRequest request, CancellationToken cancellationToken = default)
        {
            //if (request == null)
            //{
            //    return BadRequest("Nista u requestu");
            //}
            await _hubContext.Groups.AddToGroupAsync(request.ConnectionId, request.UserId);

            var userKonekcija = new UserKonekcija()
            {
                UserId = request.UserId,
                ConnectionId = request.ConnectionId
            };
            await _applicationDbContext.UserKonekcija.AddAsync(userKonekcija);
            await _applicationDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
