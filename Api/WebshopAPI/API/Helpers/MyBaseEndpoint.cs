using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Helpers
{
    [ApiController]
    public abstract class MyBaseEndpoint<TRequest, TResponse> : ControllerBase
    {
        public abstract Task<ActionResult<TResponse>> Obradi(TRequest request, CancellationToken cancellationToken=default);
    }
}
