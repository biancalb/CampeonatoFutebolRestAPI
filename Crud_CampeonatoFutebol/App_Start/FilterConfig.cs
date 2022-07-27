using System.Web;
using System.Web.Mvc;

namespace Crud_CampeonatoFutebol
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
