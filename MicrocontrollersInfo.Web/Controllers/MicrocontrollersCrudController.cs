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
    public class MicrocontrollersCrudController : Controller
    {
        [Inject]
        public IInfoUnitOfWork UoW { get; set; }
        private IRepository<Microcontrollers> repository => UoW.MicrocontrollersRepository;


        // GET: CountriesCrud
        public ActionResult Index()
        {
            IEnumerable<MicrocontrollersBrowsingModel> browsingModelObjects =
                repository.GetAll()
                .Select(e => (MicrocontrollersBrowsingModel)e).OrderBy(e => e.Brand);
            //return View();
            return View(browsingModelObjects);
        }
        public ActionResult Details(int id)
        {
            MicrocontrollersEditingModel model =
                (MicrocontrollersEditingModel)repository.GetById(id);
            return View(model);
        }

        public ViewResult Create()
        {
            ViewBag.housingTypeName = UoW.ToHousingTypeSelectList();
            return View(new MicrocontrollersEditingModel());
        }

        [HttpPost]
        public ActionResult Create(MicrocontrollersEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.housingTypeName = UoW.ToHousingTypeSelectList();
                return View(model);
            }
            UoW.AddMicrocontrollers(model);
            UoW.Save();
            TempData["message"] = string.Format(
                "Дані мікроконтролери \"{0}\" збережено", model.Brand);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            MicrocontrollersEditingModel model =
                (MicrocontrollersEditingModel)repository.GetById(id);
            ViewBag.housingTypeName =
                UoW.ToHousingTypeSelectList(model.housingTypeName);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(MicrocontrollersEditingModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.housingTypeName =
                    UoW.ToHousingTypeSelectList(model.housingTypeName);
                return View(model);
            }
            UoW.UpdateMicrocontrollers(model);
            UoW.Save();
            TempData["message"] = string.Format(
                "Зміни даних мікроконтролери \"{0}\" збережено", model.Brand);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            MicrocontrollersEditingModel model =
                (MicrocontrollersEditingModel)repository.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(MicrocontrollersEditingModel model)
        {
            //repository.Delete(repository.GetById(model.Id));
            Microcontrollers obj = repository.GetById(model.Id);
            repository.Delete(obj);
            UoW.Save();
            TempData["message"] = string.Format(
                "Дані мікроконтролери \"{0}\" видалено", obj.brand);
            return RedirectToAction("Index");
        }

    }
}