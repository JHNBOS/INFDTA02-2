using Assignment1.Components;
using Assignment1.Components.Algorithmes;
using System;

namespace Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var data = parser.Parse(',', @"./Data/WineData.csv");

            var iterations = 5;
            var clusters = 4;
            
            var kMeans = new Kmeans(data, iterations, clusters);
            kMeans.Run(50);

            Console.Read();
        }
    }
}
