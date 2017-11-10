using System;

namespace Core.Model
{
	public class CommandLineFlag : CommandLineParameter
    {
		public override object Get(string[] args)
		{
			return IsPresent(args);
		}

		protected override string GetValue(string[] args)
		{
			throw new ArgumentException($"{Name} parameter is a valueless parameter. Do not try and get it's string value.");
		}
	}
}
