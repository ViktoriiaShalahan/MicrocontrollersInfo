using MicrocontrollersInfo.Entity;
using MicrocontrollersInfo.Repositories.Interfaces;
using MicrocontrollersInfo.Web.Models;
using MicrocontrollersInfo.Web.Models.Extensions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicrocontrollersInfo.Web.Controllers
{
    public class MicrocontrollersController : Controller

    {
        [Inject]
        public IInfoUnitOfWork UoW { get; set; }

        IEnumerable<Microcontrollers> objects => UoW.MicrocontrollersRepository.GetAll();
        // GET: Microcontrollers
        private IEnumerable<MicrocontrollersBrowsingModel> browsingModelObjects
        {
            get
            {
                return objects.Select(e => (MicrocontrollersBrowsingModel)e)
                    .OrderBy(e => e.Brand);
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Робота з даними
        public ViewResult ObjectsInfo()
        {
            return View(objects);
        }


        public PartialViewResult _DescriptiveInfo(int id)
        {
            var obj = objects.First(e => e.Id == id);
            string[] model = null;
            if (!string.IsNullOrWhiteSpace(obj.description))
            {
                string s = "Опис\n" + obj.description;
                model = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return PartialView(model);
        }
        #endregion

        #region Навігація за категоріями
        public ViewResult MicrocontrollersByHousingTypeInfo(
             string categoryName = RouteConfig.ALL_VALUES)
        {
            IEnumerable<Microcontrollers> models = objects.OrderBy(e => e.brand);
            if (!string.IsNullOrEmpty(categoryName) &&
                categoryName != RouteConfig.ALL_VALUES)
            {
                models = models
                    .Where(e => e.housingType?.name == categoryName);
            }
            ViewBag.SelectedCategoryName = categoryName;
            return View(models);
        }
        #endregion

        public ActionResult Selection()
        {
            ViewBag.SelhousingTypeName = objects
               .Select(e => e.housingType.name)
               .Distinct()
               .ToSelectList(firstValue: RouteConfig.ALL_VALUES);
            return View(browsingModelObjects);
        }

        public PartialViewResult _SelectData(MicrocontrollersFilteringInfo info)
        {
            var model = browsingModelObjects;
            if (!string.IsNullOrWhiteSpace(info.SelBrand))
                model = model.Where(e => e.Brand
                    .StartsWith(info.SelBrand,
                    StringComparison.InvariantCultureIgnoreCase));
            if (info.SelhousingTypeName != null
                && info.SelhousingTypeName != RouteConfig.ALL_VALUES)
                model = model.Where(
                    e => e.housingTypeName == info.SelhousingTypeName);
            if (info.PriceFrom.HasValue)
                model = model.Where(e => e.Price >= info.PriceFrom.Value);
            if (info.PriceTo.HasValue)
                model = model.Where(e => e.Price <= info.PriceTo.Value);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_TableBody", model);
        }

        public ActionResult BrowseByLetters()
        {
            ViewBag.Letters = new[] { RouteConfig.ALL_PAGES }
                .Concat(objects
                    .Select(e => e.brand[0].ToString())
                    .Distinct().OrderBy(e => e));
            return View(browsingModelObjects);
        }

        public PartialViewResult _GetDataByLetter(string selLetter)
        {
            var model = browsingModelObjects;
            if (selLetter != null && selLetter != RouteConfig.ALL_PAGES)
                model = model.Where(e => e.Brand[0] == selLetter[0]);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_BrowseData", model);
        }

        public ViewResult Browse()
        {
            return View(browsingModelObjects);
        }
        public int ItemsPerPage { get; set; }
        public ViewResult InfoWithPaging(string pageKey = "..", int pageNumber = 0)
        {
            IEnumerable<Microcontrollers> model = objects; //.OrderBy(e => e.Name)
            if (!string.IsNullOrEmpty(pageKey) && pageKey != "..")
            {
                model = model.Where(e => e.brand[0].ToString() == pageKey);
            }
            if (pageNumber != 0)
            {
                model = model
                    .Skip((pageNumber - 1) * ItemsPerPage)
                    .Take(ItemsPerPage);
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult JsonIdInfo(int id)
        {
            var info = GetInfo(id);
            return Json(new { Id = id, Info = info }); //, JsonRequestBehavior.AllowGet
        }

        string[] GetInfo(int id)
        {
            var obj = objects.First(e => e.Id == id);
            string s = null;
            if (!string.IsNullOrWhiteSpace(obj.note))
            {
                s += "Примітка: " + obj.note + "\n";
            }
            if (!string.IsNullOrWhiteSpace(obj.description))
            {
                s += "Опис\n" + obj.description;
            }
            string[] info = null;
            if (s != null)
            {
                info = s.Split(new[] { '\n' },
                    StringSplitOptions.RemoveEmptyEntries);
            }
            return info;
        }

    }
}