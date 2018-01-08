using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Kunduz.PyramidSolver.App
{
    /// <summary>
    /// Reader class for reading input from file system.
    /// </summary>
    public class PyramidReader : IPyramidReader
    {
        private readonly IFileHelper _fileHelper;
        /// <param name="fileHelper">file helper class for system.io operations</param>
        /// <exception cref="ArgumentNullException">Thrown if file helper is null</exception>
        public PyramidReader(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper ?? throw new ArgumentNullException(nameof(fileHelper));
        }
        /// <summary>
        /// Generates linked pyramid sections and returns the bottom one.
        /// </summary>
        /// <returns></returns>
        public IPyramidSection GeneratePyramidSections(string path)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (!_fileHelper.FileExists(path)) throw new FileNotFoundException($"File does not exists at path {path}");

            var file = _fileHelper.ReadAllText(path).TrimEnd().TrimStart();
            var lines = SplitIntoLines(file);
            var stack = lines.Select(l => SplitIntoNumbers(l));
            var enumerator = stack.GetEnumerator();
            enumerator.MoveNext();
            var firstSection = enumerator.Current;
            if (firstSection.Count() != 1) throw new ArgumentException();
            IPyramidSection currentSection = new PyramidSection(firstSection.First());
            while (enumerator.MoveNext())
            {
                var section = enumerator.Current;
                currentSection = currentSection.CreateNext(section.ToArray());
            }
            return currentSection;
        }
        private static IEnumerable<int> SplitIntoNumbers(string line)
        {
            foreach (var character in line.Split(' '))
            {
                if (!int.TryParse(character, out int number)) throw new ArgumentException();
                yield return number;
            }
        }
        private static  IEnumerable<string> SplitIntoLines(string file)
        {
            foreach (var line in file.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                yield return line;
            }
        }
    }
}
