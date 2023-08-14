using MicrocontrollersInfo.Entity;
using MicrocontrollersInfo.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicrocontrollersInfo.Web.Models.Extensions
{
    public static class InfoUoWMethods
    {
        public static List<SelectListItem> ToHousingTypeSelectList(
                  this IInfoUnitOfWork uow, string selectedValue = "")
        {
            return uow.HousingTypesRepository.GetAll().Select(e => e.name)
                .ToSelectList(selectedValue);
        }
       

        public static void UpdateMicrocontrollers(this IInfoUnitOfWork uow,
            MicrocontrollersEditingModel model)
        {
            Microcontrollers microcontrollers = uow.MicrocontrollersRepository.GetById(model.Id);
            UpdateMicrocontrollers(uow, ref microcontrollers, model);


            
        }

        public static void UpdateHousingType(this IInfoUnitOfWork uow,
           HousingTypeEditingModel model)
        {
            HousingType housingType = uow.HousingTypesRepository.GetById(model.Id);
            UpdateHousingType(uow, ref housingType, model);



        }
        private static void UpdateMicrocontrollers(IInfoUnitOfWork uow,
                ref Microcontrollers microcontrollers, MicrocontrollersEditingModel model)
        {
          
            microcontrollers.brand = model.Brand;
            microcontrollers.housingType = uow.HousingTypesRepository.GetAll()
                .First(e => e.name == model.housingTypeName);
            microcontrollers.bitRate = model.BitRate;
            microcontrollers.price = model.Price.GetValueOrDefault();
            microcontrollers.note = model.Note;
            microcontrollers.description = model.Description;
        }

        private static void UpdateHousingType(IInfoUnitOfWork uow,
               ref HousingType housingType, HousingTypeEditingModel model)
        {

            housingType.name = model.Name;
            housingType.abbreviation = model.Abbreviation;
            housingType.numberRows = model.NumberRows;
            housingType.note = model.Note;
            housingType.description = model.Description;
        }

        public static void AddMicrocontrollers(this IInfoUnitOfWork uow,
            MicrocontrollersEditingModel model)
        {
            Microcontrollers microcontrollers = new Microcontrollers();
            UpdateMicrocontrollers(uow, ref microcontrollers, model);
            uow.MicrocontrollersRepository.Add(microcontrollers);
        }

        public static void AddHousingType(this IInfoUnitOfWork uow,
            HousingTypeEditingModel model)
        {
           HousingType housingType = new HousingType();
            UpdateHousingType(uow, ref housingType, model);
            uow.HousingTypesRepository.Add(housingType);
        }

    }

    
}