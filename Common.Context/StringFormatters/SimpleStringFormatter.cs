using System;
using System.Text;

namespace Common.Context.StringFormatters {

  public class SimpleStringFormatter : StringFormatter {

    public override string FormatWithLineBreaks(
                    string text, int indentLength) {
      string indent = new String(' ', indentLength);
      if (text == null)
        return indent;
      StringBuilder sb = new StringBuilder(text.Length * 2);
      string[] arr = text.Split(new char[] { '\n' },
                StringSplitOptions.RemoveEmptyEntries);
      foreach (string s in arr) {
        FormatParagraph(s + "\n", indentLength, indent, sb, 70);
      }
      return sb.ToString();
    }

    private void FormatParagraph(string text,
        int indentLength, string indent, StringBuilder sb, int LineLength) {
      int pos = 0;
      int len = LineLength - indentLength - 1;
      //int patt = text.LastIndexOf(' ');
      //text.Replace(' ', '\n');


      while (true) {
        if (pos + len >= text.Length) {
          sb.Append(indent);
          sb.Append(text.Substring(pos));
          break;
        }
        int i = pos + len;


        for (; i > pos && char.IsWhiteSpace(text[i]) == false; i--) ;

        sb.Append(indent);
        if (i == pos) {
          sb.Append(text.Substring(pos, len) + "\n");
          pos += len;
        } else {
          sb.Append(text.Substring(pos, i - pos) + "\n");
          pos = i + 1;
        }
      }
    }
  }
}
