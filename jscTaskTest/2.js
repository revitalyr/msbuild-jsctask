/*@cc_on @*/
/*@if (@_jscript_version>6)
@else @*/
function print(s) {
  WScript.StdErr.WriteLine (s);
}
/*@end @*/

print("testing")
  /*@if (@_jscript_build)
  var jscript_build = @_jscript_build;
  print("_jscript_build defined =" + jscript_build + ", _jscript_version=" + @_jscript_version);
  @else @*/
  print("_jscript_build not defined");
  /*@end @*/
