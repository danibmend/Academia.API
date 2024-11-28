using Microsoft.AspNetCore.Mvc;

namespace academia.WebApi.Controllers.Base.Response
{
    public class PutResponse : IResponse
    {
        public IActionResult Execute()
        {
            return new NoContentResult();
        }

        public IActionResult Execute(object result)
        {
            return new OkObjectResult(result);
        }
    }
}
