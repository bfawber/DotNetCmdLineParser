using System;

namespace Core.Model
{
	/// <summary>
	/// A type of command line parameter that doesn't have a value.
	/// It just matters if it is present in the arguments string or not.
	/// </summary>
	public class CommandLineFlag : CommandLineParameter
    {
		/// <summary>
		/// Get whether the flag is in the argument string
		/// </summary>
		/// <param name="args">The arguments passed in</param>
		/// <returns>true if it's present, false if it isn't</returns>
		public override object Get(string[] args)
		{
			return IsPresent(args);
		}

		/// <summary>
		/// Shouldn't be used by a command line flag object!
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		protected override string GetValue(string[] args)
		{
			throw new ArgumentException($"{Name} parameter is a valueless parameter. Do not try and get it's string value.");
		}
	}
}
