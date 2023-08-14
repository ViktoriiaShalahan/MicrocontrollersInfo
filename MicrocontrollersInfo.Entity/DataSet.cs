using System;
using System.Collections.Generic;

namespace MicrocontrollersInfo.Entity
{
    [Serializable]
    public class DataSet
    {
        public List<HousingType> housingTypes { get; private set; }
        public List<Microcontrollers> microcontrollers { get; private set; }
       
        

        public DataSet() {
            housingTypes = new List<HousingType>();
            microcontrollers = new List<Microcontrollers>();
        }
    }
}
