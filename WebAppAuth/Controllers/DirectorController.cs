namespace WebAppAuth.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Личный кабинет руководителя
    /// </summary>
    [Authorize(Roles = AppConstants.RoleDirector)]
    public class DirectorController : BaseController
    {
        /// <summary>
        /// Аналитика начисления и оплат
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleDirector)]
        public ActionResult Index(FormCollection formValue)
        {
            return View();
        }

        /// <summary>
        /// Заявки
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleDirector)]
        public ActionResult Claims(FormCollection formValue)
        {
            return View();
        }

        /// <summary>
        /// Должники
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleDirector)]
        public ActionResult Debtors(FormCollection formValue)
        {
            return View();
        }

        /// <summary>
        /// Коммунальные услуги
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleDirector)]
        public ActionResult Services(FormCollection formValue)
        {
            return View();
        }

        /// <summary>
        /// Начисления и платежи
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleDirector)]
        public ActionResult Payments(FormCollection formValue)
        {
            return View();
        }

        /// <summary>
        /// Собираемость
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = AppConstants.RoleDirector)]
        public ActionResult Collections(FormCollection formValue)
        {
            return View();
        }
    }
}
