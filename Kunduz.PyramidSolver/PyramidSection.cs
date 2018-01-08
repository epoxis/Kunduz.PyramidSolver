using System;
using System.Collections.Generic;

namespace Kunduz.PyramidSolver
{
    /// <summary>
    /// Pyramid section of an integer pyramid. Sections are linked from bottom to top.
    /// </summary>
    public class PyramidSection : IPyramidSection
    {
        private readonly IPyramidSection _previous;
        public IReadOnlyList<int> Values { get; }
        public IPyramidSection Previous { get { return _previous; } }
        public int Step { get; }
        /// <summary>
        /// public constructor for the pyramid. This is the top section of the pyramid and can only contain a single value.
        /// </summary>
        /// <param name="value"></param>
        public PyramidSection(int value)
        {
            Values = new int[] { value };
            Step = 1;
        }
        /// <summary>
        /// private constructor for linking cotinuous sections of the pyramid
        /// </summary>
        /// <param name="array">value array</param>
        /// <param name="prevSection">previous section that created this section</param>
        private PyramidSection(int[] array, PyramidSection prevSection)
        {
            Values = array;
            Step = array.Length;
            _previous = prevSection;
        }
        /// <summary>
        /// Creates the next section on the pyramid. Returns an exception if the value array is of the wrong size.
        /// </summary>
        /// <param name="array">value array of the next section on the pyramid</param>
        /// <returns>the next section of the pyramid</returns>
        public IPyramidSection CreateNext(int[] array)
        {
            if (array.Length != Step + 1) throw new ArgumentException($"Expected array size: {Step + 1}, received array size: {array.Length}");
            var newSection = new PyramidSection(array, this);

            return newSection;
        }
    }
}
