using Microsoft.AspNetCore.Mvc;
using WebshopApi.Data;

namespace WebAPI.Helpers.SortiranjaEndpoint
{
    [Microsoft.AspNetCore.Mvc.Route("sortiranja-getall")]
    public class SortiranjeGetEndpoint:ControllerBase
    {
        [HttpGet]
        public ActionResult<SortiranjaGetResponse> Obradi(CancellationToken cancellationToken = default)
        {
            return Ok(new SortiranjaGetResponse()
            {
                sortiranja = Sortiranje.Sortiranja
            });
        }
    }
}
