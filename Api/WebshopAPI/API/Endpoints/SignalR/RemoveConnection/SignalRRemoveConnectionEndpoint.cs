using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebAPI.Helpers;
using WebAPI.Helpers.SignalR;
using WebAPI.Services.JwtHeader;
using WebshopApi.Data;

namespace WebAPI.Endpoints.SignalR.RemoveConnection
{
    [Route("signal-r")]
    public class SignalRRemoveConnectionEndpoint:MyBaseEndpoint<SignalRRemoveConnectionRequest, NoResponse>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IHubContext<SignalRHub> _hubContext;
        public SignalRRemoveConnectionEndpoint(ApplicationDbContext applicationDbContext, IHubContext<SignalRHub> hubContext)
        {
            _applicationDbContext = applicationDbContext;
            _hubContext = hubContext;
        }
        [HttpDelete("remove-connection")]
        public override async Task<ActionResult<NoResponse>> Obradi([FromQuery]SignalRRemoveConnectionRequest request, CancellationToken cancellationToken = default)
        {
            if (request == null)
            {
                return BadRequest("UserId nije pronaden.");
            }
            var connections = await _applicationDbContext.UserKonekcija.Where(x => x.UserId == request.UserId).ToListAsync();

            foreach (var connection in connections)
            {
                _applicationDbContext.UserKonekcija.Remove(connection);
            }
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return Ok();
        }
    }
}
