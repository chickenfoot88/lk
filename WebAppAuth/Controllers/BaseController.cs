using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OhDataManager.Entities.System;
using WebAppAuth.Context;
using WebAppAuth.Entities;

namespace WebAppAuth.Controllers
{
    /// <summary>
    /// Базовый контроллер
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// Получение текущего сотрудника
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        protected async Task<Employee> GetCurrentEmployee(ApplicationDbContext dbContext)
        {
            var userIdentityid = User.Identity.GetUserId<int>();
            var employee =
                dbContext.Employee
                    .Include(x => x.Organization)
                    .Where(x => x.ApplicationUser.Id == userIdentityid);
            if (!employee.Any()) return null;
            if (employee.Count() > 1) throw new Exception("К пользователю прикреплен более 1 сорудника");
            return await employee.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Получение текущего абонента
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        protected async Task<Abonent> GetCurrentAbonent(ApplicationDbContext dbContext)
        {
            var userIdentityid = User.Identity.GetUserId<int>();
            var accounts = dbContext.Abonent
                .Include(x => x.Organization)
                .Where(x => x.ApplicationUser.Id == userIdentityid);
            if (!accounts.Any()) return null;
            if (accounts.Count() > 1) throw new Exception("К пользователю прикреплен более 1 ЛС");
            return await accounts.SingleOrDefaultAsync();
        }

        /// <summary>
        /// Получение организации пользователя
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        protected async Task<Organization> GetCurrentOrganization(ApplicationDbContext dbContext)
        {
            var employee = await GetCurrentEmployee(dbContext);
            if (employee != null)
            {
                return employee.Organization;
            }
            
            var abonent = await GetCurrentAbonent(dbContext);
            return abonent?.Organization;
        }
    }
}