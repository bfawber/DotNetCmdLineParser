namespace Core.Model
{
    public interface ICommandLineParameter
    {
	    object Get(string[] args);
    }
}
