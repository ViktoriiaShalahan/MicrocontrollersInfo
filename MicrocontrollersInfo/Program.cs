using System;
using Common.ConsoleIO;
using MicrocontrollersInfo.Editor;
//using MicrocontrollersInfo.FileIO;
using MicrocontrollersInfo.Entity;

namespace MicrocontrollersInfo {
	class Program {
		static DataContext dataContext;
		static MicrocontrollersInfo.Editor.Editor editor;
		static BinarySerializationController binarySerializationController = new BinarySerializationController();
		static XmlFileIoController xmlFileIoController = new XmlFileIoController();
		static TextController textController = new TextController();

		static void Main(string[] args) {
			Console.Title = "MicrocontrollersInfo.ConsoleEditor (Шалаган В. С.)";
			Settings.SetConsoleParam();
			Console.WriteLine("Реалізація класів для предметної області");

			dataContext = new DataContext(xmlFileIoController);
			editor = new Editor.Editor(dataContext, binarySerializationController, xmlFileIoController, textController);
			editor.Run();

			Entering.Pause();
		}
	}
}
