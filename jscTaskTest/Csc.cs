using Microsoft.Build.Framework;
using Microsoft.Build.Shared;
using Microsoft.Build.Shared.LanguageParser;
using Microsoft.Build.Tasks.Hosting;
using Microsoft.Build.Tasks.InteropUtilities;
using Microsoft.Build.Utilities;
using Microsoft.Internal.Performance;
using System;
using System.Globalization;
using System.Runtime;
using System.Text;
namespace Microsoft.Build.Tasks
{
	/// <summary>Implements the Csc task. Use the Csc element in your project file to create and execute this task. For usage and parameter information, see Csc Task.</summary>
	public class Csc : ManagedCompiler
	{
		private bool useHostCompilerIfAvailable;
		/// <summary>Gets or sets a Boolean value that indicates whether to compile code that uses the unsafe keyword.</summary>
		/// <returns>true if will compile code that uses the unsafe keyword; otherwise, false.</returns>
		public bool AllowUnsafeBlocks
		{
			get
			{
				return base.GetBoolParameterWithDefault("AllowUnsafeBlocks", false);
			}
			set
			{
				base.Bag["AllowUnsafeBlocks"] = value;
			}
		}
		/// <summary>Returns configuration information.</summary>
		public string ApplicationConfiguration
		{
			get
			{
				return (string)base.Bag["ApplicationConfiguration"];
			}
			set
			{
				base.Bag["ApplicationConfiguration"] = value;
			}
		}
		/// <summary>Gets or sets the preferred base address at which to load a DLL.</summary>
		/// <returns>The preferred base address at which to load a DLL.</returns>
		public string BaseAddress
		{
			get
			{
				return (string)base.Bag["BaseAddress"];
			}
			set
			{
				base.Bag["BaseAddress"] = value;
			}
		}
		/// <summary>Gets or sets a Boolean value that indicates whether the Csc task should cause an exception at run time for integer arithmetic that overflows the bounds of the data type.</summary>
		/// <returns>Gets or sets a Boolean value that indicates whether true if the Csc task should cause an exception at run time for integer arithmetic that overflows the bounds of the data type.</returns>
		public bool CheckForOverflowUnderflow
		{
			get
			{
				return base.GetBoolParameterWithDefault("CheckForOverflowUnderflow", false);
			}
			set
			{
				base.Bag["CheckForOverflowUnderflow"] = value;
			}
		}
		/// <summary>Gets or sets the XML file to hold the documentation comments.</summary>
		/// <returns>The XML file to hold the documentation comments.</returns>
		public string DocumentationFile
		{
			get
			{
				return (string)base.Bag["DocumentationFile"];
			}
			set
			{
				base.Bag["DocumentationFile"] = value;
			}
		}
		/// <summary>Gets or sets the list of warnings to be disabled.</summary>
		/// <returns>The list of warnings to be disabled.</returns>
		public string DisabledWarnings
		{
			get
			{
				return (string)base.Bag["DisabledWarnings"];
			}
			set
			{
				base.Bag["DisabledWarnings"] = value;
			}
		}
		public bool ErrorEndLocation
		{
			get
			{
				return base.GetBoolParameterWithDefault("ErrorEndLocation", false);
			}
			set
			{
				base.Bag["ErrorEndLocation"] = value;
			}
		}
		/// <summary>Gets or sets the method to report a C# internal compiler error to Microsoft.</summary>
		/// <returns>The method to report a C# internal compiler error to Microsoft.</returns>
		public string ErrorReport
		{
			get
			{
				return (string)base.Bag["ErrorReport"];
			}
			set
			{
				base.Bag["ErrorReport"] = value;
			}
		}
		/// <summary>Gets or sets a Boolean value that indicates whether to generate the absolute path to the file in the compiler output (true) or to generate the name of the file in the compiler output (false).</summary>
		/// <returns>true if the Csc task should generate the absolute path to the file in the compiler output; false if the Csc task should generate the name of the file in the compiler output.</returns>
		public bool GenerateFullPaths
		{
			get
			{
				return base.GetBoolParameterWithDefault("GenerateFullPaths", false);
			}
			set
			{
				base.Bag["GenerateFullPaths"] = value;
			}
		}
		/// <summary>Gets or sets the version of the language to use.</summary>
		/// <returns>The version of the language to use.</returns>
		public string LangVersion
		{
			get
			{
				return (string)base.Bag["LangVersion"];
			}
			set
			{
				base.Bag["LangVersion"] = value;
			}
		}
		/// <summary>Gets or sets an assembly whose non-public types a .netmodule can access.</summary>
		/// <returns>An assembly whose non-public types a .netmodule can access.</returns>
		public string ModuleAssemblyName
		{
			get
			{
				return (string)base.Bag["ModuleAssemblyName"];
			}
			set
			{
				base.Bag["ModuleAssemblyName"] = value;
			}
		}
		/// <summary>Gets or sets a Boolean value that indicates whether the Csc task should prevent the import of mscorlib.dll, which defines the entire System namespace.</summary>
		/// <returns>true if the Csc task should prevent the import of mscorlib.dll; otherwise, false.</returns>
		public bool NoStandardLib
		{
			get
			{
				return base.GetBoolParameterWithDefault("NoStandardLib", false);
			}
			set
			{
				base.Bag["NoStandardLib"] = value;
			}
		}
		/// <summary>Gets or sets the path of the .pdb file.</summary>
		/// <returns>The path of the .pdb file.</returns>
		public string PdbFile
		{
			get
			{
				return (string)base.Bag["PdbFile"];
			}
			set
			{
				base.Bag["PdbFile"] = value;
			}
		}
		public string PreferredUILang
		{
			get
			{
				return (string)base.Bag["PreferredUILang"];
			}
			set
			{
				base.Bag["PreferredUILang"] = value;
			}
		}
		/// <summary>Gets or sets a Boolean value that indicates whether the Csc task should use the in-process compiler object, if available.</summary>
		/// <returns>true if the Csc task should use the in-process compiler object, if available; otherwise, false.</returns>
		public bool UseHostCompilerIfAvailable
		{
			[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
			get
			{
				return this.useHostCompilerIfAvailable;
			}
			[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
			set
			{
				this.useHostCompilerIfAvailable = value;
			}
		}
		/// <summary>Gets or sets the warning level for the compiler to display.</summary>
		/// <returns>The warning level for the compiler to display.</returns>
		public int WarningLevel
		{
			get
			{
				return base.GetIntParameterWithDefault("WarningLevel", 4);
			}
			set
			{
				base.Bag["WarningLevel"] = value;
			}
		}
		/// <summary>Gets or sets a list of warnings to treat as errors.</summary>
		/// <returns>The list of warnings to treat as errors.</returns>
		public string WarningsAsErrors
		{
			get
			{
				return (string)base.Bag["WarningsAsErrors"];
			}
			set
			{
				base.Bag["WarningsAsErrors"] = value;
			}
		}
		/// <summary>Gets or sets a list of warnings that are not treated as errors.</summary>
		/// <returns>The list of warnings that are not treated as errors.</returns>
		public string WarningsNotAsErrors
		{
			get
			{
				return (string)base.Bag["WarningsNotAsErrors"];
			}
			set
			{
				base.Bag["WarningsNotAsErrors"] = value;
			}
		}
		/// <summary>Returns the name of the Csc tool (csc.exe).</summary>
		/// <returns>The name of the Csc tool (csc.exe).</returns>
		protected override string ToolName
		{
			get
			{
				return "Csc.exe";
			}
		}
		/// <summary>Returns the full file path of the Csc tool.</summary>
		/// <returns>The full file path of the Csc tool.</returns>
		protected override string GenerateFullPathToTool()
		{
			string pathToDotNetFrameworkFile = ToolLocationHelper.GetPathToDotNetFrameworkFile(this.ToolName, TargetDotNetFrameworkVersion.Version45);
			if (pathToDotNetFrameworkFile == null)
			{
				base.Log.LogErrorWithCodeFromResources("General.FrameworksFileNotFound", new object[]
				{
					this.ToolName,
					ToolLocationHelper.GetDotNetFrameworkVersionFolderPrefix(TargetDotNetFrameworkVersion.Version45)
				});
			}
			return pathToDotNetFrameworkFile;
		}
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
		internal string GetDefineConstantsSwitch(string originalDefineConstants)
		{
			if (originalDefineConstants == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string[] array = originalDefineConstants.Split(new char[]
			{
				',',
				';',
				' '
			});
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (Csc.IsLegalIdentifier(text))
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.Append(";");
					}
					stringBuilder.Append(text);
				}
				else
				{
					if (text.Length > 0)
					{
						base.Log.LogWarningWithCodeFromResources("Csc.InvalidParameterWarning", new object[]
						{
							"/define:",
							text
						});
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				return stringBuilder.ToString();
			}
			return null;
		}
		/// <summary>Returns a host object initialization status value that indicates what the next action should be.</summary>
		/// <returns>A host object initialization status value that indicates what the next action should be.</returns>
		protected override HostObjectInitializationStatus InitializeHostObject()
		{
			if (base.HostObject != null)
			{
				using (RCWForCurrentContext<ICscHostObject> rCWForCurrentContext = new RCWForCurrentContext<ICscHostObject>(base.HostObject as ICscHostObject))
				{
					ICscHostObject rCW = rCWForCurrentContext.RCW;
					if (rCW != null)
					{
						bool flag = this.InitializeHostCompiler(rCW);
						if (rCW.IsDesignTime())
						{
							HostObjectInitializationStatus result = flag ? HostObjectInitializationStatus.NoActionReturnSuccess : HostObjectInitializationStatus.NoActionReturnFailure;
							return result;
						}
						if (!base.HostCompilerSupportsAllParameters || this.UseAlternateCommandLineToolToExecute())
						{
							HostObjectInitializationStatus result;
							if (!base.CheckAllReferencesExistOnDisk())
							{
								result = HostObjectInitializationStatus.NoActionReturnFailure;
								return result;
							}
							base.UsedCommandLineTool = true;
							result = HostObjectInitializationStatus.UseAlternateToolToExecute;
							return result;
						}
						else
						{
							HostObjectInitializationStatus result;
							if (flag)
							{
								result = (rCW.IsUpToDate() ? HostObjectInitializationStatus.NoActionReturnSuccess : HostObjectInitializationStatus.UseHostObjectToExecute);
								return result;
							}
							result = HostObjectInitializationStatus.NoActionReturnFailure;
							return result;
						}
					}
					else
					{
						base.Log.LogErrorWithCodeFromResources("General.IncorrectHostObject", new object[]
						{
							"Csc",
							"ICscHostObject"
						});
					}
				}
			}
			base.UsedCommandLineTool = true;
			return HostObjectInitializationStatus.UseAlternateToolToExecute;
		}
		/// <summary>Compiles the project through the host object.</summary>
		/// <returns>true if compilation succeeded; otherwise, false.</returns>
		protected override bool CallHostObjectToExecute()
		{
			ICscHostObject cscHostObject = base.HostObject as ICscHostObject;
			bool result;
			try
			{
				CodeMarkers.Instance.CodeMarker(8804);
				result = cscHostObject.Compile();
			}
			finally
			{
				CodeMarkers.Instance.CodeMarker(8805);
			}
			return result;
		}
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Build.Tasks.Csc" /> class.</summary>
		[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
		public Csc()
		{
		}
		private void AddReferencesToCommandLine(CommandLineBuilderExtension commandLine)
		{
			if (base.References == null || base.References.Length == 0)
			{
				return;
			}
			ITaskItem[] references = base.References;
			for (int i = 0; i < references.Length; i++)
			{
				ITaskItem taskItem = references[i];
				string metadata = taskItem.GetMetadata("Aliases");
				string switchName = "/reference:";
				bool flag = MetadataConversionUtilities.TryConvertItemMetadataToBool(taskItem, "EmbedInteropTypes");
				if (flag)
				{
					switchName = "/link:";
				}
				if (metadata == null || metadata.Length == 0)
				{
					commandLine.AppendSwitchIfNotNull(switchName, taskItem.ItemSpec);
				}
				else
				{
					string[] array = metadata.Split(new char[]
					{
						','
					});
					string[] array2 = array;
					for (int j = 0; j < array2.Length; j++)
					{
						string text = array2[j];
						string text2 = text.Trim();
						if (text.Length != 0)
						{
							if (text2.IndexOfAny(new char[]
							{
								',',
								' ',
								';',
								'"'
							}) != -1)
							{
								ErrorUtilities.VerifyThrowArgument(false, "Csc.AssemblyAliasContainsIllegalCharacters", taskItem.ItemSpec, text2);
							}
							if (string.Compare("global", text2, StringComparison.OrdinalIgnoreCase) == 0)
							{
								commandLine.AppendSwitchIfNotNull(switchName, taskItem.ItemSpec);
							}
							else
							{
								commandLine.AppendSwitchAliased(switchName, text2, taskItem.ItemSpec);
							}
						}
					}
				}
			}
		}
		private static bool IsLegalIdentifier(string identifier)
		{
			if (identifier.Length == 0)
			{
				return false;
			}
			if (!TokenChar.IsLetter(identifier[0]) && identifier[0] != '_')
			{
				return false;
			}
			for (int i = 1; i < identifier.Length; i++)
			{
				char c = identifier[i];
				if (!TokenChar.IsLetter(c) && !TokenChar.IsDecimalDigit(c) && !TokenChar.IsConnecting(c) && !TokenChar.IsCombining(c) && !TokenChar.IsFormatting(c))
				{
					return false;
				}
			}
			return true;
		}
		private bool InitializeHostCompiler(ICscHostObject cscHostObject)
		{
			base.HostCompilerSupportsAllParameters = this.UseHostCompilerIfAvailable;
			string text = "Unknown";
			try
			{
				text = "LinkResources";
				base.CheckHostObjectSupport(text, cscHostObject.SetLinkResources(base.LinkResources));
				text = "References";
				base.CheckHostObjectSupport(text, cscHostObject.SetReferences(base.References));
				text = "Resources";
				base.CheckHostObjectSupport(text, cscHostObject.SetResources(base.Resources));
				text = "Sources";
				base.CheckHostObjectSupport(text, cscHostObject.SetSources(base.Sources));
			}
			catch (Exception ex)
			{
				if (ExceptionHandling.IsCriticalException(ex))
				{
					throw;
				}
				if (base.HostCompilerSupportsAllParameters)
				{
					base.Log.LogErrorWithCodeFromResources("General.CouldNotSetHostObjectParameter", new object[]
					{
						text,
						ex.Message
					});
				}
				bool result = false;
				return result;
			}
			bool flag;
			try
			{
				text = "BeginInitialization";
				cscHostObject.BeginInitialization();
				text = "AdditionalLibPaths";
				base.CheckHostObjectSupport(text, cscHostObject.SetAdditionalLibPaths(base.AdditionalLibPaths));
				text = "AddModules";
				base.CheckHostObjectSupport(text, cscHostObject.SetAddModules(base.AddModules));
				text = "AllowUnsafeBlocks";
				base.CheckHostObjectSupport(text, cscHostObject.SetAllowUnsafeBlocks(this.AllowUnsafeBlocks));
				text = "BaseAddress";
				base.CheckHostObjectSupport(text, cscHostObject.SetBaseAddress(this.BaseAddress));
				text = "CheckForOverflowUnderflow";
				base.CheckHostObjectSupport(text, cscHostObject.SetCheckForOverflowUnderflow(this.CheckForOverflowUnderflow));
				text = "CodePage";
				base.CheckHostObjectSupport(text, cscHostObject.SetCodePage(base.CodePage));
				text = "EmitDebugInformation";
				base.CheckHostObjectSupport(text, cscHostObject.SetEmitDebugInformation(base.EmitDebugInformation));
				text = "DebugType";
				base.CheckHostObjectSupport(text, cscHostObject.SetDebugType(base.DebugType));
				text = "DefineConstants";
				base.CheckHostObjectSupport(text, cscHostObject.SetDefineConstants(this.GetDefineConstantsSwitch(base.DefineConstants)));
				text = "DelaySign";
				base.CheckHostObjectSupport(text, cscHostObject.SetDelaySign(base.Bag["DelaySign"] != null, base.DelaySign));
				text = "DisabledWarnings";
				base.CheckHostObjectSupport(text, cscHostObject.SetDisabledWarnings(this.DisabledWarnings));
				text = "DocumentationFile";
				base.CheckHostObjectSupport(text, cscHostObject.SetDocumentationFile(this.DocumentationFile));
				text = "ErrorReport";
				base.CheckHostObjectSupport(text, cscHostObject.SetErrorReport(this.ErrorReport));
				text = "FileAlignment";
				base.CheckHostObjectSupport(text, cscHostObject.SetFileAlignment(base.FileAlignment));
				text = "GenerateFullPaths";
				base.CheckHostObjectSupport(text, cscHostObject.SetGenerateFullPaths(this.GenerateFullPaths));
				text = "KeyContainer";
				base.CheckHostObjectSupport(text, cscHostObject.SetKeyContainer(base.KeyContainer));
				text = "KeyFile";
				base.CheckHostObjectSupport(text, cscHostObject.SetKeyFile(base.KeyFile));
				text = "LangVersion";
				base.CheckHostObjectSupport(text, cscHostObject.SetLangVersion(this.LangVersion));
				text = "MainEntryPoint";
				base.CheckHostObjectSupport(text, cscHostObject.SetMainEntryPoint(base.TargetType, base.MainEntryPoint));
				text = "ModuleAssemblyName";
				base.CheckHostObjectSupport(text, cscHostObject.SetModuleAssemblyName(this.ModuleAssemblyName));
				text = "NoConfig";
				base.CheckHostObjectSupport(text, cscHostObject.SetNoConfig(base.NoConfig));
				text = "NoStandardLib";
				base.CheckHostObjectSupport(text, cscHostObject.SetNoStandardLib(this.NoStandardLib));
				text = "Optimize";
				base.CheckHostObjectSupport(text, cscHostObject.SetOptimize(base.Optimize));
				text = "OutputAssembly";
				base.CheckHostObjectSupport(text, cscHostObject.SetOutputAssembly(base.OutputAssembly.ItemSpec));
				text = "PdbFile";
				base.CheckHostObjectSupport(text, cscHostObject.SetPdbFile(this.PdbFile));
				ICscHostObject4 cscHostObject2 = cscHostObject as ICscHostObject4;
				if (cscHostObject2 != null)
				{
					text = "PlatformWith32BitPreference";
					base.CheckHostObjectSupport(text, cscHostObject2.SetPlatformWith32BitPreference(base.PlatformWith32BitPreference));
					text = "HighEntropyVA";
					base.CheckHostObjectSupport(text, cscHostObject2.SetHighEntropyVA(base.HighEntropyVA));
					text = "SubsystemVersion";
					base.CheckHostObjectSupport(text, cscHostObject2.SetSubsystemVersion(base.SubsystemVersion));
				}
				else
				{
					text = "Platform";
					base.CheckHostObjectSupport(text, cscHostObject.SetPlatform(base.Platform));
				}
				text = "ResponseFiles";
				base.CheckHostObjectSupport(text, cscHostObject.SetResponseFiles(base.ResponseFiles));
				text = "TargetType";
				base.CheckHostObjectSupport(text, cscHostObject.SetTargetType(base.TargetType));
				text = "TreatWarningsAsErrors";
				base.CheckHostObjectSupport(text, cscHostObject.SetTreatWarningsAsErrors(base.TreatWarningsAsErrors));
				text = "WarningLevel";
				base.CheckHostObjectSupport(text, cscHostObject.SetWarningLevel(this.WarningLevel));
				text = "WarningsAsErrors";
				base.CheckHostObjectSupport(text, cscHostObject.SetWarningsAsErrors(this.WarningsAsErrors));
				text = "WarningsNotAsErrors";
				base.CheckHostObjectSupport(text, cscHostObject.SetWarningsNotAsErrors(this.WarningsNotAsErrors));
				text = "Win32Icon";
				base.CheckHostObjectSupport(text, cscHostObject.SetWin32Icon(base.Win32Icon));
				if (cscHostObject is ICscHostObject2)
				{
					ICscHostObject2 cscHostObject3 = (ICscHostObject2)cscHostObject;
					text = "Win32Manifest";
					base.CheckHostObjectSupport(text, cscHostObject3.SetWin32Manifest(base.GetWin32ManifestSwitch(base.NoWin32Manifest, base.Win32Manifest)));
				}
				else
				{
					if (!string.IsNullOrEmpty(base.Win32Manifest))
					{
						base.CheckHostObjectSupport("Win32Manifest", false);
					}
				}
				text = "Win32Resource";
				base.CheckHostObjectSupport(text, cscHostObject.SetWin32Resource(base.Win32Resource));
				if (cscHostObject is ICscHostObject3)
				{
					ICscHostObject3 cscHostObject4 = (ICscHostObject3)cscHostObject;
					text = "ApplicationConfiguration";
					base.CheckHostObjectSupport(text, cscHostObject4.SetApplicationConfiguration(this.ApplicationConfiguration));
				}
				else
				{
					if (!string.IsNullOrEmpty(this.ApplicationConfiguration))
					{
						base.CheckHostObjectSupport("ApplicationConfiguration", false);
					}
				}
				if (!string.IsNullOrEmpty(this.PreferredUILang) && !string.Equals(this.PreferredUILang, CultureInfo.CurrentUICulture.Name, StringComparison.OrdinalIgnoreCase))
				{
					base.CheckHostObjectSupport("PreferredUILang", false);
				}
			}
			catch (Exception ex2)
			{
				if (ExceptionHandling.IsCriticalException(ex2))
				{
					throw;
				}
				if (base.HostCompilerSupportsAllParameters)
				{
					base.Log.LogErrorWithCodeFromResources("General.CouldNotSetHostObjectParameter", new object[]
					{
						text,
						ex2.Message
					});
				}
				bool result = false;
				return result;
			}
			finally
			{
				string text2;
				int num;
				flag = cscHostObject.EndInitialization(out text2, out num);
				if (base.HostCompilerSupportsAllParameters)
				{
					if (!flag)
					{
						base.Log.LogError(null, "CS" + num.ToString("D4", CultureInfo.InvariantCulture), null, null, 0, 0, 0, 0, text2, new object[0]);
					}
					else
					{
						if (text2 != null && text2.Length > 0)
						{
							base.Log.LogWarning(null, "CS" + num.ToString("D4", CultureInfo.InvariantCulture), null, null, 0, 0, 0, 0, text2, new object[0]);
						}
					}
				}
			}
			return flag;
		}
	}
}
