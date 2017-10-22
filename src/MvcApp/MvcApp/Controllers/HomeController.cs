namespace MvcApp.Controllers
{
    using MvcFramework.Attributes;
    using MvcFramework.Contracts;
    using MvcFramework.Controllers;

    public class HomeController :Controller
    {
        [HttpGet]
        public IActionResult Index(int id)
        {
            return View();
        }
    }
}
