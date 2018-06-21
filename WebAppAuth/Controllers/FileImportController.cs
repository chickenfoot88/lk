using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using OhDataManager.Enums;
using WebAppAuth.Context;
using WebAppAuth.Services.Import;

namespace WebAppAuth.Controllers
{
    /// <summary>
    /// Импорт данных
    /// </summary>
    [Authorize(Roles = AppConstants.RoleEmployee + ", " + AppConstants.RoleDirector)]
    public class FileImportController : BaseController
    {
        /// <summary>
        /// Импорт файла
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> StartImport(HttpPostedFileBase inputFile)
        {
            if (inputFile != null && inputFile.ContentLength > 0)
            {
                var fileImportType = Request["FileImportType"];

                if (string.IsNullOrEmpty(fileImportType))
                    throw new ArgumentException("Не задан тип загрузки");

                var importer =
                    OhConfigurator.Container.Resolve<BaseFileImport>(fileImportType);
                await importer.Run(inputFile, User);
            }
            var referrer = Request.UrlReferrer;

            if (referrer != null)
                return Redirect(referrer.ToString());

            throw new HttpUnhandledException("Не задан uri получателя");
        }


        /// <summary>
        /// Получение списка загруженных файлов
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ImportedFileList()
        {
            var userIdentityid = User.Identity.GetUserId<int>();

            using (var context = new ApplicationDbContext())
            {
                var data =
                    context
                        .ImportedFiles
                        .Include(x => x.ExchangeFile)
                        .Where(x => x.ExchangeFile.UserIdentityId == userIdentityid)

                        .OrderByDescending(x => x.ExchangeFile.StartTime)
                        .ToList();

                ViewData["FileImportType"] = new[]
                {
                    new SelectListItem {Text = EImportType.Account.GetDisplayName(), Selected = true}
                };
                return View("_FileRegisterPartial", data);
            }
        }
    }
}