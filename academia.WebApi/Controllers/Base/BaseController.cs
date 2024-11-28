using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace academia.WebApi.Controllers.Base
{
    [ApiController]
    [Route("api")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IResponseFactory _responseFactory;

        public BaseController(IServiceProvider serviceProvider)
        {
            _responseFactory = serviceProvider.GetRequiredService<IResponseFactory>();
        }

        protected new IActionResult Response(object result)
        {
            var response = _responseFactory.CreateResponse(Request.Method);
            if (result == null)
                return response.Execute();

            return response.Execute(result);
        }

        protected new IActionResult Response()
        {
            var response = _responseFactory.CreateResponse(Request.Method);
            return response.Execute();
        }
    }
}
