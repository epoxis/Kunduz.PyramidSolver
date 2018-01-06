namespace Kunduz.PyramidSolver.App
{
    public interface IFileHelper
    {
        string ReadAllText(string path);
        bool FileExists(string path);
    }
}