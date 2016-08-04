using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;

namespace pinpiece.api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            SwaggerConfig.Register(config);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
