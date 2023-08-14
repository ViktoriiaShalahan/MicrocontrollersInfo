using System;

namespace MicrocontrollersInfo.Entity
{
    [Serializable]
    public class HousingType : Common.Entities.Entity
    {
        public string name;
        public string abbreviation;
        public int? numberRows;
        public string description;
        public string note;

        public HousingType(string name, string abbreviation, int? numberRows = null, string description = "", string note = "")
        {
            this.name = name;
            this.abbreviation = abbreviation;
            this.numberRows = numberRows;
            this.description = description;
            this.note = note;
        }

        public HousingType() { }

        public override string ToString()
        {
            return string.Format("{0,7} {1,20} {2,6} {3,10},\n{4}\n{5}", Id, name, abbreviation, numberRows, description, note);
        }
    }
}
