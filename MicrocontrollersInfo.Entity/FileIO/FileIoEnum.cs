using System;
using System.Collections.Generic;
using System.Text;

namespace MicrocontrollersInfo.Entity
{
	public enum FileIoEnum : uint{
		Binary,
		Xml,
		Text
	}
	public static class ClassFileIoEnum {
		public static readonly string[] namesFileIoEnum = new string[]{"Binary", "Xml", "Text"};
	}
}
