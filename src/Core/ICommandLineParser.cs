namespace Core
{
    public interface ICommandLineParser
    {
	    void AddParameter<T>(string name, string prefix = "-", string separator = "=", bool isRequired = true, bool hasValue = true, string description = "");

	    T Parse<T>(string[] args) where T : new();

	    string GetHelpString();
    }
}
