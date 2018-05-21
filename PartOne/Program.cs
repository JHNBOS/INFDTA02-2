using PartOne.Components;
using PartOne.Helpers;
using PartOne.Models;
using System;
using System.Collections.Generic;

namespace PartOne
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileReader = new FileReader();

            //Read file
            var data = fileReader.Parse(',', @"./Data/WineData.csv");

            //Start clustering
            StartClustering(data, 4, 5);

            Console.ReadKey();
        }

        private static void StartClustering(List<Vector> data, int clusters, int loops)
        {
            var kMeans = new KMeans(data, clusters, loops);

            ////Assign observations to cluster
            kMeans.AssignObservations();

            var vectors = kMeans.GetVectors();

            Console.WriteLine("Point 1 has cluster at position [" + vectors[0].GetCentroid() + "as center.");
            Console.WriteLine("Point 2 has cluster at position [" + vectors[1].GetCentroid() + "as center.");
            Console.WriteLine("Point 3 has cluster at position [" + vectors[2].GetCentroid() + "as center.");
            Console.WriteLine("Point 4 has cluster at position [" + vectors[3].GetCentroid() + "as center.");
        }

    }
}
