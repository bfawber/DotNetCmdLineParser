﻿using Core.Exceptions;
using System;

namespace Core
{
	public class CommandLineParameter
	{
		public Type Type { get; set; }

		public string Prefix { get; set; }

		public string Separator { get; set; }

		public string Description { get; set; }

		public string Name { get; set; }
		
		public bool Required { get; set; }

		public bool HasValue { get; set; }

		public virtual object Get(string[] args)
		{
			return GetValue(args);
		}
		
		protected virtual string GetValue(string[] args)
		{
			string nameWithPrefix = Prefix + Name;
			string value = null;

			for(int i=0; i < args.Length; i++)
			{
				string[] splitByEquals = args[i].Split(Separator.ToCharArray());
				if(string.Equals(splitByEquals[0], nameWithPrefix, StringComparison.OrdinalIgnoreCase))
				{
					if(splitByEquals.Length == 2)
					{
						value = splitByEquals[1];
					}
					else if(Required)
					{
						throw new ArgumentWithoutValueException(Name);
					}
				}
			}

			if(Required && value == null)
			{
				throw new ArgumentNotFoundException(Name);
			}

			return value;
		}

		protected virtual bool IsPresent(string[] args)
		{
			string nameWithPrefix = Prefix + Name;
			for (int i = 0; i < args.Length; i++)
			{
				if (string.Equals(args[i], nameWithPrefix, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}

			return false;
		}
		
	}
}