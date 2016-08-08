namespace pinpiece.api
{
    using pinpiece.api.App_Start;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var config = GlobalConfiguration.Configuration;
            SwaggerConfig.Register(config);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutofacRegister.Run();
        }
    }
}
