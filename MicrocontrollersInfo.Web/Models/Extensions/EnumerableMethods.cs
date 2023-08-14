using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MicrocontrollersInfo.Web.Models.Extensions
{
    public static class EnumerableMethods
    {

        public static List<SelectListItem> ToSelectList(
            this IEnumerable<string> values, string selectedValue = "", string firstValue = null)
        {
            List<string> valuesList = new List<string>();
            if (firstValue != null)
            {
                valuesList.Add(firstValue);
            }
            valuesList.AddRange(values);
            List<SelectListItem> itemsList = new List<SelectListItem>();
            foreach (string e in valuesList)
            {
                itemsList.Add(new SelectListItem
                {
                    Text = e,
                    Value = e,
                    Selected = e == selectedValue
                });
            }
            return itemsList;
        }



    }
}