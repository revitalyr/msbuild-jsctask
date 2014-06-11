// Microsoft.Build.Tasks.Csc
/// <summary>Fills the specified <paramref name="commandLine" /> parameter with switches and other information that can go into a response file.</summary>
/// <param name="commandLine">Command line builder to add arguments to.</param>
protected internal override void AddResponseFileCommands(CommandLineBuilderExtension commandLine)
{
	commandLine.AppendSwitchIfNotNull("/lib:", base.AdditionalLibPaths, ",");
	commandLine.AppendPlusOrMinusSwitch("/unsafe", base.Bag, "AllowUnsafeBlocks");
	commandLine.AppendPlusOrMinusSwitch("/checked", base.Bag, "CheckForOverflowUnderflow");
	commandLine.AppendSwitchWithSplitting("/nowarn:", this.DisabledWarnings, ",", new char[]
	{
		';',
		','
	});
	commandLine.AppendWhenTrue("/fullpaths", base.Bag, "GenerateFullPaths");
	commandLine.AppendSwitchIfNotNull("/langversion:", this.LangVersion);
	commandLine.AppendSwitchIfNotNull("/moduleassemblyname:", this.ModuleAssemblyName);
	commandLine.AppendSwitchIfNotNull("/pdb:", this.PdbFile);
	commandLine.AppendPlusOrMinusSwitch("/nostdlib", base.Bag, "NoStandardLib");
	commandLine.AppendSwitchIfNotNull("/platform:", base.PlatformWith32BitPreference);
	commandLine.AppendSwitchIfNotNull("/errorreport:", this.ErrorReport);
	commandLine.AppendSwitchWithInteger("/warn:", base.Bag, "WarningLevel");
	commandLine.AppendSwitchIfNotNull("/doc:", this.DocumentationFile);
	commandLine.AppendSwitchIfNotNull("/baseaddress:", this.BaseAddress);
	commandLine.AppendSwitchUnquotedIfNotNull("/define:", this.GetDefineConstantsSwitch(base.DefineConstants));
	commandLine.AppendSwitchIfNotNull("/win32res:", base.Win32Resource);
	commandLine.AppendSwitchIfNotNull("/main:", base.MainEntryPoint);
	commandLine.AppendSwitchIfNotNull("/appconfig:", this.ApplicationConfiguration);
	commandLine.AppendWhenTrue("/errorendlocation", base.Bag, "ErrorEndLocation");
	commandLine.AppendSwitchIfNotNull("/preferreduilang:", this.PreferredUILang);
	commandLine.AppendPlusOrMinusSwitch("/highentropyva", base.Bag, "HighEntropyVA");
	this.AddReferencesToCommandLine(commandLine);
	base.AddResponseFileCommands(commandLine);
	commandLine.AppendSwitchWithSplitting("/warnaserror+:", this.WarningsAsErrors, ",", new char[]
	{
		';',
		','
	});
	commandLine.AppendSwitchWithSplitting("/warnaserror-:", this.WarningsNotAsErrors, ",", new char[]
	{
		';',
		','
	});
	if (base.ResponseFiles != null)
	{
		ITaskItem[] responseFiles = base.ResponseFiles;
		for (int i = 0; i < responseFiles.Length; i++)
		{
			ITaskItem taskItem = responseFiles[i];
			commandLine.AppendSwitchIfNotNull("@", taskItem.ItemSpec);
		}
	}
}
