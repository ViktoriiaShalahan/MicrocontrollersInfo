using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MicrocontrollersInfo.Entity.FileIO.Interfaces;

namespace MicrocontrollersInfo.Entity
{
    public class BinarySerializationController :  IFileIoController {

        private string fileExtension = ".mbd";

        public BinarySerializationController()
        {
        }

        public BinarySerializationController(string fileExtension)
        {
            FileExtension = fileExtension;
        }

        public string FileExtension
        {
            get { return fileExtension; }
            set
            {
                if (value == null || !Regex.IsMatch(value,
                    @"\A\s*\.[A-Za-z]{1,3}(bd)\s*\z"))
                {
                    throw new FormatException(
                        "Розширення файлу з даними двійкової "
                        + "серіалізації повинно об'єднувати "
                        + "символ крапки, 1-3 латинських літери, "
                        + "що вказують на предметну область, "
                        + "та сполучення символів bd (binary data), "
                        + "(за замовчанням: \".sbd\")");
                }
                this.fileExtension = value.Trim().ToLower();
            }
        }

        public void Load(DataSet dataSet, string fileName) {
            DataSet newDataSet = LoadDataSet(fileName);
            if (newDataSet == null)
                return;
            foreach(var el in newDataSet.housingTypes) {
                dataSet.housingTypes.Add(el);
            }
            foreach(var el in newDataSet.microcontrollers) {
                dataSet.microcontrollers.Add(el);
            }
        }

        public DataSet LoadDataSet(string fileName)
        {
            fileName = Path.ChangeExtension(fileName, FileExtension);
            if (!File.Exists(fileName))
                return null;
            BinaryFormatter bFormatter = new BinaryFormatter();
            using (FileStream fSteam = File.OpenRead(fileName))
            {
                return bFormatter.Deserialize(fSteam) as DataSet;
            }
        }

        public void Save(DataSet dataSet, string fileName)
        {

            fileName = Path.ChangeExtension(fileName, FileExtension);
            BinaryFormatter bFormatter = new BinaryFormatter();
            using (var fStream = new FileStream(fileName,
                FileMode.Create,
                FileAccess.Write, FileShare.None))
            {
                bFormatter.Serialize(fStream, dataSet);
            }
        }
    }
}
