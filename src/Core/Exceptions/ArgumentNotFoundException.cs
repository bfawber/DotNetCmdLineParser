using System;

namespace Core.Exceptions
{
    public class ArgumentNotFoundException : Exception
    {
		public ArgumentNotFoundException(string argumentName) : base($"{argumentName} was expected, but not found!")
		{

		}
    }
}
