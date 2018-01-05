using System.IO;

namespace Kunduz.PyramidSolver.App
{
    /// <summary>
    /// Wrapper class for static File class. Directly maps to the static methods of the wrapped class.
    /// </summary>
    public class FileHelper : IFileHelper
    {
        public string ReadAllText(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
