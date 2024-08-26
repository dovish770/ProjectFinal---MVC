using Microsoft.AspNetCore.Mvc;

namespace ProjectFinal___MVC.Controllers
{
    public class MissionsContoller : Controller
    {
        public IActionResult GetAllMissions()
        {
            return View();
        }
    }
}
