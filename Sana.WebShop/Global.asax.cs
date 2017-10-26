using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Ninject;
using Ninject.Web.Common.WebHost;
using Sana.WebShop.App_Start;
using Sana.WebShop.Infrastructure.Core;
using Sana.WebShop.Infrastructure.Data;

namespace Sana.WebShop
{
    public class MvcApplication : NinjectHttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }
        
        private void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IStorage>()
                    .To<MemoryStorage>()
                    .InSingletonScope()
                    .Named("Memory");

            kernel.Bind<IStorage>()
                .To<XmlStorage>()
                .InSingletonScope()
                .Named("Xml");

            kernel.Bind<ProductRepository>()
                .ToSelf()
                .InSingletonScope();

            kernel.Bind<IMapper>()
                .ToMethod(x => MapperConfig.Build())
                .InSingletonScope();
        }

        protected override void OnApplicationStarted()
        {
            base.OnApplicationStarted();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
