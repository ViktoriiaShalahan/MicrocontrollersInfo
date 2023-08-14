using System;
using System.Collections.Generic;
using System.Text;


namespace MicrocontrollersInfo.Entity
{
    [Serializable]
    public class Microcontrollers : Common.Entities.Entity
    {
        public string brand;
        public int bitRate;
        public HousingType housingType;
        public decimal price;
        public string description;
        public string note;
       

        public Microcontrollers() { }
        public Microcontrollers(
            HousingType housingType,
            string brand,
            int bitRate,
            decimal price,
            string description,
            string note
            )
        {
            this.housingType = housingType;
            this.brand = brand;
            this.bitRate = bitRate;
            this.price = price;
            this.description = description;
            this.note = note;
        }

        public Microcontrollers(string brand):this(brand, null, 1M){}

        public Microcontrollers(string brand, HousingType housingType, decimal price) : this(housingType, brand, 1, price, "", ""){ }

        public Microcontrollers(string brand, HousingType housingType1, string v1, int v2, decimal v3) : this(brand)
        {
        }

        public Microcontrollers(string brand, HousingType housingType1) : this(brand, housingType1, 1M)
        {
        }

        public Microcontrollers(string brand, HousingType housingType1, string v1, decimal v2) : this(brand, housingType1)
        {
        }

        public override string ToString()
        {
            return string.Format("{0} - {1}, Абревіатура: {2}, Розрядність: {3}\n\tОпис: {4}", this.Id, housingType, brand, bitRate, price,  description);
        }
    }
}
