using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using BlogDemo2.App_Start;
using BlogDemo2.Core;
using BlogDemo2.Core.Objects;
using BlogDemo2.Providers;
using Ninject;
using Ninject.Web.Common;

namespace BlogDemo2
{
    public class MvcApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Load(new RepositoryModule());
            kernel.Bind<IBlogRepository>().To<BlogRepository>();
            kernel.Bind<IAuthProvider>().To<AuthProvider>();

            return kernel;
        }

        protected override void OnApplicationStarted()
        {
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Post), new PostModelBinder(Kernel));
            base.OnApplicationStarted();
        }
    }
}