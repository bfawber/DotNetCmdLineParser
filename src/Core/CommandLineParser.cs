using Core.Factories;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core
{
	public class CommandLineParser
    {
		private Dictionary<string, CommandLineParameter> commandLineParameters = new Dictionary<string, CommandLineParameter>();

		public CommandLineParser()
		{
		}

		public void AddParameter<T>(string name, string prefix = "-", string separator = "=", bool isRequired = true, bool hasValue = true, string description = "")
		{
			commandLineParameters.Add(name, CommandLineParameterFactory.Create<T>(name, prefix, separator, isRequired, hasValue, description));
		}

		public T Parse<T>(string[] args) where T : new()
		{
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

		public T ConvertToType<T>(string value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
    }
}
