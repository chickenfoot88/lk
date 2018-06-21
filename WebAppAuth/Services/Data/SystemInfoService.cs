using System.Linq;
using System.Threading.Tasks;
using WebAppAuth.Context;
using WebAppAuth.Entities;
using WebAppAuth.Models;

namespace WebAppAuth.Services.Data
{
    /// <summary>
    /// Сервис системная информация
    /// </summary>
    public class SystemInfoService : ISystemInfoService
    {
        /// <summary>
        /// Получить общую системную информацию по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <returns></returns>
        public async Task<IServiceResult<GeneralSystemInfo>> GetGeneralSystemInfo(long personalAccountId)
        {
            using (var context = new ApplicationDbContext())
            {
                var organizationId = (await context.PersonalAccount
                    .FindAsync(
                        personalAccountId))?.OrganizationId;

                var currentCalculationMonth =
                    context.CalculationMonth
                        .FirstOrDefault(x => x.OrganizationId == organizationId &&
                                             x.IsCurrent)?.CalculationDate;

                return new ServiceResult<GeneralSystemInfo>
                {
                    Content = new GeneralSystemInfo {CurrentCalculationDate = currentCalculationMonth},
                    Success = true
                };
            }
        }
    }
}
