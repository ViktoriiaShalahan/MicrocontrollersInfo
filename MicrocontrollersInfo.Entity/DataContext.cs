using System.Collections.Generic;
using Common.Context.Extensions;
using System.Linq;
using System;
using MicrocontrollersInfo.Entity.FileIO.Interfaces;
using System.IO;

namespace MicrocontrollersInfo.Entity
{
    public class DataContext
    {
        
        private  XmlFileIoController xmlFileIoController;

        

        public readonly DataSet dataSet = new DataSet();
        private object housingTypes;
        private string FileName = "test";

        public ICollection<HousingType> HousingTypes
        {
            get { return dataSet.housingTypes; }
        }

        public ICollection<Microcontrollers> Microcontrollers
        {
            get { return dataSet.microcontrollers; }
        }

        private IFileIoController fileIoController;

        public IFileIoController FileIoController
        {
            get
            {
                return fileIoController;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                fileIoController = value;
            }
        }

        private string directory = "";

        public string Directory
        {
            get { return directory; }
            set
            {
                directory = value ?? "";
                if (!System.IO.Directory.Exists(directory))
                {
                    System.IO.Directory.CreateDirectory(directory);
                }
            }
        }

        public string FilePath {
            get
            {
                return Path.Combine(Directory,
                    FileName + FileIoController.FileExtension);
            }
        }

        public DataContext(IFileIoController fileIoController)
        {
            FileIoController = fileIoController;
        }

        public override string ToString()
        {
            return string.Concat("Інформація про об'єкти ПО \"Мікроконтролери\"\n",
               dataSet.microcontrollers.ToLineList("Мікроконтролери"),
               dataSet.housingTypes.ToLineList("Тип корпусу"));
        }

        public void Clear()
        {
            dataSet.housingTypes.Clear();
            dataSet.microcontrollers.Clear();
        }
        public void Save()
        {
            FileIoController.Save(dataSet, FilePath);
        }

        public void Load()
        {
            FileIoController.Load(dataSet, FilePath);
        }
        public void CreateTestingData()
        {
            CreateHousingType();
            CreateMicrocontrollers();
        }

       

        private void CreateHousingType()
        {
            dataSet.housingTypes.Add(new HousingType("Dual In-line Package","DIP",  16, "DIP — тип корпусу мікросхем, модулів і деяких інших електронних компонентів. Має прямокутну форму з двома рядами виводів по довгих сторонах. Може бути виконаний з пластику або кераміки. Керамічний корпус застосовується через близькі з кристалом коефіцієнти температурного розширення.", "Что такое микроконтроллер, семейства и корпуса AVR микроконтроллеров, https://ph0en1x.net/68-what-is-atmel-avr-microcontroller-families-and-chop-boxes.html") { Id = 1 });
            dataSet.housingTypes.Add(new HousingType("Thin Quad Flat Pack","TQFP", 16, "TQFP (Thin Quad Flat Pack) - тип корпусу мікросхеми.Має ті ж переваги, що QFP, але відрізняється меншою товщиною, яка становить від 1, 0 мм для 32 - вивідних мікросхем і до 1, 4 мм для 256 - вивідних, у той час як товщина QFP становить від 2, 0 до 3, 8мм.Має стандартний розмір висновків(2 мм). Можлива кількість висновків від 32 до 176 за розміром однієї сторони корпусу від 5 до 20 міліметрів, https://ph0en1x.net/68-what-is-atmel-avr-microcontroller-families-and-chop-boxes.html") { Id = 2});
            dataSet.housingTypes.Add(new HousingType("Quad Flat Package","QFP",  16, "QFP (Quad Flat Package) — плоский корпус із чотирма рядами контактів.Є квадратним корпусом з розташованими по краях контактами, https://www.chipdip/info/import-ic-packages") { Id = 3 });
        }


        public void CreateMicrocontrollers()
        {
            dataSet.microcontrollers.Add(new Microcontrollers(dataSet.housingTypes.FirstOrDefault(e => e.name == "Dual In-line Package"), "Atiny11L", 2, 2M, "Виконаний за AVR RISC- архітектурою Високопродуктивна та малопотужна 8 - розрядна RISC - архітектура - 90 інструкцій, більшість яких виконуються за 1 цикл. ", "https://studfile.net/preview/5286787/page:5/")
            {
                Id = 1,
                
            }) ;
            dataSet.microcontrollers.Add(new Microcontrollers(dataSet.housingTypes.FirstOrDefault(e => e.name == "Quad Flat Package"), "AT90S2313", 2, 200M, "AVR® - висока продуктивність та RISC архітектура з низьким енергоспоживанням 118 потужних інструкцій - більшість із них виконуються за один такт. ", "https://studfile.net/preview/5286787/page:5/")
            {
                Id = 2,
   
            });
            dataSet.microcontrollers.Add(new Microcontrollers(dataSet.housingTypes.FirstOrDefault(e => e.name == "Dual In-line Package"), "Atiny15L", 1, 70M, "Високопродуктивний, 8-розрядний AVR® мікроконтролер з низьким рівнем енергоспоживання. ", "https://studfile.net/preview/5286787/page:5/")
            {
                Id = 3,
                
            });
            dataSet.microcontrollers.Add(new Microcontrollers(dataSet.housingTypes.FirstOrDefault(e => e.name == "Quad Flat Package"), "AT90S8515", 1, 65M, "AVR® - висока продуктивність та RISC архітектура з низьким енергоспоживанням 118 потужних інструкцій - більшість із них виконуються за один такт ", "https://studfile.net/preview/5286787/page:5/")
            {
                Id = 4,
            });
        }

    }
}