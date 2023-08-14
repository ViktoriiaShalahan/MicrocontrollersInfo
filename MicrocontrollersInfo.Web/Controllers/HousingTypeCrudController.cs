using Common.Repositories.Interfaces;
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
    public class HousingTypeCrudController : Controller
    {
        [Inject]
        public IInfoUnitOfWork UoW { get; set; }
        private IRepository<HousingType> repository => UoW.HousingTypesRepository;


        // GET: CountriesCrud
        public ActionResult Index()
        {
            IEnumerable<HousingTypeBrowsingModel> browsingModelObjects =
                repository.GetAll()
                .Select(e => (HousingTypeBrowsingModel)e).OrderBy(e => e.Name);
            //return View();
            return View(browsingModelObjects);
        }

        public ActionResult Details(int id)
        {
            HousingTypeEditingModel model =
                (HousingTypeEditingModel)repository.GetById(id);
            return View(model);
        }

        public ViewResult Create()
        {
            
            return View(new HousingTypeEditingModel());
        }

        [HttpPost]
        public ActionResult Create(HousingTypeEditingModel model)
        {
            if (!ModelState.IsValid)
            {
               
               
                return View(model);
            }
            UoW.AddHousingType(model);
            UoW.Save();
            TempData["message"] = string.Format(
                "Дані типу корпусу \"{0}\" збережено", model.Name);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            HousingTypeEditingModel model =
                (HousingTypeEditingModel)repository.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(HousingTypeEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                
                return View(model);
            }
            UoW.UpdateHousingType(model);
            UoW.Save();
            TempData["message"] = string.Format(
                "Зміни даних типу корпусу \"{0}\" збережено", model.Name);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HousingTypeEditingModel model =
                (HousingTypeEditingModel)repository.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(HousingTypeEditingModel model)
        {
            //repository.Delete(repository.GetById(model.Id));
            HousingType obj = repository.GetById(model.Id);
            repository.Delete(obj);
            UoW.Save();
            TempData["message"] = string.Format(
                "Дані типу корпусу \"{0}\" видалено", obj.name);
            return RedirectToAction("Index");
        }
    }
}