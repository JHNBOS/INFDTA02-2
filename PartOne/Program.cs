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
        }

    }
}
