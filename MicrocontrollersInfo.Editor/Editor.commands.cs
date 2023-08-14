using System;
using System.Linq;
using System.Text.RegularExpressions;
using Common.ConsoleIO;
using Common.Context.Extensions;
using MicrocontrollersInfo.Entity;
//using MicrocontrollersInfo.FileIO;
namespace MicrocontrollersInfo.Editor {
	public partial class Editor {
		private void OutData() {
			OutMicrocontrollers();
			OutHousingTypes();
		}

		private void OutMicrocontrollers() {
			Console.WriteLine("\tСписок мікроконтролерів");
			foreach (var obj in sortingMicrocontrollers) {
				Console.WriteLine("{0,5} {1,-17}, {2,-3}, {3,-7}, {4,8:F2}\n{5}{6}", obj.Id, obj.brand, obj.bitRate, obj.housingType?.name, obj.price,
					string.IsNullOrEmpty(obj.description) ? "" : $"Опис:\n{obj.description.ToIndentedLineBlock(4)}\n",
					string.IsNullOrEmpty(obj.note) ? "" : $"Примітка:\n{obj.note.ToIndentedLineBlock(4)}\n");
			}
		}

		private void OutHousingTypes() {
			Console.WriteLine("\n  Список типів корпусу:");
			foreach (var obj in sortingHousingTypes) {
				Console.WriteLine("{0,5} {1,-20} {2,-5}, {3,3}\n{4}{5}",
						obj.Id, obj.name, obj.abbreviation, obj.numberRows?.ToString(),
							string.IsNullOrEmpty(obj.description) ? "" : $"Опис:\n{obj.description.ToIndentedLineBlock(4)}\n",
					string.IsNullOrEmpty(obj.note) ? "" : $"Примітка:\n{obj.note.ToIndentedLineBlock(4)}\n"
						);
			}
		}

		public void AddMicrocontrollers() {
			Microcontrollers inst = new Microcontrollers();
			inst.brand = Entering.EnterString("\tВведіть назву марки", 2, 50);
			inst.bitRate = Entering.EnterInt32("Введіть кількість розрядність", 1, 512);
			inst.housingType = SelectHousingType();
			if (inst.housingType == null) {
				Console.WriteLine("Такого типу немає!");
				return;
			}
			if (dataContext.Microcontrollers.Any()) {
				inst.Id = dataContext.Microcontrollers.Select(e => e.Id).Max() + 1;
			} else inst.Id = 1;
			inst.price = Entering.EnterDecimal("Введіть ціну", 2M, 14100M);
			inst.description = Entering.EnterString("Опишіть мікросхему", 65535);
			inst.note = Entering.EnterString("Опишіть нотацію", 1024);
			dataContext.dataSet.microcontrollers.Add(inst);
		}

		private HousingType SelectHousingType() {
			string abbr = Entering.EnterString("Введіть абревіатуру").ToUpper();
			return dataContext.HousingTypes.FirstOrDefault(e => e.abbreviation == abbr);
		}

		public void AddHousingType() {
			HousingType inst = new HousingType();
			inst.name = Entering.EnterString("Введіть назву типу корпусу", 3, 30);
			inst.abbreviation = Entering.EnterString("Введіть абревіатуру", @"^[A-Z]{1,6}$", RegexOptions.IgnoreCase).ToUpper();
			inst.numberRows = Entering.EnterInt32("Введіть к-ть ніжок", 1, int.MaxValue);
			inst.description = Entering.EnterString("Опишіть тип", 65535);
			inst.note = Entering.EnterString("Опишіть нотацію", 1024);
			if (dataContext.HousingTypes.Any()) {
				inst.Id = dataContext.HousingTypes.Select(e => e.Id).Max() + 1;
			} else inst.Id = 1;
			dataContext.dataSet.housingTypes.Add(inst);
		}




		public void RemoveMicrocontroller() {
			int id = Entering.EnterInt32("Введіть Id мікроконтролера");
			Microcontrollers inst = dataContext.Microcontrollers.FirstOrDefault(e => e.Id == id);
			dataContext.Microcontrollers.Remove(inst);
			Console.WriteLine(inst == null ? "Не існує мікроконтролера з таким Id" : "Об'єкт успішно видалено");
			Entering.Pause();
		}

		public void RemoveHousingType() {
			int id = Entering.EnterInt32("Введіть Id корпусу");
			HousingType inst = dataContext.HousingTypes.FirstOrDefault(e => e.Id == id);
			if (inst == null) {
				Console.WriteLine("Не існує корпусу з таким Id");
			} else if (dataContext.Microcontrollers.Select(e => e.housingType).Any(e => e == inst)) {
				Console.WriteLine("Неможливо видалити корпус, тому що містить залежності");
			} else {
				Console.WriteLine("Об'єкт успішно видалено");
				dataContext.HousingTypes.Remove(inst);
			}
			Entering.Pause();
		}


		private void SortMicrocontrollersByBrand() {
			sortingMicrocontrollers = sortingMicrocontrollers.OrderBy(e => e.brand);
		}
		private void SortMicrocontrollersByDescendingBrand() {
			sortingMicrocontrollers = sortingMicrocontrollers.OrderByDescending(e => e.brand);
		}
		private void SortMicrocontrollersByPrice() {
			sortingMicrocontrollers = sortingMicrocontrollers.OrderBy(e => e.price);
		}
		private void SortMicrocontrollersByDPrice() {
			sortingMicrocontrollers = sortingMicrocontrollers.OrderByDescending(e => e.price);
		}
		private void SortMicrocontrollersByBitRate() {
			sortingMicrocontrollers = sortingMicrocontrollers.OrderBy(e => e.bitRate);
		}
		private void SortMicrocontrollersByDBitRate() {
			sortingMicrocontrollers = sortingMicrocontrollers.OrderByDescending(e => e.bitRate);
		}



		private void SortHousingTypeByName() {
			sortingHousingTypes = sortingHousingTypes.OrderBy(e => e.name);
		}
		private void SortHousingTypeByDName() {
			sortingHousingTypes = sortingHousingTypes.OrderByDescending(e => e.name);
		}
		private void SortHousingTypeByNumberRows() {
			sortingHousingTypes = sortingHousingTypes.OrderBy(e => e.numberRows);
		}
		private void SortHousingTypeByDNumberRows() {
			sortingHousingTypes = sortingHousingTypes.OrderByDescending(e => e.numberRows);
		}

		private void Save() {
			try {
				selectedFileIoController.Save(dataContext.dataSet, fileName);
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
				KeyPressWaiting();
			}

		}
		private void Load() {
			try {
				selectedFileIoController.Load(dataContext.dataSet, fileName);
			}
			catch (Exception e) {
				Console.WriteLine($"Exception: {e.Message}");
				Entering.Pause();
			}
		}

		private void SwitchFileIO() {
			fileIoEnum = Entering.EnterEnum<FileIoEnum>("Виберіть тип", ClassFileIoEnum.namesFileIoEnum);
			selectedFileIoController = fileIoControllers[(int)fileIoEnum];
		}

		private void OutTypeFileIO() {
			Console.WriteLine("Name: {0}\n" +
					"Type file is: {1}", fileName, selectedFileIoController.FileExtension);
		}

		private void SetTypeFileIO() {

			try {
				selectedFileIoController.FileExtension = Entering.EnterString("Введіть тип файла", 1, 20);
			}
			catch (Exception e) {
				Console.WriteLine(e.Message);
				KeyPressWaiting();
			}

		}

		private void SetNameFile() {
			FileName = Entering.EnterString("Введіть назву файла", 1, 20);
		}
	}
}
