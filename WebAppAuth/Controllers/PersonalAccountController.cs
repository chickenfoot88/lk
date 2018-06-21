using System.Data.Entity;
using System.Threading.Tasks;

namespace WebAppAuth.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Context;

    /// <summary>
    /// Лицевой счет
    /// </summary>
    [Authorize(
        Roles = AppConstants.RoleAbonent + ", "
                + AppConstants.RoleDirector + ", "
                + AppConstants.RoleAdministrator + ", "
                + AppConstants.RoleWorker + ", "
                + AppConstants.RoleEmployee)]
    public class PersonalAccountController : BaseController
    {
        /// <summary>
        /// Получение списка ЛС по идентификатору дома
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetByHouseId(long houseId)
        {
            using (var context = new ApplicationDbContext())
            {
                ViewBag.PersonalAccountId = (await context.PersonalAccount
                    .Where(x => x.HouseId == houseId)
                    .Select(x =>
                        new
                        {
                            x.Id,
                            x.ApartmentOwnerFullName,
                            x.ApartmentAddress,
                            x.ApartmentNumber
                        })
                    .ToListAsync())
                    .OrderBy(x => x.ApartmentNumber, new SemiNumericComparer())
                    .Select(x =>
                        new SelectListItem
                        {
                            Text = $"{x.ApartmentAddress} ({x.ApartmentOwnerFullName})",
                            Value = x.Id.ToString()
                        });

                return PartialView();
            }
        }

        /// <summary>
        /// Получение номера телефона абонента по идентификатору ЛС
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetContactPhoneNumber(long personalAccountId)
        {
            using (var context = new ApplicationDbContext())
            {
                var сontactPhoneNumber = (await context.Abonent
                    .FirstOrDefaultAsync(x => x.PersonalAccountId == personalAccountId))
                    ?.MobilePhoneNumber;

                if (string.IsNullOrEmpty(сontactPhoneNumber))

                    сontactPhoneNumber = (await context.AbonentClaim
                        .FirstOrDefaultAsync(x => x.PersonalAccountId == personalAccountId
                                             && !string.IsNullOrEmpty(x.ContactPhoneNumber)))
                        ?.ContactPhoneNumber;


                ViewBag.ContactPhoneNumber = сontactPhoneNumber;

                return PartialView();
            }
        }
    }
}
