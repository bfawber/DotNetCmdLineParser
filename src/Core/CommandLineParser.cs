using Core.Factories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.Model;

namespace Core
{
	public class CommandLineParser : ICommandLineParser
    {
		private readonly Dictionary<string, CommandLineParameter> _commandLineParameters = new Dictionary<string, CommandLineParameter>();

		private readonly Dictionary<Type, MethodInfo> _methodCache = new Dictionary<Type, MethodInfo>();

		public void AddParameter<T>(string name, string prefix = "-", string separator = "=", bool isRequired = true, bool hasValue = true, string description = "")
		{
			_commandLineParameters.Add(name, CommandLineParameterFactory.Create<T>(name, prefix, separator, isRequired, hasValue, description));
		}

		public T Parse<T>(string[] args) where T : new()
		{
			T parametersContainer = new T();
			Type typeOfT = typeof(T);

			// Find value for each entry in dictionary
			foreach(var cmdLineParameter in _commandLineParameters)
			{
				PropertyInfo property = typeOfT.GetRuntimeProperty(cmdLineParameter.Key);
				MethodInfo specificMethod;

				if(!_methodCache.TryGetValue(property.PropertyType, out specificMethod))
				{
					MethodInfo genericMethod = typeof(CommandLineParser).GetRuntimeMethod(nameof(ConvertToType), new Type[] { typeof(string) });
					specificMethod = genericMethod.MakeGenericMethod(property.PropertyType);
					_methodCache.Add(property.PropertyType, specificMethod);
				}
				property.SetValue(parametersContainer, specificMethod.Invoke(this, new[] { cmdLineParameter.Value.Get(args) }));
			}
			

			return parametersContainer;
		}
	   
	    public string GetHelpString()
	    {
			StringBuilder bob = new StringBuilder();
		    foreach (var parameter in _commandLineParameters)
		    {
			    bob.Append($"{parameter.Value.Prefix}{parameter.Key}: {parameter.Value.Description}");
		    }
		    return bob.ToString();
	    }

		public T ConvertToType<T>(string value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
    }
}
