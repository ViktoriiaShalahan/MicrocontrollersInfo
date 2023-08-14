using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using MicrocontrollersInfo.Entity.FileIO.Interfaces;

namespace MicrocontrollersInfo.Entity
{
    public class XmlFileIoController: IFileIoController
    {
        
        public XmlFileIoController()
        {
            FileExtension = ".xml";
        }


        public XmlFileIoController(string fileExtension)
        {
            FileExtension = fileExtension;
        }
        private string fileExtension;
        public string FileExtension
        {
            get { return fileExtension; }
            set
            {
                if (value == null || !Regex.IsMatch(value,
                    @"\A\s*\.[A-Za-z]{0,3}(xml)\s*\z"))
                {
                    throw new FormatException(
                        "Розширення файлу з даними XML-типу "
                        + "повинно об'єднувати "
                        + "символ крапки, 1-3 латинських літери, "
                        + "що вказують на предметну область, "
                        + "та сполучення символів xml, "
                        + "(за замовчанням: \".xml\")");
                }
                this.fileExtension = value.Trim().ToLower();
            }
        }

        public void Save(DataSet dataSet, string fileName)
        {
            //string fullFileName = fileName + fileExtension;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.Unicode;
            XmlWriter writer = null;
            try
            {
                writer = XmlWriter.Create(fileName, settings);
                WriteData(dataSet, writer);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        void WriteData(DataSet dataSet, XmlWriter writer)
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("MicrocontrollersInfo");
            WritesHousingType(dataSet.housingTypes, writer);
            WriteMicrocontrollers(dataSet.microcontrollers, writer);
            writer.WriteEndElement();
            writer.WriteEndDocument();
        }


        void WritesHousingType(IEnumerable<HousingType> collection, XmlWriter writer)
        {
            /*
             * public int Id;
                public string name;
                public string abbreviation;
                public int? numberRows;
                public string description;
                public string note;
             */
            writer.WriteStartElement("HousingTypesData");
            foreach (var inst in collection)
            {
                writer.WriteStartElement("HousingType");
                writer.WriteElementString("Id", inst.Id.ToString());
                writer.WriteElementString("name", inst.name);
                writer.WriteElementString("abbreviation", inst.abbreviation);
                writer.WriteElementString("numberRows", inst.numberRows.ToString());
                writer.WriteElementString("description", inst.description);
                writer.WriteElementString("note", inst.note);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }

        private void WriteMicrocontrollers(IEnumerable<Microcontrollers> collection, XmlWriter writer)
        {
            writer.WriteStartElement("MicrocontrollersData");
            foreach (var inst in collection)
            {
                /*
                 * public int Id;
            *   public string brand;
                public int bitRate;
                public HousingType housingType;
                public decimal price;
                public string description;
                public string note;
                 */
                writer.WriteStartElement("Microcontroller");
                writer.WriteElementString("Id", inst.Id.ToString());
                writer.WriteElementString("brand", inst.brand.ToString());
                writer.WriteElementString("bitRate", inst.bitRate.ToString());
                int housingTypeId = inst.housingType == null ? 0 : inst.housingType.Id;
                writer.WriteElementString("housingTypeId", housingTypeId.ToString());
                writer.WriteElementString("price", inst.price.ToString());
                writer.WriteElementString("description", inst.description);
                writer.WriteElementString("note", inst.note);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }


        //-----------------------------------------------------------

        public void Load(DataSet dataSet, string fileName)
        {
            //string fullFileName = fileName + fileExtension;
            if (!File.Exists(fileName)) return;

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreWhitespace = true;
            using (XmlReader reader = XmlReader.Create(fileName, settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "HousingType":
                                ReadHousingType(reader, dataSet.housingTypes);
                                break;
                            case "Microcontroller":
                                ReadMicrocontroller(reader, dataSet);
                                break;
                        }
                    }
                }
            }
        }


        void ReadHousingType(XmlReader reader, ICollection<HousingType> collection)
        {
            HousingType inst = new HousingType();
            /*
             * public int Id;
                public string name;
                public string abbreviation;
                public int? numberRows;
                public string description;
                public string note;
             */
            reader.ReadStartElement("HousingType");
            inst.Id = reader.ReadElementContentAsInt();
            inst.name = reader.ReadElementContentAsString();
            inst.abbreviation = reader.ReadElementContentAsString();
            inst.numberRows = reader.ReadElementContentAsInt();
            inst.description = reader.ReadElementContentAsString();
            inst.note = reader.ReadElementContentAsString();
            collection.Add(inst);
        }

        void ReadMicrocontroller(XmlReader reader, DataSet dataSet)
        {
            /*
             * public int Id;
        *   public string brand;
            public int bitRate;
            public HousingType housingType;
            public decimal price;
            public string description;
            public string note;
             */
            Microcontrollers inst = new Microcontrollers();
            reader.ReadStartElement("Microcontroller");
            inst.Id = reader.ReadElementContentAsInt();
            inst.brand = reader.ReadElementContentAsString();
            inst.bitRate = reader.ReadElementContentAsInt();
            int housingtypeId = reader.ReadElementContentAsInt();
            inst.housingType = dataSet.housingTypes
                .FirstOrDefault(e => e.Id == housingtypeId);
            inst.price = reader.ReadElementContentAsDecimal();
            inst.description = reader.ReadElementContentAsString();
            inst.note = reader.ReadElementContentAsString();
            dataSet.microcontrollers.Add(inst);
        }

    }
}
