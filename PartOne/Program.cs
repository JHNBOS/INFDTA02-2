using PartOne.Components;
using PartOne.Helpers;
using PartOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            StartClustering(data, 3);

            Console.ReadKey();
        }

        private static void StartClustering(List<Vector> data, int k)
        {
            var kMeans = new KMeans();

            //Set data for k-means algorithm
            kMeans.Vectors = data;

            //Get three random vectors and add to array
            Point[] clusterCenters = new Point[k];
            for (int totalCenters = 0; totalCenters < k; totalCenters++)
            {
                clusterCenters[totalCenters] = kMeans.GetRandomPoints();
            }

            //Assign observations to cluster
            var clusters = kMeans.AssignObservations(clusterCenters);

            Console.WriteLine("Cluster one has " + clusters.Where(q => q.Centroid == clusterCenters[0]).ToList().Count + " points");
            Console.WriteLine("Cluster two has " + clusters.Where(q => q.Centroid == clusterCenters[1]).ToList().Count + " points");
            Console.WriteLine("Cluster three has " + clusters.Where(q => q.Centroid == clusterCenters[2]).ToList().Count + " points");

            //Console.WriteLine("Point 1 has cluster at position [" + clusters[0].Centroid.X + ", " + clusters[0].Centroid.Y + "] as center.");
            //Console.WriteLine("Point 2 has cluster at position [" + clusters[1].Centroid.X + ", " + clusters[1].Centroid.Y + "] as center.");
            //Console.WriteLine("Point 3 has cluster at position [" + clusters[2].Centroid.X + ", " + clusters[2].Centroid.Y + "] as center.");
            //Console.WriteLine("Point 4 has cluster at position [" + clusters[3].Centroid.X + ", " + clusters[3].Centroid.Y + "] as center.");
        }

    }
}
