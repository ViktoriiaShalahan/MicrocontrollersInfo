using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using MicrocontrollersInfo.Repositories.Interfaces;
using MicrocontrollersInfo.Entity.FileIO.Interfaces;
using MicrocontrollersInfo.Entity;
using MicrocontrollersInfo.Repositories;
using MicrocontrollersInfo.Entity.FileIO;

namespace MicrocontrollersInfo.Web.Infrastructure
{
    public class BindingModule : NinjectModule
    {
        public override void Load()
        {
            //throw new NotImplementedException();
            string virtualPath = "~" + HttpContext.Current
               .Application["dataFilesPath"] as string;
            string path = HostingEnvironment.MapPath(virtualPath);
            Bind<IFileIoController>().To<BinarySerializationAdapter>().InSingletonScope();
            Bind<DataContext>().ToSelf().InSingletonScope()
                .WithPropertyValue("Directory", path);
            Bind<IInfoUnitOfWork>().To<FileBasedUnitOfWork>().InSingletonScope();
        }
    }
}