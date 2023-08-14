using MicrocontrollersInfo.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicrocontrollersInfo.Web.Models
{
    public class MicrocontrollersBrowsingModel
    {
        public int Id { get; set; }

        [Display(Name = "Марка")]
        public string Brand{ get; set; }

        [Display(Name = "Тип корпусу")]
        public  string housingTypeName{ get; set; }

        [Display(Name = "Розрядність")]
        public int BitRate { get; set; }

        [Display(Name = "Ціна, грн.")]
        public decimal? Price{ get; set; }

        [ScaffoldColumn(false)]
        public bool HasInfo { get; set; }


        public static explicit operator MicrocontrollersBrowsingModel(Microcontrollers obj)
        {
            return new MicrocontrollersBrowsingModel()
            {
                Id = obj.Id,
                Brand = obj.brand,
                housingTypeName = obj.housingType?.name,
                BitRate = obj.bitRate,
                Price = obj.price,
                HasInfo = !string.IsNullOrWhiteSpace(obj.note)
                    || !string.IsNullOrWhiteSpace(obj.description)
            };
        }
    }
}