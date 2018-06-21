using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebAppAuth.Models;

namespace WebAppAuth.WebApi.Controllers
{
    /// <summary>
    /// Базовый контроллер Web Api
    /// </summary>
    public class BaseApiController : ApiController
    {
        //    private ModelFactory _modelFactory;
        //    private ApplicationUserManager _AppUserManager = null;

        //    protected ApplicationUserManager AppUserManager
        //    {
        //        get
        //        {
        //            return _AppUserManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); 
        //        }
        //    }

        //    public BaseApiController()
        //    {
        //    }

        //    protected ModelFactory TheModelFactory
        //    {
        //        get
        //        {
        //            if (_modelFactory == null)
        //            {
        //                _modelFactory = new ModelFactory(this.Request, this.AppUserManager);
        //            }
        //            return _modelFactory;
        //        }
        //    }

        //    protected IHttpActionResult GetErrorResult(IdentityResult result)
        //    {
        //        if (result == null)
        //        {
        //            return InternalServerError();
        //        }

        //        if (!result.Succeeded)
        //        {
        //            if (result.Errors != null)
        //            {
        //                foreach (string error in result.Errors)
        //                {
        //                    ModelState.AddModelError("", error);
        //                }
        //            }

        //            if (ModelState.IsValid)
        //            {
        //                // No ModelState errors are available to send, so just return an empty BadRequest.
        //                return BadRequest();
        //            }

        //            return BadRequest(ModelState);
        //        }

        //        return null;
        //    }
        //}
        //public class ModelFactory
        //{
        //    private UrlHelper _UrlHelper;
        //    private ApplicationUserManager _AppUserManager;

        //    public ModelFactory(HttpRequestMessage request, ApplicationUserManager appUserManager)
        //    {
        //        _UrlHelper = new UrlHelper(request);
        //        _AppUserManager = appUserManager;
        //    }

        //    public UserReturnModel Create(ApplicationUser appUser)
        //    {
        //        return new UserReturnModel
        //        {
        //            Url = _UrlHelper.Link("GetUserById", new { id = appUser.Id }),
        //            Id = appUser.Id,
        //            UserName = appUser.UserName,
        //            Email = appUser.Email,
        //            EmailConfirmed = appUser.EmailConfirmed,
        //            Roles = _AppUserManager.GetRolesAsync(appUser.Id).Result,
        //            Claims = _AppUserManager.GetClaimsAsync(appUser.Id).Result
        //        };
        //    }
        //}

        //public class UserReturnModel
        //{
        //    public string Url { get; set; }
        //    public string Id { get; set; }
        //    public string UserName { get; set; }
        //    public string FullName { get; set; }
        //    public string Email { get; set; }
        //    public bool EmailConfirmed { get; set; }
        //    public int Level { get; set; }
        //    public DateTime JoinDate { get; set; }
        //    public IList<string> Roles { get; set; }
        //    public IList<System.Security.Claims.Claim> Claims { get; set; }
    }
}
