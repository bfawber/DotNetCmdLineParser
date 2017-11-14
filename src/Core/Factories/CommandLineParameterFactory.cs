using DotNetCommandLineParser.Core.Model;

namespace DotNetCommandLineParser.Core.Factories
{
	/// <summary>
	/// A factory for creating <see cref="CommandLineParameter"/> objects
	/// </summary>
	public static class CommandLineParameterFactory
	{
		/// <summary>
		/// Creates a new <see cref="CommandLineParameter"/> based on the inputs provided
		/// </summary>
		/// <typeparam name="T">The type of the command line parameter</typeparam>
		/// <param name="name">The name of the command line parameter</param>
		/// <param name="prefix">The prefix expected before the command line parameter</param>
		/// <param name="separator">The separator between the command line parameter key and its value</param>
		/// <param name="isRequired">Is the command line parameter required</param>
		/// <param name="hasValue">Does it have a value</param>
		/// <param name="description">The description of the command line parameter</param>
		/// <returns>A new <see cref="CommandLineParameter"/> based on the inputs provided </returns>
		public static CommandLineParameter Create<T>(string name, string prefix = "-", string separator = "=", bool isRequired = true, bool hasValue = true, string description = "")
		{
			if (hasValue)
			{
				return new CommandLineParameter
				{
					Name = name,
					Type = typeof(T),
					Prefix = prefix,
					Required = isRequired,
					Description = description,
					Separator = separator,
				};
			}
			else
			{
				return new CommandLineFlag
				{

					Name = name,
					Type = typeof(T),
					Prefix = prefix,
					Required = isRequired,
					Description = description,
				};
			}

		}

    }
}
