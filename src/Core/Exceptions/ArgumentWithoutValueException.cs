using System;

namespace Core.Exceptions
{
	/// <summary>
	/// An exception class for when an argument requires a value, but is not provided one
	/// </summary>
    public class ArgumentWithoutValueException : Exception
    {
		/// <summary>
		/// Creates a new instance of a <see cref="ArgumentWithoutValueException"/>
		/// </summary>
		/// <param name="argumentName">The name of the argument that was missing a value</param>
		public ArgumentWithoutValueException(string argumentName) : base($"Argument {argumentName} found, but had no value!")
		{

		}
    }
}
