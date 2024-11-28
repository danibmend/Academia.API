using Microsoft.AspNetCore.Mvc;

namespace academia.WebApi.Controllers.Base
{
    public interface IResponse
    {
        IActionResult Execute();

        IActionResult Execute(object result);
    }
}
