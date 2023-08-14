using MicrocontrollersInfo.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicrocontrollersInfo.Web.Models
{
    public class MicrocontrollersEditingModel
    {
        public int Id { get; set; }

        [Display(Name = "Марка")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Марка\'")]
        [StringLength(50, MinimumLength = 2,
             ErrorMessage = "Марка мікроконтролера "
             + "повинна містити від 2 до 50 символів")]
        public string Brand { get; set; }

        [Display(Name = "Тип корпусу")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Тип корпусу\'")]
        public string housingTypeName { get; set; }

        [Display(Name = "Розрядність")]
        [Range(1,512, ErrorMessage =
            "Розрядність " + "повинно бути в межах від 1 до 512")]
        public int BitRate{ get; set; }

        [Display(Name = "Ціна ")]
        [Range(2, 14100, ErrorMessage = "Значенння ціни "
            + "повинно бути в межах від 2 до 14100 грн.")]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        [Display(Name = "Примітка")]
        [DataType(DataType.MultilineText)]
        public string Note { get; set; }

        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public static explicit operator MicrocontrollersEditingModel(Microcontrollers obj)
        {
            return new MicrocontrollersEditingModel()
            {
                Id = obj.Id,
                Brand = obj.brand,
                housingTypeName = obj.housingType?.name,
                BitRate = obj.bitRate,
                Price = obj.price,
                Note = obj.note,
                Description = obj.description

            };
        }
    }
}