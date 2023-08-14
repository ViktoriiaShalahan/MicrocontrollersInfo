#define _TEST_
//#undef _TEST_



using MicrocontrollersInfo.Entity;
using MicrocontrollersInfo.Web.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MicrocontrollersInfo.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["dataFilesPath"] = "/files";

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var bindingModule = new BindingModule();
            var kernel = new StandardKernel(bindingModule);
            kernel.Unbind<ModelValidatorProvider>();
            var resolver = new Ninject.Web.Mvc.NinjectDependencyResolver(kernel);
            DependencyResolver.SetResolver(resolver);

            LoadData(kernel);
        }
        private void LoadData(StandardKernel kernel)
        {
            //throw new NotImplementedException();
            DataContext dataContext = kernel.Get<DataContext>();
            if (File.Exists(dataContext.FilePath))
            {
                dataContext.Load();
            }
            else
            {
#if _TEST_
                dataContext.CreateTestingData();
                dataContext.Save();
#endif
            }
        }
    }
}
