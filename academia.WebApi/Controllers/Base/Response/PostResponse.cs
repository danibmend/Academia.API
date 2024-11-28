using Microsoft.AspNetCore.Mvc;

namespace academia.WebApi.Controllers.Base.Response
{
    public class PostResponse : IResponse
    {
        public IActionResult Execute()
        {
            return new CreatedAtActionResult(null,null, null, null);
        }

        public IActionResult Execute(object result)
        {
            return new AcceptedResult(string.Empty, result);
        }
    }
    
}
