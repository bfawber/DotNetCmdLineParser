using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Exceptions
{
    public class ArgumentNotFoundException : Exception
    {
		public ArgumentNotFoundException(string argumentName) : base($"{argumentName} was expected, but not found!")
		{

		}
    }
}
