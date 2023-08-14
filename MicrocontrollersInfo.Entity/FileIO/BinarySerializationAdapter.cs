using Common.IO;
using MicrocontrollersInfo.Entity.FileIO.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrocontrollersInfo.Entity.FileIO
{
    public class BinarySerializationAdapter : IFileIoController
    {

        GenericBinarySerializationController<DataSet> fileIoController
            = new GenericBinarySerializationController<DataSet>();

        public string FileExtension
        {
            get { return fileIoController.FileExtension; }
            set { fileIoController.FileExtension = value; }
        }

        public BinarySerializationAdapter()
        {
            FileExtension = ".mibd";
        }

        public void Load(DataSet dataSet, string fileName)
        {
            //throw new NotImplementedException();
            DataSet newDataSet = fileIoController.Load(fileName);
            foreach (HousingType el in newDataSet.housingTypes)
            {
                dataSet.housingTypes.Add(el);
            }
            foreach (Microcontrollers el in newDataSet.microcontrollers)
            {
                dataSet.microcontrollers.Add(el);
            }
        }

        public void Save(DataSet dataSet, string fileName)
        {
            //throw new NotImplementedException();
            fileIoController.Save(dataSet, fileName);
        }
    }
}
