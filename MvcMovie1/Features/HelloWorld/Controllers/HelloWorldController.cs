using Microsoft.AspNetCore.Mvc;

namespace MvcMovie2.Features.HelloWorld.Controllers
{
    [Route("hello")]
    public class HelloWorldController : Controller
    {
        // GET: /hello
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome
        [HttpGet("welcome/{name?}/{numTimes?}")]
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = numTimes;
            return View();
        }

    }
}
