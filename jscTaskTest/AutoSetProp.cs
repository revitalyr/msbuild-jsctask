using System;
using System.Diagnostics;
using System.Collections;

namespace AutoSetProp {
    public class ToolTaskExtension //: ToolTask
	{
    	private Hashtable bag = new Hashtable();

    	protected internal Hashtable Bag { get { return this.bag; } }
    	protected internal bool GetBoolParameterWithDefault(string parameterName, bool defaultValue) {
            object obj = this.bag[parameterName];
			return (obj == null) ? defaultValue : (bool)obj;
		}
    }

  public class Programm : ToolTaskExtension {
    object  getPropByName(object defaultValue) {
      StackTrace stackTrace = new StackTrace();           // get call stack
      StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

      Console.WriteLine("\ngetPropByName:");
      // write call stack method names
      foreach (StackFrame stackFrame in stackFrames) {
        Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
      }

      return defaultValue;
    }

   	public bool TheProperty
   	{
   		get {
   			return (bool)getPropByName(false);
   		}
   		set {
   			base.Bag["CheckForOverflowUnderflow"] = value;
   		}
   	}

    public static void Main() {
        Programm    inst = new Programm();
        Console.WriteLine("TheProperty = {0}", inst.TheProperty);
    }
  }
}
