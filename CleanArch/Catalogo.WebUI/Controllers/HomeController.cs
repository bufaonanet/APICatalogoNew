using Microsoft.AspNetCore.Mvc;

namespace Catalogo.WebUI.Controllers
{
    public class HomeController : Controller
    {       
        public IActionResult Index()
        {
            return View();
        }        
    }
}