using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RRHH_Backend.Common.Core.Controller
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ApiController : ControllerBase
    {

        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    }

}
