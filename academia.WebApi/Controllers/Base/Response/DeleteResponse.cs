using Microsoft.AspNetCore.Mvc;

namespace academia.WebApi.Controllers.Base.Response
{
    public class DeleteResponse : IResponse
    {
        public IActionResult Execute()
        {
            return new NoContentResult();
        }

        public IActionResult Execute(object result)
        {
            return new NoContentResult();
        }
    }
}
