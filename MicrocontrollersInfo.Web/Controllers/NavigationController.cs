using MicrocontrollersInfo.Entity;
using MicrocontrollersInfo.Repositories.Interfaces;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicrocontrollersInfo.Web.Controllers
{
    public class NavigationController : Controller
    {
        [Inject]
        public IInfoUnitOfWork UoW { get; set; }

        private IEnumerable<Microcontrollers> microcontrollers => UoW.MicrocontrollersRepository.GetAll();
        

        ///GET: Navigation
        //public ActionResult Index()
        //{
        //    return View();
        //}

        [ChildActionOnly]
        public PartialViewResult UsedHousingTypeMenu(
                string categoryName = RouteConfig.ALL_VALUES)
        {
            ViewBag.SelectedCategoryName = categoryName;
            List<string> categoryNames = new List<string>();
            categoryNames.Add(RouteConfig.ALL_VALUES);
            categoryNames.AddRange(microcontrollers
                    .Select(e => e.housingType.name)
                    .Distinct().OrderBy(e => e));
            return PartialView(categoryNames);
        }

        
    }
}