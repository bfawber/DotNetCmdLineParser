namespace Core.Factories
{
	public static class CommandLineParameterFactory
	{
		public static CommandLineParameter Create<T>(string name, string prefix, string separator, bool isRequired, bool hasValue, string description)
		{
			if (hasValue)
			{
				return new CommandLineParameter
				{
					Name = name,
					Type = typeof(T),
					Prefix = prefix,
					Required = isRequired,
					HasValue = true,
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
					HasValue = false,
					Description = description,
				};
			}

		}

    }
}
