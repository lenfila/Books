using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    public class HomeController : Controller
    {
            /// <summary>
            /// Возвращение слова "Hello!"
            /// </summary>
            /// <returns>Контент "Hello!"</returns>
            public IActionResult Index()
            {
                return Content("Hello!");
            }
        
    }
}
