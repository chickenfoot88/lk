using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OhDataManager.Enums;
using WebAppAuth.Context;
using WebAppAuth.Services.Export;

namespace WebAppAuth.Controllers
{
    /// <summary>
    /// Экспорт данных
    /// </summary>
    [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleDirector)]
    public class FileExportController : BaseController
    {
        /// <summary>
        /// Экспорт файла
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> StartExport()
        {
            var fileExportType = Request["FileExportType"];

            if (string.IsNullOrEmpty(fileExportType))
                throw new ArgumentException("Не задан тип выгрузки");

            var exporter =
                OhConfigurator.Container.Resolve<BaseFileExport>(fileExportType);
            await exporter.Run(User);

            var referrer = Request.UrlReferrer;

            if (referrer != null)
                return Redirect(referrer.ToString());

            throw new HttpUnhandledException("Не задан uri получателя");
        }


        /// <summary>
        /// Получение списка выгруженных файлов
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult UnloadedFileList()
        {
            var userIdentityid = User.Identity.GetUserId<int>();

            using (var context = new ApplicationDbContext())
            {
                var data =
                    context.ExportedFiles
                        .Include(x => x.ExchangeFile)
                        //.Where(x => x.ExchangeFile.UserIdentityId == userIdentityid)
                        .OrderByDescending(x => x.ExchangeFile.StartTime)
                        .ToList();
                
                ViewData["FileExportType"] = new[]
                {
                    new SelectListItem {Text = EExportType.MeterReading.GetDisplayName(),Selected = true}
                };
                return View("_UnloadedFileRegisterPartial", data);
            }
        }
    }
}