using System.Collections.Generic;

namespace Kunduz.PyramidSolver
{
    public interface IPyramidSection
    {
        int Step { get; }
        IReadOnlyList<int> Values { get; }
        IPyramidSection Previous { get; }
        IPyramidSection CreateNext(int[] array);
    }
}
