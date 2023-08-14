using System;
using System.Text.RegularExpressions;
using System.Linq;
namespace Common.ConsoleIO {
	public static class Entering {
		public static int EnterInt32(string prompt) {
			while (true) {
				Console.Write("\t{0}: ", prompt);
				string s = Console.ReadLine();
				int value;
				if (int.TryParse(s, out value)) {
					return value;
				}
				Console.WriteLine("\t\tпомилка введення цілого числа");
			}
		}
		public static int EnterInt32(string prompt, int min, int max) {
			while (true) {
				Console.Write("\t{0}: ", prompt);
				string s = Console.ReadLine();
				int value;
				if (int.TryParse(s, out value)) {
					if (value >= min && value <= max)
						return value;
				}
				Console.WriteLine("\t\tпомилка введення цілого числа");
			}
		}
		public static decimal EnterDecimal(string prompt) {
			while (true) {
				Console.Write("\t{0}: ", prompt);
				string s = Console.ReadLine();
				decimal value;
				if (decimal.TryParse(s, out value)) {
					return value;
				}
				Console.WriteLine("\t\tпомилка введення цілого числа");
			}
		}
		public static decimal EnterDecimal(string prompt, decimal min, decimal max) {
			while (true) {
				Console.Write("\t{0}: ", prompt);
				string s = Console.ReadLine();
				decimal value;
				if (decimal.TryParse(s, out value)) {
					if (value >= min && value <= max)
						return value;
				}
				Console.WriteLine("\t\tпомилка введення цілого числа");
			}
		}
		public static int? EnterNullableInt32(string prompt) {
			Console.Write("{0}: ", prompt);
			while (true) {
				Console.Write("\t{0}: ", prompt);
				string s = Console.ReadLine();
				int value;
				if (string.IsNullOrWhiteSpace(s) == true) {
					return (int?)null;
				}
				if (int.TryParse(s, out value)) {
					return value;
				}
				Console.WriteLine("\t\tпомилка введення цілого числа");
			}
		}
		public static string EnterString(string prompt) {
			Console.Write("\t{0}: ", prompt);
			return Console.ReadLine().Trim();
		}
		public static string EnterString(string prompt, int min, int max) {
			Console.Write("\t{0}: ", prompt);
			while (true) {
				string str = Console.ReadLine().Trim();
				if (str.Length >= min && str.Length <= max) return str;
				Console.WriteLine($"Розмір введеного від {min} до {max}"); ;
			}
		}
		public static string EnterString(string prompt, string regex, RegexOptions regexOptions = RegexOptions.None) {
			Console.Write("\t{0}: ", prompt);
			while (true) {
				string str = Console.ReadLine().Trim();
				if (Regex.IsMatch(str, regex, regexOptions))
					return str;
				Console.WriteLine("Не за форматом");
			}
		}

		public static string EnterString(string prompt, int max) {
			Console.Write("\t{0}: ", prompt);
			while (true) {
				string str = Console.ReadLine().Trim();
				if (str.Length <= max) return str;
				Console.WriteLine("Розмір введеного завеликий");
			}
		}

		public static T EnterEnum<T>(string title, string[] arr) where T : System.Enum {
			while (true) {
				Console.Write($"{title} ({string.Join(",", arr)}): ");
				string str = Console.ReadLine().Trim();
				dynamic res;
				int i = 0;
				for (; i < arr.Length; i++) {
					if (arr[i] == str) break;
				}
				if (i == arr.Length) {
					Console.WriteLine("Неправильно введено!");
					continue;
				}
				res = i;
				return (T)res;
			}
		}

		public static void Pause() {
			Console.WriteLine("Щоб продовжити, натисніть на будь-яку клавішу...");
			Console.ReadKey(false);
		}
	}
}
