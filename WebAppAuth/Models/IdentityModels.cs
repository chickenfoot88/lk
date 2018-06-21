using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebAppAuth.Context;

namespace WebAppAuth.Models
{
    public class ApplicationUser : IdentityUser<int, UserLogin, UserRole, UserClaim>
    {
        /// <summary>
        /// Дата создания
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Создание
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class UserRole : IdentityUserRole<int>
    {
    }

    public class UserClaim : IdentityUserClaim<int>
    {
    }

    public class UserLogin : IdentityUserLogin<int>
    {
    }
    public class Role : IdentityRole<int, UserRole>
    {
        public Role() { }
        public Role(string name) { Name = name; }
    }

    public class UserStore : UserStore<ApplicationUser, Role, int,
        UserLogin, UserRole, UserClaim>
    {
        public UserStore(ApplicationDbContext context) : base(context)
        {
        }
    }

    public class RoleStore : RoleStore<Role, int, UserRole>
    {
        public RoleStore(ApplicationDbContext context) : base(context)
        {
        }
    }
}