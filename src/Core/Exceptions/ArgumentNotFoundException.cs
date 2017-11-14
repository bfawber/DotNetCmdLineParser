using System;

namespace DotNetCommandLineParser.Core.Exceptions
{
	/// <summary>
	/// An exception class for when a required argument is not found in the arguments string
	/// </summary>
    public class ArgumentNotFoundException : Exception
    {
		/// <summary>
		/// Creates a new instance of a <see cref="ArgumentNotFoundException"/>
		/// </summary>
		/// <param name="argumentName">The name of the argument that was expected</param>
		public ArgumentNotFoundException(string argumentName) : base($"{argumentName} was expected, but not found!")
		{

		}
    }
}
