using Arabam.Com.Application.Adverts.Queries.GetAdvertsById;
using Arabam.Com.Application.Adverts.Queries.GetAdvertsWithPagination;
using Arabam.Com.Application.AdvertVisits.Commands.AddQueueAdvertVisit;
using Arabam.Com.Application.Common.Model;
using Microsoft.AspNetCore.Mvc;

namespace Arabam.Com.Api.Controllers
{
    [Route("api/advert")]
    public class AdvertController : ApiControllerBase
    {

        [Route("visit")]
        [HttpPost]
        public async Task<ActionResult> CreateAdvertVisitAsync(AddQueueAdvertVisitCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult> GetAdvertAsync(int id)
        {
            var advert = await Mediator.Send(new GetAdvertsByIdQuery { Id = id });

            if (advert == null)
            {
                return NoContent();
            }

            return Ok(advert);
        }

        [Route("all")]
        [HttpGet]
        public async Task<ActionResult<PaginatedList<GetAdvertsWithPaginationDTO>>> GetAdvertAllAsync([FromQuery] GetAdvertsWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
