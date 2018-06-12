using Assignment2.Components.Algorithms;
using System;

namespace Assignment2
{
    class Program
    {
        static void Main(string[] args)
        {
            var geneticAlgoritm = new GeneticAlgorithm(0.85, 0.01, true, 10, 50);
            geneticAlgoritm.Run();

            Console.ReadKey();
        }
    }
}
