using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InfoWeather
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            NLogConfig.NLogStartConfig();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
