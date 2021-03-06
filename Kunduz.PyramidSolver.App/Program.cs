﻿using System;
namespace Kunduz.PyramidSolver.App
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Say Hello");
            var reader = new PyramidReader(new FileHelper());
            var bottomSection = reader.GeneratePyramidSections("./100_satir.txt");
            var totalizer = new PyramidTotalizer();
            var total = totalizer.Totalize(bottomSection);
            Console.WriteLine(total);
            Console.ReadKey();
        }
    }
}
