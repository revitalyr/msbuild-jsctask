using System;

using Microsoft.Build.Tasks;

using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Web.Script.Serialization;
using System.Diagnostics;

namespace jscTask {
  public class jsc : ManagedCompiler {
    protected override string ToolName {
      get { return "jsc.exe";      }
    }

    protected override string GenerateFullPathToTool ( ) {
      throw new NotImplementedException ( );
    }

    protected override void AddResponseFileCommands (CommandLineBuilderExtension commandLine) {
      try {
        Log.LogMessage ("AddResponseFileCommands: {0}", dump<jsc> (this));
      }
      catch ( Exception ex ) {
        Trace.Assert (false, ex.ToString ( ));
        throw;
      }
    }
  }

  //http://stackoverflow.com/questions/4580397/json-formatter-in-c
  public static class JsonHelper  {
      private const string INDENT_STRING = "    ";
      public static string FormatJson(string str)  {
          var indent = 0;
          var quoted = false;
          var sb = new StringBuilder();
          for (var i = 0; i < str.Length; i++)  {
              var ch = str[i];
              switch (ch) {
                  case '{':
                  case '[':
                      sb.Append(ch);
                      if (!quoted) {
                          sb.AppendLine();
                          Enumerable.Range(0, ++indent).ForEach(item => sb.Append(INDENT_STRING));
                      }
                      break;
                  case '}':
                  case ']':
                      if (!quoted) {
                          sb.AppendLine();
                          Enumerable.Range(0, --indent).ForEach(item => sb.Append(INDENT_STRING));
                      }
                      sb.Append(ch);
                      break;
                  case '"':
                      sb.Append(ch);
                      bool escaped = false;
                      var index = i;
                      while (index > 0 && str[--index] == '\\')
                          escaped = !escaped;
                      if (!escaped)
                          quoted = !quoted;
                      break;
                  case ',':
                      sb.Append(ch);
                      if (!quoted)  {
                          sb.AppendLine();
                          Enumerable.Range(0, indent).ForEach(item => sb.Append(INDENT_STRING));
                      }
                      break;
                  case ':':
                      sb.Append(ch);
                      if (!quoted)
                          sb.Append(" ");
                      break;
                  default:
                      sb.Append(ch);
                      break;
              }
          }
          return sb.ToString();
      }

      public static string dump<T> (this T val) {
        JavaScriptSerializer  dumper = new JavaScriptSerializer ( );

        //if ( _SupportedTypes == null )
        //  _SupportedTypes = new Type[0];

        //dumper.RegisterConverters (new JavaScriptConverter[] { new SKB_ToStringConverter (_SupportedTypes) });
        string                result = JsonHelper.FormatJson (dumper.Serialize (val));

        return result;
      }

  }
}
