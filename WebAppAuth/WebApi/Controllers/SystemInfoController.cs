using System.Threading.Tasks;
using System.Web.Http;
using WebAppAuth.Services.Data;
using WebAppAuth.WebApi.Models.Requests;

namespace WebAppAuth.WebApi.Controllers
{
    /// <summary>
    /// Данные о системе
    /// </summary>
    public class SystemInfoController : BaseApiController
    {
        // POST: api/SystemInfo/GeneralInfo
        public async Task<IHttpActionResult> GeneralInfo(CommonRequest request)
        {
            var data = await OhConfigurator.Container.Resolve<ISystemInfoService>()
                .GetGeneralSystemInfo(request.Id);

            if (data == null || !data.Success || !data.Content.CurrentCalculationDate.HasValue)
                return NotFound();
            
            return Ok(new
            {
                CalculationDate = data.Content.CurrentCalculationDate.Value,
                CalculationMonth = data.Content.CurrentCalculationDate.Value.Month,
                CalculationYear = data.Content.CurrentCalculationDate.Value.Year
            });
        }
   }
}