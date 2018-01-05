using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kunduz.PyramidSolver.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Say Hello");
            var reader = new PyramidReader("./100_satir.txt", new FileHelper());
            var bottomSection = reader.GeneratePyramidSections();
            var totalizer = new PyramidTotalizer();
            var total = totalizer.Totalize(bottomSection);
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
