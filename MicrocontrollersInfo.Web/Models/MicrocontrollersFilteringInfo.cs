using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MicrocontrollersInfo.Web.Models
{
    public class MicrocontrollersFilteringInfo
    {
        public string SelBrand { get; set; }
        public string SelhousingTypeName { get; set; }
        public decimal?PriceFrom { get; set; }
        public decimal? PriceTo { get; set; }
    }
}