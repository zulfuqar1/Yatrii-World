using Microsoft.AspNetCore.Mvc;
namespace YatriiWorld.MVC.Controllers
{
    public class ErrorController : Controller
    {
        
        [Route("Error/{code}")]
        public IActionResult HandleError(int code)
        {
          
            if (code == 404)
            {
                return View("Error404");
            }

            return View("GeneralError");
        }
    }
}