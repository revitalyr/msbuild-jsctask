//http://www.csharp-examples.net/reflection-callstack/
using System;
using System.Diagnostics;

public class Programm {
  public static void Main()
  {
    StackTrace stackTrace = new StackTrace();           // get call stack
    StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

    // write call stack method names
    foreach (StackFrame stackFrame in stackFrames)
    {
      Console.WriteLine(stackFrame.GetMethod().Name);   // write method name
    }
  }
}
