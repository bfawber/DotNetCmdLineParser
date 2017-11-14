using System;
using DotNetCommandLineParser.Core.Exceptions;

namespace DotNetCommandLineParser.Core.Model
{
	/// <summary>
	/// A class to represent a command line parameter
	/// </summary>
	public class CommandLineParameter : ICommandLineParameter
	{
		/// <summary>
		/// The type of the command line parameter's value
		/// </summary>
		public Type Type { get; set; }

		/// <summary>
		/// The prefix expected before the command line parameter's key
		/// </summary>
		public string Prefix { get; set; }

		/// <summary>
		/// The separator between the key and value
		/// </summary>
		public string Separator { get; set; }

		/// <summary>
		/// The description of the command line parameter
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The name of the command line parameter
		/// </summary>
		public string Name { get; set; }
		
		/// <summary>
		/// Whether the command line parameter is required or not
		/// </summary>
		public bool Required { get; set; }

		/// <summary>
		/// Gets the value of a command line parameter in a argument array
		/// </summary>
		/// <param name="args">The arguments passed in</param>
		/// <returns>The value of the command line parameter</returns>
		public virtual object Get(string[] args)
		{
			return GetValue(args);
		}
		
		/// <summary>
		/// Gets the value of a command line parameter in a argument array
		/// </summary>
		/// <param name="args">the argument array</param>
		/// <returns>The value of the parameter</returns>
		protected virtual string GetValue(string[] args)
		{
			// Get the string that the parameter will look like with the prefix in front of it
			string nameWithPrefix = Prefix + Name;
			string value = null;

			// Spin through all arguments to find the command line parameter and attempt to get its value
			for(int i=0; i < args.Length; i++)
			{
				string[] valueSplit = args[i].Split(Separator.ToCharArray());
				if(string.Equals(valueSplit[0].Trim(), nameWithPrefix, StringComparison.OrdinalIgnoreCase))
				{
					if(valueSplit.Length == 2)
					{
						value = valueSplit[1].Trim();
						if (string.IsNullOrEmpty(value))
						{
							throw new ArgumentWithoutValueException(Name);
						}
						break;
					}
					else if(Required)
					{
						throw new ArgumentWithoutValueException(Name);
					}
				}
			}

			// Argument not found
			if(Required && value == null)
			{
				throw new ArgumentNotFoundException(Name);
			}

			return value;
		}

		/// <summary>
		/// Is the argument present in the arguments array
		/// </summary>
		/// <param name="args">The arguments array</param>
		/// <returns></returns>
		protected virtual bool IsPresent(string[] args)
		{
			// Get the string that the parameter will look like with the prefix in front of it
			string nameWithPrefix = Prefix + Name;

			// Spin through all arguments to find the command line parameter
			for (int i = 0; i < args.Length; i++)
			{
				if (string.Equals(args[i].Trim(), nameWithPrefix, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}
		
	}
}
