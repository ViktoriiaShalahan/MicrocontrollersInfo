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
    public class HousingTypeController : Controller
    {
        [Inject]
        public IInfoUnitOfWork UoW { get; set; }

        IEnumerable<HousingType> objects => UoW.HousingTypesRepository.GetAll();
      
        private IEnumerable<HousingTypeBrowsingModel> browsingModelObjects
        {
            get
            {
                return objects.Select(e => (HousingTypeBrowsingModel)e)
                    .OrderBy(e => e.Name);
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
        public ActionResult Selection()
        {
            ViewBag.SelName = objects
             .Select(e => e.name)
             .Distinct()
             .ToSelectList(firstValue: RouteConfig.ALL_VALUES);

            return View(browsingModelObjects);
        }

        public PartialViewResult _SelectData(HousingTypeFilteringInfo info)
        {
            var model = browsingModelObjects;
            if (!string.IsNullOrWhiteSpace(info.SelName))
                model = model.Where(e => e.Name
                    .StartsWith(info.SelName,
                    StringComparison.InvariantCultureIgnoreCase));
            if (info.NumberRowsFrom.HasValue)
                model = model.Where(e => e.NumberRows >= info.NumberRowsFrom.Value);
            if (info.NumberRowsTo.HasValue)
                model = model.Where(e => e.NumberRows <= info.NumberRowsTo.Value);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_TableBody", model);
        }

        public ActionResult BrowseByLetters()
        {
            ViewBag.Letters = new[] { RouteConfig.ALL_PAGES }
                .Concat(objects
                    .Select(e => e.name[0].ToString())
                    .Distinct().OrderBy(e => e));
            return View(browsingModelObjects);
        }

        public PartialViewResult _GetDataByLetter(string selLetter)
        {
            var model = browsingModelObjects;
            if (selLetter != null && selLetter != RouteConfig.ALL_PAGES)
                model = model.Where(e => e.Name[0] == selLetter[0]);
            System.Threading.Thread.Sleep(2000);
            return PartialView("_BrowseData", model);
        }

        public ViewResult Browse()
        {
            return View(browsingModelObjects);
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