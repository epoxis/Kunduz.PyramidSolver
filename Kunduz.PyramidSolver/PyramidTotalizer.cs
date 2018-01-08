using System;
using System.Collections.Generic;

namespace Kunduz.PyramidSolver
{
    /// <summary>
    /// Finds the maximum sum of the pyramid
    /// </summary>
    public class PyramidTotalizer : IPyramidTotalizer
    {
        private static IReadOnlyList<int> CrossSumArrays(IReadOnlyList<int> bottomArray, IReadOnlyList<int> topArray)
        {
            if (bottomArray.Count != topArray.Count + 1) throw new ArgumentException();            
            var topArrayLength = topArray.Count;
            var resultArray = new int[topArrayLength];

            for (int i = 0; i < topArrayLength; i++)
            {
                resultArray[i] = Math.Max(bottomArray[i] + topArray[i], bottomArray[i + 1] + topArray[i]);
            }

            return resultArray;
        }
        /// <summary>
        /// returns the maximum sum of the pyramid. Starts from the bottom section and continues until it reaches the top.
        /// </summary>
        /// <param name="bottomSection">bottom section of the pyramid</param>
        /// <returns>maximum sum</returns>
        public int Totalize(IPyramidSection bottomSection)
        {
            var nextSection = bottomSection.Previous;
            var totalizedValues = CrossSumArrays(bottomSection.Values, nextSection.Values);
            while (nextSection.Previous != null)
            {
                totalizedValues = CrossSumArrays(totalizedValues, nextSection.Previous.Values);
                nextSection = nextSection.Previous;
            }
            if (totalizedValues.Count != 1) throw new InvalidOperationException("Something went wrong");
            return totalizedValues[0];
        }
    }
}
