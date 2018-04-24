using PartOne.Components;
using PartOne.Helpers;
using System;
using System.Linq;

namespace PartOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileReader = new FileReader();
            var kMeans = new KMeans();
            var data = fileReader.Parse(',', @"./Data/WineData.csv");

            //Set data for k-means algorithm
            kMeans.Vectors = data;

            //Get three random vectors
            var markerOne = kMeans.GetRandomPoints();
            var markerTwo = kMeans.GetRandomPoints();
            var markerThree = kMeans.GetRandomPoints();

            Console.WriteLine("Marker 1 at [" + markerOne.GetX() + ", " + markerOne.GetY() + "]");
            Console.WriteLine("Marker 2 at [" + markerTwo.GetX() + ", " + markerTwo.GetY() + "]");
            Console.WriteLine("Marker 3 at [" + markerThree.GetX() + ", " + markerThree.GetY() + "]");

            Console.ReadKey();
        }
    }
}
