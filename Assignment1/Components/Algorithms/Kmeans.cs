using Assignment1.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1.Components.Algorithms
{
    public class Kmeans
    {
        private List<Vector> Data { get; set; }
        private List<Vector> Centroids { get; set; }
        private Euclidian euclidianAlgorithm;

        private int Clusters { get; set; }
        private int Iterations { get; set; }
        private double SSE { get; set; }

        public Kmeans(List<Vector> data, int iterations, int cluster)
        {
            this.Data = data;
            this.Iterations = iterations;
            this.Clusters = cluster;

            this.Centroids = new List<Vector>();
            this.euclidianAlgorithm = new Euclidian();
        }

        public void Run()
        {
            this.GenerateCentroids();

            for (int iteration = 0; iteration < this.Iterations; iteration++)
            {
                var oldCentroids = this.Centroids;

                // Assign observation to centroid
                foreach (var observation in this.Data)
                {
                    this.AssignToCentroid(observation);
                }

                // Recompute centroids
                this.RecomputeCentroids();

                // Check if program should stop or continue
                if (StoppedChanging(oldCentroids))
                {
                    Console.WriteLine("K-means algorithm completed in " + iteration + " iterations.");
                    break;
                }
            }
        }

        private void GenerateCentroids()
        {
            var random = new Random();
            while (this.Centroids.Count != this.Clusters)
            {
                var randomIndex = random.Next(0, this.Data.Count);
                var centroid = this.Data.ElementAtOrDefault(randomIndex);

                if (!this.Centroids.Contains(centroid))
                {
                    this.Centroids.Add(centroid);
                }
            }
        }

        private void AssignToCentroid(Vector vector)
        {
            var lowestDistance = double.MaxValue;
            for (int cluster = 0; cluster < this.Clusters; cluster++)
            {
                var centroidPoint = this.Centroids[cluster];

                // Calculate Euclidian distance
                var distance = this.euclidianAlgorithm.Calculate(vector, centroidPoint);

                // Check if distance is lower than lowestDistance variable, then assign this centroid to vector
                if (!vector.Distance.HasValue || distance < lowestDistance)
                {
                    lowestDistance = distance;
                    vector.Centroid = cluster;
                    vector.Distance = distance;
                }
            }
        }

        private void RecomputeCentroids()
        {
            int centroid = 0;
            var newCentroid = new Vector(this.Data.First().Points.Count);
            var cluster = this.Data.Where(q => q.Centroid == centroid).ToList();

            for (int j = 0; j < cluster.Count; j++)
            {
                newCentroid.Points[j] += cluster[centroid].Points[j];
            }
            for (int k = 0; k < newCentroid.Points.Count; k++)
            {
                newCentroid.Points[k] = newCentroid.Points[k] / cluster.Count;
            }

            this.Centroids[centroid] = newCentroid;

            //var clusterSet = this.Data.Where(vector => vector.Centroid == centroid).ToList();
            //var newCluster = new Vector(this.Data.First().Points.Count);
            //newCluster = SumCluster(newCluster, clusterSet);
            //newCluster = DivideCluster(newCluster, clusterSet.Count());
            //this.Centroids[centroid] = newCluster;
        }

        private bool StoppedChanging(List<Vector> oldCentroids)
        {
            var stopped = false;
            if (this.Centroids.Except(oldCentroids).ToList() == this.Centroids)
            {
                stopped = true;
            }

            return stopped;
        }

        private double CalculateSSE(int centroid)
        {
            double SSE = 0;
            var clusterPoints = this.Data.Where(q => q.Centroid == centroid).ToList();

            foreach (var point in clusterPoints)
            {
                SSE += Math.Pow(point.Distance.Value, 2);
            }

            return SSE;
        }

        public void Print()
        {
            Console.WriteLine("K-means:\n");
            Console.WriteLine(" - Amount of iterations: " + this.Iterations);
            Console.WriteLine(" - Amount of clusters: " + this.Clusters);
            Console.WriteLine(" - Amount of observations: " + this.Data.Count);
            Console.WriteLine("-----------------------------------------------\n");

            for (int centroid = 0; centroid < this.Centroids.Count; centroid++)
            {
                var cluster = this.Data.Where(q => q.Centroid == centroid).ToList();

                Console.WriteLine("\nCluster " + (centroid + 1) + " contains " + cluster.Count + " customers.");
                Console.WriteLine("SSE: " + this.CalculateSSE(centroid) + "\n");

                foreach (var observation in cluster.Distinct())
                {
                    for (int key = 0; key < observation.Points.Count; key++)
                    {
                        var value = observation.Points.ElementAtOrDefault(key);
                        if (value > 0)
                        {
                            Console.Write("Offer " + (key + 1) + " was taken " + value + " times.\n");
                        }
                    }
                }
            }
        }
    }
}
