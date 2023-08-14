﻿using System.Collections.Generic;
using Common.Context.LineIndents;

namespace Common.Context.Extensions {
	public static class EnumerableMethods {
		public static string ToIndentedLineList<T>(
						this IEnumerable<T> collection, string prompt) {
			string s = string.Concat(LineIndent.Current.Value, prompt, ":\n");
			LineIndent.Current.Increase();
			s += string.Concat(string.Join("\n", collection), "\n");
			LineIndent.Current.Decrease();
			return s;
		}

		public static string ToLineList<T>(this IEnumerable<T> collection, string prompt) {
			return string.Concat(prompt, ":\n", string.Join("\n", collection), "\n");
		}
	}
}
