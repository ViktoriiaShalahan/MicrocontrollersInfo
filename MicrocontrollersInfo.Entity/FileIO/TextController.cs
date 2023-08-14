using System.IO;
using System.Linq;
using System.Text;
using MicrocontrollersInfo.Entity.FileIO.Interfaces;

namespace MicrocontrollersInfo.Entity
{
    public class TextController : IFileIoController {
		public TextController() { }

		public string FileExtension { get; set; } = ".txt";

		public void Load(DataSet dataSet, string fileName) {
			string fullFileName = fileName + FileExtension;
			if (!File.Exists(fullFileName)) {
				return;
			}
			StringBuilder sb = new StringBuilder();
			using (StreamReader sr = new StreamReader(fullFileName, Encoding.Unicode)) {
				while (sr.Peek() >= 0) {
					string[] str = GetTagAndValue(sr.ReadLine());
					if (str[0] == "Type") {
						switch (str[1]) {
							case nameof(Microcontrollers): {
								ReadMicrocontroller(sr, dataSet);
								break;
							}
							case nameof(HousingType): {
								ReadHousingType(sr, dataSet);
								break;
							}
						}
					}
				}
			}
		}

		private void ReadMicrocontroller(StreamReader sr, DataSet dataSet) {
			Microcontrollers microcontroller = new Microcontrollers(){
				Id = int.Parse(GetValue(sr.ReadLine())),
				brand = GetValue(sr.ReadLine()),
				bitRate = int.Parse(GetValue(sr.ReadLine()))
			};

			string temp = GetValue(sr.ReadLine());
			microcontroller.housingType = dataSet.housingTypes.FirstOrDefault(e => e.name == temp);
			microcontroller.price = decimal.Parse(GetValue(sr.ReadLine()));
			microcontroller.description = GetValue(sr.ReadLine());
			microcontroller.note = GetValue(sr.ReadLine());
			dataSet.microcontrollers.Add(microcontroller);
		}

		private void ReadHousingType(StreamReader sr, DataSet dataSet) {
			HousingType housingType = new HousingType(){
				Id = int.Parse(GetValue(sr.ReadLine())),
				name = GetValue(sr.ReadLine()),
				abbreviation = GetValue(sr.ReadLine()),
			};

			string temp = GetValue(sr.ReadLine());
			housingType.numberRows = string.IsNullOrEmpty(temp) ? (int?)null : int.Parse(temp);
			housingType.description = GetValue(sr.ReadLine());
			housingType.note = GetValue(sr.ReadLine());
			dataSet.housingTypes.Add(housingType);
		}

		private string GetValue(string s) {
			int pos = s.IndexOf(":");
			return s.Substring(pos + 1).Trim();
		}

		private string[] GetTagAndValue(string s) {
			int pos = s.IndexOf(":");
			return new string[2] { s.Substring(0, pos).Trim(), s.Substring(pos + 1).Trim() };
		}

		public void Save(DataSet dataSet, string fileName) {
			string fullFileName = fileName + FileExtension;
			if (File.Exists(fullFileName))
				File.Delete(fullFileName);
			foreach (var o in dataSet.housingTypes) {
				File.AppendAllText(fullFileName, WriteHousingType(o), Encoding.Unicode);
			}
			foreach (var o in dataSet.microcontrollers) {
				File.AppendAllText(fullFileName, WriteMicrocontroller(o), Encoding.Unicode);
			}
		}

		public string WriteHousingType(HousingType h) {
			return $"Type: {nameof(HousingType)}\n" +
				$"\tId: {h.Id}\n" +
				$"\tname: {h.name}\n" +
				$"\tabbreviation: {h.abbreviation}\n" +
				$"\tnumberRows: {h.numberRows?.ToString()}\n" +
				$"\tdescription: {h.description.Replace('\n', ' ')}\n" +
				$"\tnote: {h.note.Replace('\n', ' ')}\n";
		}

		public string WriteMicrocontroller(Microcontrollers m) {
			return $"Type: {nameof(Microcontrollers)}\n" +
				$"\tId: {m.Id}\n" +
				$"\tBrand: {m.brand}\n" +
				$"\tbitRate: {m.bitRate}\n" +
				$"\thousingTypeName: {m.housingType?.name}\n" +
				$"\tprice: {m.price}\n" +
				$"\tdescription: {m.description.Replace('\n', ' ')}\n" +
				$"\tnote: {m.note.Replace('\n', ' ')}\n";
		}

	}
}
