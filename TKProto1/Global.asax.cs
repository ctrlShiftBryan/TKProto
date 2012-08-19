using System;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using TKData;
using Microsoft.Practices.Unity;
using TKData.Interfaces;
using TKMVC.Controllers;
using TKModel;
using TKModel.Interfaces;
using TKProto1;
using TKServices;
using TKServices.Interfaces;

namespace TKMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.([iI][cC][oO]|[gG][iI][fF])(/.*)?" });
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Category", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);


            IUnityContainer container = new UnityContainer();

            container.RegisterType<IResourceService, ResourceService>();
            container.RegisterType<IDatabaseFactory, DatabaseFactory>();
           
            string @namespace = "TKModel";

            var q = from t in Assembly.GetAssembly(typeof(Category)).GetTypes()
                    where t.IsClass && t.Namespace == @namespace
                            && t.GetInterfaces().Contains(typeof(IEntity))
                    select t;

            q.ToList().ForEach(t => RegisterGenericRepository(t, container));
            ControllerBuilder.Current.SetControllerFactory(new UnityControllerFactory(container));
        }

        private void RegisterGenericRepository(Type type, IUnityContainer container)
        {

            Type genericRepositoryInterface = typeof(IRepository<>).MakeGenericType(type);
            Type impl = typeof(Repository<>).MakeGenericType(type);

            container.RegisterType(genericRepositoryInterface, impl);
        }
    }
}