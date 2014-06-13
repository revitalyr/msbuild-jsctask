using Microsoft.Build.Shared;
using Microsoft.Build.Utilities;
using System;
using System.Collections;
using System.Runtime;
namespace Microsoft.Build.Tasks
{
	/// <summary>Comprises extended utility methods for constructing a task that wraps a command line tool.</summary>
	public abstract class ToolTaskExtension : ToolTask
	{
		private TaskLoggingHelperExtension logExtension;
		private Hashtable bag = new Hashtable();
		/// <summary>Gets an instance of a <see cref="T:Microsoft.Build.Tasks.TaskLoggingHelperExtension" /> class containing task logging methods.</summary>
		/// <returns>An instance of a <see cref="T:Microsoft.Build.Tasks.TaskLoggingHelperExtension" /> class containing task logging methods.</returns>
		public new TaskLoggingHelper Log
		{
			[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
			get
			{
				return this.logExtension;
			}
		}
		/// <summary>Returns whether this ToolTask has logged any errors.</summary>
		/// <returns>true if the ToolTask logged errors, false if otherwise.</returns>
		protected override bool HasLoggedErrors
		{
			get
			{
				return this.Log.HasLoggedErrors || base.HasLoggedErrors;
			}
		}
		/// <summary>Gets the collection of parameters used by the derived task class.</summary>
		/// <returns>The collection of parameters used by the derived task class.</returns>
		protected internal Hashtable Bag
		{
			[TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
			get
			{
				return this.bag;
			}
		}
		internal ToolTaskExtension() : base(AssemblyResources.PrimaryResources, "MSBuild.")
		{
			this.logExtension = new TaskLoggingHelperExtension(this, AssemblyResources.PrimaryResources, AssemblyResources.SharedResources, "MSBuild.");
		}
		/// <summary>Gets the value of the specified Boolean parameter.</summary>
		/// <returns>The parameter value.</returns>
		/// <param name="parameterName">The name of the parameter to return.</param>
		/// <param name="defaultValue">The value to return if <paramref name="parameterName" /> does not exist in the <see cref="P:Microsoft.Build.Tasks.ToolTaskExtension.Bag" />.</param>
		protected internal bool GetBoolParameterWithDefault(string parameterName, bool defaultValue)
		{
			object obj = this.bag[parameterName];
			if (obj != null)
			{
				return (bool)obj;
			}
			return defaultValue;
		}
		/// <summary>Gets the value of the specified integer parameter.</summary>
		/// <returns>The parameter value.</returns>
		/// <param name="parameterName">The name of the parameter to return.</param>
		/// <param name="defaultValue">The value to return if <paramref name="parameterName" /> does not exist in the <see cref="P:Microsoft.Build.Tasks.ToolTaskExtension.Bag" />.</param>
		protected internal int GetIntParameterWithDefault(string parameterName, int defaultValue)
		{
			object obj = this.bag[parameterName];
			if (obj != null)
			{
				return (int)obj;
			}
			return defaultValue;
		}
		/// <summary>Gets the switch used by the command line tool to specify the response file.</summary>
		/// <returns>The switch used by the command line tool to specify the response file.</returns>
		protected override string GenerateResponseFileCommands()
		{
			CommandLineBuilderExtension commandLineBuilderExtension = new CommandLineBuilderExtension();
			this.AddResponseFileCommands(commandLineBuilderExtension);
			return commandLineBuilderExtension.ToString();
		}
		/// <summary>Gets the switches and other information that the command line tool must run directly from the command line and not from a response file.</summary>
		/// <returns>A string containing the switches and other information that the command line tool must run directly from the command line and not from a response file.</returns>
		protected override string GenerateCommandLineCommands()
		{
			CommandLineBuilderExtension commandLineBuilderExtension = new CommandLineBuilderExtension();
			this.AddCommandLineCommands(commandLineBuilderExtension);
			return commandLineBuilderExtension.ToString();
		}
		/// <summary>Fills the specified <see cref="T:Microsoft.Build.Tasks.CommandLineBuilderExtension" /> with the switches and other information that the command line tool can run from a response file.</summary>
		/// <param name="commandLine">The <see cref="T:Microsoft.Build.Tasks.CommandLineBuilderExtension" /> to fill.</param>
		protected internal virtual void AddResponseFileCommands(CommandLineBuilderExtension commandLine)
		{
		}
		/// <summary>Fills the specified <see cref="T:Microsoft.Build.Tasks.CommandLineBuilderExtension" /> with the switches and other information that the command line tool must run from the command line and not from a response file.</summary>
		/// <param name="commandLine">The <see cref="T:Microsoft.Build.Tasks.CommandLineBuilderExtension" /> to fill.</param>
		protected internal virtual void AddCommandLineCommands(CommandLineBuilderExtension commandLine)
		{
		}
	}
}
