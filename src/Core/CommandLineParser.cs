using Core.Factories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.Model;

namespace Core
{
	/// <summary>
	/// Parses the command line arguments
	/// </summary>
	public class CommandLineParser : ICommandLineParser
    {
		/// <summary>
		/// Holds a map of command line parameter name to the command line parameter class
		/// </summary>
		private readonly Dictionary<string, ICommandLineParameter> _commandLineParameters = new Dictionary<string, ICommandLineParameter>();

		/// <summary>
		/// A cache of the methods created while parsing the command line parameters
		/// </summary>
		private readonly Dictionary<Type, MethodInfo> _methodCache = new Dictionary<Type, MethodInfo>();

		/// <summary>
		/// Adds a new command line parameter to the parser's store
		/// </summary>
		/// <typeparam name="T">The type of the command line parameter</typeparam>
		/// <param name="name">The name of the command line parameter</param>
		/// <param name="prefix">The prefix expected before the command line parameter</param>
		/// <param name="separator">The separator between the command line parameter key and its value</param>
		/// <param name="isRequired">Is the command line parameter required</param>
		/// <param name="hasValue">Does it have a value</param>
		/// <param name="description">The description of the command line parameter</param>
		public void AddParameter<T>(string name, string prefix = "-", string separator = "=", bool isRequired = true, bool hasValue = true, string description = "")
		{
			_commandLineParameters.Add(name, CommandLineParameterFactory.Create<T>(name, prefix, separator, isRequired, hasValue, description));
		}

		/// <summary>
		/// Parses the command line arguments passed in to the object provided using the command line
		/// parameter store.
		/// </summary>
		/// <typeparam name="T">The type of the object to parse into</typeparam>
		/// <param name="args">The command line arguments</param>
		/// <returns>The object with the properties populated with the command line argument values</returns>
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
					MethodInfo genericMethod = typeof(CommandLineParser).GetRuntimeMethod(nameof(ConvertToType), new Type[] { typeof(object) });
					specificMethod = genericMethod.MakeGenericMethod(property.PropertyType);
					_methodCache.Add(property.PropertyType, specificMethod);
				}
				property.SetValue(parametersContainer, specificMethod.Invoke(this, new[] { cmdLineParameter.Value.Get(args) }));
			}
			

			return parametersContainer;
		}

		/// <summary>
		/// Gets the string that shows the command line parameter names next to their descriptions
		/// </summary>
		/// <returns>A string that shows the command line parameter names next to their descriptions</returns>
		public string GetHelpString()
	    {
			StringBuilder bob = new StringBuilder();
		    foreach (var parameter in _commandLineParameters)
		    {
			    bob.Append($"{parameter.Value.Prefix}{parameter.Key}: {parameter.Value.Description}");
		    }
		    return bob.ToString();
	    }

		/// <summary>
		/// Converts the object to it's concrete type
		/// </summary>
		/// <typeparam name="T">The type to convert the object to</typeparam>
		/// <param name="value">The value of the object</param>
		/// <returns>The object in its concrete form</returns>
		public T ConvertToType<T>(object value)
		{
			return (T)Convert.ChangeType(value, typeof(T));
		}
    }
}
