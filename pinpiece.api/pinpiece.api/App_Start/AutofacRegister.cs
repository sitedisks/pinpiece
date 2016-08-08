namespace pinpiece.api.App_Start
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using pinpiece.data.DbContext;
    using pinpiece.data.Interface;
    using pinpiece.service.Services;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;
    public class AutofacRegister
    {
        public static void Run()
        {

            var builder = new ContainerBuilder();
            var config = GlobalConfiguration.Configuration;

            // Autofac
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerRequest();
            builder.RegisterWebApiFilterProvider(config);

            // register mysqldb context
            builder.RegisterType<GeoGoDb>().As<IGeoGoDb>()
                .WithParameter("connectionString", "GeoGoDbContext").InstancePerLifetimeScope();

            // regiter service
            builder.RegisterType<MongoService>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<EntityService>().AsImplementedInterfaces().InstancePerDependency();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}