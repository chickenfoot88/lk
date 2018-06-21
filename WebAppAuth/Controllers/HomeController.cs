using System.Web.Mvc;

namespace WebAppAuth.Controllers
{
    /// <summary>
    /// Главная страница
    /// </summary>
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("RedirectByUserRole", "Account");
        }
    }
}