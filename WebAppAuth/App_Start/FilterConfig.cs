using System.Web.Mvc;

namespace WebAppAuth
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAllErrorAttribute());
        }
    }
}
