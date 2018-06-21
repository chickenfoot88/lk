using WebAppAuth.Context;
using WebAppAuth.Services.FileManager;

namespace WebAppAuth.Controllers
{
    using System.Web.Mvc;

    /// <summary>
    /// Скачивание для пользователей
    /// </summary>
    [Authorize]
    public class PublicDownloadController : BaseController
    {
        /// <summary>
        /// Главная страница
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        public FileResult Index(long id)
        {
            using (var context = new ApplicationDbContext())
            {
                var file =  context.ExchangeFiles.Find(id);
                var bytes =  OhConfigurator.Container.Resolve<IFileStorageManager>().GetFileBytes(id).Result;
                
                return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName);
            }
        }
    }
}
