using Core.Factories;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core
{
	public class CommandLineParser
    {
		private Dictionary<string, CommandLineParameter> commandLineParameters = new Dictionary<string, CommandLineParameter>();
	    private readonly CommandLineFlag _helpCommandLineFlag;

		public CommandLineParser()
		{
			_helpCommandLineFlag = (CommandLineFlag)CommandLineParameterFactory.Create<bool>(
				name: "help",
				prefix:"--",
				isRequired: false, 
				hasValue:false, 
				description: "Shows all command line options"
			);
		}

		public void AddParameter<T>(string name, string prefix = "-", string separator = "=", bool isRequired = true, bool hasValue = true, string description = "")
		{
			commandLineParameters.Add(name, CommandLineParameterFactory.Create<T>(name, prefix, separator, isRequired, hasValue, description));
		}

		public T Parse<T>(string[] args) where T : new()
		{
			if (args == null || HandleHelpParameter(args))
			{
				return new T();
			}

			T parametersContainer = new T();
			Type typeOfT = typeof(T);

			// Find value for each entry in dictionary
			foreach(var cmdLineParameter in commandLineParameters)
			{
				PropertyInfo property = typeOfT.GetRuntimeProperty(cmdLineParameter.Key);
				MethodInfo genericMethod = typeof(CommandLineParser).GetRuntimeMethod(nameof(ConvertToType), new Type[] { typeof(string) });
				MethodInfo specificMethod = genericMethod.MakeGenericMethod(property.PropertyType);
				property.SetValue(parametersContainer, specificMethod.Invoke(this, new[] { cmdLineParameter.Value.Get(args) }));
			}
			

			return parametersContainer;
		}

	    private bool HandleHelpParameter(string[] args)
	    {
		    if ((bool)Convert.ChangeType(_helpCommandLineFlag.Get(args), typeof(bool)))
		    {
			    PrintHelpStatement();
			    return true;
		    }

		    return false;
	    }

	    private void PrintHelpStatement()
	    {
		    foreach (var parameter in commandLineParameters)
		    {
			    Console.WriteLine($"{parameter.Value.Prefix}{parameter.Key}: {parameter.Value.Description}");
		    }
	    }

		public T ConvertToType<T>(string value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
    }
}
