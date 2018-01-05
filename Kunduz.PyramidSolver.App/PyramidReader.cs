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
        private readonly string _file;
        public PyramidReader(string path, IFileHelper fileHelper)
        {
            _file = fileHelper.ReadAllText(path).TrimEnd().TrimStart();
        }
        /// <summary>
        /// Generates linked pyramid sections and returns the bottom one.
        /// </summary>
        /// <returns></returns>
        public IPyramidSection GeneratePyramidSections()
        {
            var lines = SplitIntoLines(_file);
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
        private IEnumerable<int> SplitIntoNumbers(string line)
        {
            foreach (var character in line.Split(' '))
            {
                if (!int.TryParse(character, out int number)) throw new ArgumentException();
                yield return number;
            }
        }
        private IEnumerable<string> SplitIntoLines(string file)
        {
            foreach (var line in _file.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries))
            {
                yield return line;
            }
        }
    }
}
