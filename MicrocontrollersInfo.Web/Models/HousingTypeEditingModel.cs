using MicrocontrollersInfo.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MicrocontrollersInfo.Web.Models
{
    public class HousingTypeEditingModel{
        public int Id { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Назва\'")]
        [StringLength(30, MinimumLength = 4,
             ErrorMessage = "Назва типу корпуса "
             + "повинна містити від 4 до 30 символів")]
        public string Name { get; set; }

        [Display(Name = "Абревіатура")]
        [RegularExpression(@"^[A-Za-z]{1,6}$", ErrorMessage =
            "Абревіатура " + "повинно бути в межах від 1 до 6 літер")]
        [Required(ErrorMessage =
             "Потрібно заповнити поле \'Абревіатура\'")]
        public string Abbreviation { get; set; }

        [Display(Name = "Кількість рядів ніжок")]
        [Range(1, 240, ErrorMessage =
            "Кількість рядів ніжок " + "повинно бути в межах від 1 до 240")]
        public int? NumberRows { get; set; }


        [Display(Name = "Примітка")]
        [DataType(DataType.MultilineText)]
        [MaxLength(1023)]
        public string Note { get; set; }

        [Display(Name = "Опис")]
        [DataType(DataType.MultilineText)]
        [MaxLength(65535)]
        public string Description { get; set; }

        public static explicit operator HousingTypeEditingModel(HousingType obj)
        {
            return new HousingTypeEditingModel()
            {
                Id = obj.Id,
                Name = obj.name,
                Abbreviation = obj.abbreviation,
                NumberRows = obj.numberRows,
                Note = obj.note,
                Description = obj.description

            };
        }
    }
}