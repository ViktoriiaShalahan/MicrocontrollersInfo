using MicrocontrollersInfo.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicrocontrollersInfo.Web.Models
{
    public class HousingTypeBrowsingModel {

        public int Id { get; set; }

        [Display(Name = "Назва")]
        public string Name { get; set; }

        [Display(Name = "Абревіатура")]
        public string Abbreviation { get; set; }

        [Display(Name = "Кількість рядів ніжок")]
        public int? NumberRows { get; set; }

        [ScaffoldColumn(false)]
        public bool HasInfo { get; set; }


        public static explicit operator HousingTypeBrowsingModel(HousingType obj)
        {
            return new HousingTypeBrowsingModel()
            {
                Id = obj.Id,
                Name = obj.name,
                Abbreviation = obj.abbreviation,
                NumberRows = obj.numberRows,

                HasInfo = !string.IsNullOrWhiteSpace(obj.note)
                    || !string.IsNullOrWhiteSpace(obj.description)
            };
        }
    }
}