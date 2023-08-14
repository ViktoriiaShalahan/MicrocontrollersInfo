using System;
using System.Collections.Generic;
using Common.ConsoleIO;
//using MicrocontrollersInfo.FileIO;
using MicrocontrollersInfo.Entity;
using System.Linq;
using MethodsRangeOut;
using MicrocontrollersInfo.Entity.FileIO.Interfaces;

namespace MicrocontrollersInfo.Editor
{
    public partial class Editor {

		private DataContext dataContext;

		IEnumerable<HousingType> sortingHousingTypes;
		IEnumerable<Microcontrollers> sortingMicrocontrollers;

		IFileIoController selectedFileIoController;

		IFileIoController[] fileIoControllers;

		FileIoEnum fileIoEnum = FileIoEnum.Binary;

		bool isNotEmpty = true;

		RangeOut[] rangeOuts = new RangeOut[]{
			new RangeOut(0, 3),
			new RangeOut(4, 4),
			new RangeOut(5, 5),
			new RangeOut(6, 6),
			new RangeOut(7, 12),
			new RangeOut(13, 18),
			new RangeOut(19, 20),
			new RangeOut(21, 21),
			new RangeOut(22, 22),
			new RangeOut(23, 26)
		};

		private string fileName = "microcontrollers_info";

		public string FileName {
			get => fileName;
			set {
				fileName = value.Trim().ToLower();
			}
		}

		public Editor(DataContext dataContext, BinarySerializationController binary, XmlFileIoController xml, TextController txt) {
			this.dataContext = dataContext;
			sortingHousingTypes = dataContext.HousingTypes;
			sortingMicrocontrollers = dataContext.Microcontrollers;
			selectedFileIoController = binary;
			fileIoControllers = new IFileIoController[] { binary, xml , txt };
			IniCommandsInfo();
		}

		private CommandInfo[] commandsInfo;



		private void IniCommandsInfo() {
			commandsInfo = new CommandInfo[] {
								new CommandInfo("вийти", null),
								new CommandInfo("створити тестові дані",  dataContext.CreateTestingData),
								new CommandInfo("додати запис про тип...", AddHousingType),
								new CommandInfo("додати запис про мікроконтролер...", AddMicrocontrollers),
								new CommandInfo("видалити запис про тип...", RemoveHousingType),
								new CommandInfo("видалити запис про мікроконтролер...", RemoveMicrocontroller),
								new CommandInfo("видалити усі записи...", dataContext.Clear),
								new CommandInfo("сортувати мікроконтролери за назвою (за зростанням)", SortMicrocontrollersByBrand),
								new CommandInfo("сортувати мікроконтролери за назвою (за спаданням)", SortMicrocontrollersByDescendingBrand),
								new CommandInfo("сортувати мікроконтролери за назвою типу (за зростанням)", SortMicrocontrollersByPrice),
								new CommandInfo("сортувати мікроконтролери за ціною (за спаданням)", SortMicrocontrollersByDPrice),
								new CommandInfo("сортувати мікроконтролери за розрядністю (за зростанням)", SortMicrocontrollersByBitRate),
								new CommandInfo("сортувати мікроконтролери за розрядністю (за спаданням)", SortMicrocontrollersByDBitRate),
								new CommandInfo("сортувати тип за назвою (за зростанням)", SortHousingTypeByName),
								new CommandInfo("сортувати тип за назвою (за спаданням)", SortHousingTypeByDName),
								new CommandInfo("сортувати тип за кількістю ніжок (за зростанням)", SortHousingTypeByNumberRows),
								new CommandInfo("сортувати тип за кількістю ніжок (за спаданням)", SortHousingTypeByDNumberRows),
								new CommandInfo("показати типи корпусів, що містять к-ть ніжок", FilterHousingTypeByNumberRowsNotUnknown),
								new CommandInfo("показати типи корпусів, що НЕ містять к-ть ніжок", FilterHousingTypeByNumberRowsUnknown),
								new CommandInfo("відібрати тип: назва починається з ...", FilterHousingTypeByStartName),
								new CommandInfo("відібрати тип: назва містить ...", FilterHousingTypeByNameFragment),
								new CommandInfo("відібрати мікроконтролери: ціна мін та макс", FilterMicrocontrollersByPriceMinMax),
								new CommandInfo("зберегти файл", Save),
								new CommandInfo("завантажити файл", Load),
								new CommandInfo("змінити тип файла", SetTypeFileIO),
								new CommandInfo("змінити назву файла", SetNameFile),
								new CommandInfo("змінити тип збереження", SwitchFileIO)
					 };
		}


		public void Run() {
			while (true) {
				Console.Clear();
				OutData();
				Console.WriteLine();
				OutTypeFileIO();
				UpdateRange();
				ShowCommandsMenu();
				Command command = EnterCommand();
				if (command == null) {
					return;
				}
				command();
			}
		}

		private void UpdateRange() {
			bool isNotEmptyMicrocontrollers = dataContext.Microcontrollers.Any();
			bool isNotEmptyHousingType = dataContext.HousingTypes.Any();
			bool isNotEmpty = isNotEmptyHousingType || isNotEmptyHousingType;

			rangeOuts[2].isWork = rangeOuts[4].isWork = rangeOuts[7].isWork = isNotEmptyMicrocontrollers;
			rangeOuts[1].isWork = rangeOuts[5].isWork = rangeOuts[6].isWork = rangeOuts[8].isWork = isNotEmptyHousingType;
			rangeOuts[3].isWork = rangeOuts[8].isWork = isNotEmpty;
		}

		private void ShowCommandsMenu() {
			Console.WriteLine("  Список команд меню:");
			for (int i = 0; i < rangeOuts.Length; i++) {
				if (rangeOuts[i].isWork) {
					RangeOut r = rangeOuts[i];
					for (int j = r.begin; j <= r.end; j++) {
						Console.WriteLine("\t{0,2} - {1}", j, commandsInfo[j].name);
					}				
				}
			}
		}

		private Command EnterCommand() {
			Console.WriteLine();
			int number = RangeOut.EnterRangeOut("Введіть номер команди", rangeOuts);
			return commandsInfo[number].command;
		}

		protected virtual void KeyPressWaiting() {
			Console.Write("\nДля продовження роботи програми "
					+ "натисніть довільну клавішу...");
			Console.ReadKey(true);
		}
	}
}
