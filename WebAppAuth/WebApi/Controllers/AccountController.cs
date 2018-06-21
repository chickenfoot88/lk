using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebAppAuth.Context;
using WebAppAuth.Entities;
using WebAppAuth.Models;
using WebAppAuth.Services.Data;
using WebAppAuth.WebApi.Models.Requests;

namespace WebAppAuth.WebApi.Controllers
{
    /// <summary>
    /// Аккаунт
    /// </summary>
    public class AccountController : BaseApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
            : this(new ApplicationUserManager(new UserStore(new ApplicationDbContext())))
        {
        }

        public AccountController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get { return _signInManager ?? Request.GetOwinContext().Get<ApplicationSignInManager>(); }
            private set { _signInManager = value; }
        }
        
        private IAuthenticationManager AuthenticationManager => Request.GetOwinContext().Authentication;

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }


        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model == null)
                return BadRequest();

            var result =
                    SignInManager.PasswordSignIn(model.Email, model.Password, model.RememberMe,
                        shouldLockout: false);

            if (result == SignInStatus.Success)
            {
                var userIdentityid = User.Identity.GetUserId<int>();
                
                using (var context = new ApplicationDbContext())
                {
                    var abonent = context.Abonent
                        .Where(x => x.ApplicationUserId == userIdentityid)
                        .Include(x=>x.PersonalAccount)
                        .FirstOrDefault();

                    return Ok(new
                    {
                        abonentId = abonent?.Id,
                        personalAccountId = abonent?.PersonalAccountId,
                        abonent?.ApartmentFullAddress,
                        abonent?.PaymentCode,
                        abonent?.PersonalAccount?.ApartmentOwnerFullName
                    });
                }
            }
            return NotFound();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        
        public IHttpActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return Ok();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {UserName = model.Email, Email = model.Email};
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    if (model.Email == "admin_xWVCGLwVz@xZZ5DXBhT.ru")
                    {
                        UserManager.AddToRole(user.Id, AppConstants.RoleAdministrator);
                    }
                    if (model.Email == "employee_4cZQ9xtGr@SnnsS890.ru")
                    {
                        UserManager.AddToRole(user.Id, AppConstants.RoleEmployee);
                    }

                    UserManager.AddToRole(user.Id, AppConstants.RoleAbonent);

                    return Ok();
                }

                return BadRequest(string.Join(";", result.Errors));
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// Прикрепление лицевого счета
        /// </summary>
        /// <param name="personalAccountRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IHttpActionResult> AddPersonalAccount(PersonalAccountRequest personalAccountRequest)
        {
            //ФИО
            var apartmentOwnerFullName = personalAccountRequest.ApartmentOwnerFullName;
            //номер квартиры
            var apartmentNumber = personalAccountRequest.ApartmentNumber;

            var personalAccountResult = await OhConfigurator.Container.Resolve<IAbonentService>()
                .GetPersonalAccount(personalAccountRequest.PaymentCode, apartmentOwnerFullName, apartmentNumber);

            if (!personalAccountResult.Success)
            {
                return NotFound();
            }

            var userIdentityid = User.Identity.GetUserId<int>();
            using (var context = new ApplicationDbContext())
            {
                var abonent = new Abonent
                {
                    OrganizationId = personalAccountResult.Content.OrganizationId,
                    ApartmentFullAddress = personalAccountResult.Content.ApartmentFullAddress,
                    ApplicationUserId = userIdentityid,
                    PaymentCode = personalAccountResult.Content.PaymentCode,
                    PersonalAccountId = personalAccountResult.Content.Id
                };
                context.Abonent.Add(abonent);
                await context.SaveChangesAsync();

                return Ok(abonent.Id);
            }
        }
    }
}