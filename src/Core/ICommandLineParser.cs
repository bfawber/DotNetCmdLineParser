namespace Core
{
	/// <summary>
	/// An interface that holds methods needed for command line parsing
	/// </summary>
    public interface ICommandLineParser
	{
		/// <summary>
		/// Adds a new command line parameter to the parser's store
		/// </summary>
		/// <typeparam name="T">The type of the command line parameter</typeparam>
		/// <param name="name">The name of the command line parameter</param>
		/// <param name="prefix">The prefix expected before the command line parameter</param>
		/// <param name="separator">The separator between the command line parameter key and its value</param>
		/// <param name="isRequired">Is the command line parameter required</param>
		/// <param name="hasValue">Does it have a value</param>
		/// <param name="description">The description of the command line parameter</param>
		void AddParameter<T>(string name, string prefix = "-", string separator = "=", bool isRequired = true, bool hasValue = true, string description = "");

		/// <summary>
		/// Parses the command line arguments passed in to the object provided using the command line
		/// parameter store.
		/// </summary>
		/// <typeparam name="T">The type of the object to parse into</typeparam>
		/// <param name="args">The command line arguments</param>
		/// <returns>The object with the properties populated with the command line argument values</returns>
		T Parse<T>(string[] args) where T : new();

		/// <summary>
		/// Gets the string that shows the command line parameter names next to their descriptions
		/// </summary>
		string GetHelpString();
    }
}
