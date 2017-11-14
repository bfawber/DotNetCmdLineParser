namespace DotNetCommandLineParser.Core.Model
{
	/// <summary>
	/// An interface that all command line parameters should implement
	/// </summary>
    public interface ICommandLineParameter
    {
		string Prefix { get; }

		string Description { get; }

	    object Get(string[] args);
    }
}
