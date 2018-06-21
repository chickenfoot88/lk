using System.Threading.Tasks;
using System.Web;
using Castle.Core.Internal;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebAppAuth.Models;

namespace WebAppAuth.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using Context;

    /// <summary>
    /// Раздел администрирования
    /// </summary>
    [Authorize(Roles = AppConstants.RoleAdministrator)]
    public class AdministratorController : BaseController
    {
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();

        /// <summary>
        /// Главная страница личного кабинета
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        
        /// <summary>
        /// Получить список ролей
        /// </summary>
        /// <returns></returns>
        public ActionResult RolesList()
        {
            return View(_dbContext.Roles.ToList());
        }

        /// <summary>
        /// Создать новую роль
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateRole()
        {
            return View();
        }

        /// <summary>
        /// Создать новую роль
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateRole(FormCollection collection)
        {
            try
            {
                _dbContext.Roles.Add(new Role
                {
                    Name = collection["RoleName"]
                });
                _dbContext.SaveChanges();
                ViewBag.ResultMessage = "Роль успешно создана!";
                return RedirectToAction("RolesList");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <returns></returns>
        //
        // GET: /Administrator/CreateUser
        public ActionResult CreateUser()
        {
            return View();
        }

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <returns></returns>
        //
        // POST: /Administrator/CreateUser
        [HttpPost]
        public async Task<ActionResult> CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

                var user = new ApplicationUser
                {
                    UserName = model.Email.Replace('-', '_'),
                    Email = model.Email,
                    CreationTime = DateTime.Now
                };

                var result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //TODO: доработать чтоб с формы получать роль
                    //пока жестко зашито
                    userManager.AddToRole(user.Id, AppConstants.RoleWorker);

                    return RedirectToAction("ManageUserRoles", "Administrator");
                }
                AddErrors(result);
            }

            return View(model);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        
        /// <summary>
        /// Изменить наименование роли
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public ActionResult EditRole(string roleName)
        {
            var thisRole =
                _dbContext.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            return View(thisRole);
        }

        /// <summary>
        /// Изменить наименование роль
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRole(Role role)
        {
            try
            {
                _dbContext.Entry(role).State = System.Data.Entity.EntityState.Modified;
                _dbContext.SaveChanges();

                return RedirectToAction("RolesList");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Удалить роль
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public ActionResult DeleteRole(string roleName)
        {
            var thisRole =
                _dbContext.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase));
            _dbContext.Roles.Remove(thisRole);
            _dbContext.SaveChanges();
            return RedirectToAction("RolesList");
        }

        /// <summary>
        /// Администрирование пользовательских ролей
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageUserRoles()
        {
            ViewBag.Roles =
                _dbContext.Roles.OrderBy(r => r.Name)
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                    .ToList();

            ViewBag.Users =
                _dbContext.Users
                    .OrderByDescending(x => x.CreationTime)
                    .Select(x => new {x.Email, x.CreationTime })
                    .ToList();

            return View();
        }

        /// <summary>
        /// Получить список пользователей
        /// </summary>
        /// <returns></returns>
        public ActionResult GetUsersList()
        {
            ViewBag.Roles =
                _dbContext.Roles.OrderBy(r => r.Name)
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                    .ToList();

            ViewBag.Users =
                _dbContext.Users
                    .OrderByDescending(x => x.CreationTime)
                    .Select(x => new {x.Email, x.CreationTime}).ToList();

            return View("ManageUserRoles");
        }

        /// <summary>
        /// Получить роли пользователя 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRolesForUser(string userName)
        {
            ViewBag.Roles =
                _dbContext.Roles.OrderBy(r => r.Name)
                    .ToList()
                    .Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name })
                    .ToList();

            if (string.IsNullOrEmpty(userName))
            {
                ViewBag.ResultMessage = "Не введен email!";
                return View("ManageUserRoles");
            }

            var user =
                _dbContext.Users.FirstOrDefault(
                    u => u.Email.Equals(userName, StringComparison.CurrentCultureIgnoreCase));

            if (user == null)
            {
                ViewBag.ResultMessage = "Пользователь не найден!";
            }
            else
            {
                ViewBag.RolesForThisUser = new AccountController().UserManager.GetRoles(user.Id);
            }

            return View("ManageUserRoles");
        }

        /// <summary>
        /// Добавить пользователю роль
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string userName, string roleName)
        {
            ViewBag.Roles =
                _dbContext.Roles.OrderBy(r => r.Name)
                    .ToList()
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                    .ToList();

            if (string.IsNullOrEmpty(roleName))
            {
                ViewBag.ResultMessage = "Не выбрана роль!";
                return View("ManageUserRoles");
            }

            var user =
                _dbContext.Users.FirstOrDefault(
                    u => u.Email.Equals(userName, StringComparison.CurrentCultureIgnoreCase));

            if (user == null)
            {
                ViewBag.ResultMessage = "Пользователь не найден!";
            }
            else
            {
              var result =   new AccountController().UserManager.AddToRole(user.Id, roleName);
                if (result.Succeeded)
                {
                    ViewBag.ResultMessage = "Роль успешно присвоена!";
                }
                else
                {
                    result.Errors.ForEach(x => ViewBag.ResultMessage += x);
                }
            }

            return View("ManageUserRoles");
        }

        /// <summary>
        /// Удалить пользователю роль 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string userName, string roleName)
        {
            ViewBag.Roles =
                _dbContext.Roles.OrderBy(r => r.Name)
                    .ToList()
                    .Select(rr => new SelectListItem {Value = rr.Name.ToString(), Text = rr.Name})
                    .ToList();

            if (string.IsNullOrEmpty(roleName))
            {
                ViewBag.ResultMessage = "Не выбрана роль!";
                return View("ManageUserRoles");
            }

            var account = new AccountController();
            var user =
                _dbContext.Users.FirstOrDefault(
                    u => u.Email.Equals(userName, StringComparison.CurrentCultureIgnoreCase));

            if (user == null)
            {
                ViewBag.ResultMessage = "Пользователь не найден!";
            }
            else if (account.UserManager.IsInRole(user.Id, roleName))
            {
                var result = account.UserManager.RemoveFromRole(user.Id, roleName);
                if (result.Succeeded)
                {
                    ViewBag.ResultMessage = "Роль пользователя успешно удалена!";
                }
                else
                {
                    result.Errors.ForEach(x => ViewBag.ResultMessage += x);
                }
            }
            else
            {
                ViewBag.ResultMessage = "Этот пользователь не принадлежит к выбранной роли.";
            }
            return View("ManageUserRoles");
        }
    }
}
