using System;

namespace Core.Exceptions
{
    public class ArgumentWithoutValueException : Exception
    {
		public ArgumentWithoutValueException(string argumentName) : base($"Argument {argumentName} found, but had no value!")
		{

		}
    }
}
