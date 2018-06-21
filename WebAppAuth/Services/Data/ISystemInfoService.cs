using System.Threading.Tasks;
using WebAppAuth.Entities;
using WebAppAuth.Models;

namespace WebAppAuth.Services.Data
{
    /// <summary>
    /// Интерфейс системная информация
    /// </summary>
    interface ISystemInfoService
    {
        /// <summary>
        /// Получить общую системную информацию по лицевому счету
        /// </summary>
        /// <param name="personalAccountId"></param>
        /// <returns></returns>
        Task<IServiceResult<GeneralSystemInfo>> GetGeneralSystemInfo(long personalAccountId);
    }
}
