using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class ArgumentWithoutValueException : Exception
    {
		public ArgumentWithoutValueException(string argumentName) : base($"Argument {argumentName} found, but had no value!")
		{

		}
    }
}
