using System;
using Common.ConsoleIO;
namespace MethodsRangeOut {
	public class RangeOut {
		public int begin;
		public int end;
		public bool isWork = true;

		public RangeOut(int begin, int end) {
			this.begin = begin;
			this.end = end;
		}

		public static int EnterRangeOut(string title, RangeOut[] r) {
			while (true) {
				int num = Entering.EnterInt32(title);
				for (int i = 0; i < r.Length; i++) {
					RangeOut range = r[i];
					if (num >= range.begin && num <= range.end) {
						if (range.isWork) return num;
						goto l1;
					}
				}
			l1:
				Console.WriteLine("Число не підходить в задані вежі");
			}
		}
	}
}
