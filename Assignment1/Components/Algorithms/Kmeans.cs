using Assignment1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1.Components.Algorithms
{
    public class Kmeans
    {
        private List<Vector> Data { get; set; }
        private List<Vector> Centroids { get; set; }
        private int Clusters { get; set; }
        private int Iterations { get; set; }
        private double SSE { get; set; }
        private int BestCluster { get; set; }
        private List<Result> Results { get; set; }

        public Kmeans(List<Vector> data, int iterations, int cluster)
        {
            this.Data = data;
            this.Clusters = cluster;
            this.Centroids = new List<Vector>();
            this.Iterations = iterations;
        }

        public void Run()
        {
            var results = new List<Result>();

            var temp = 0d;
            this.GenerateCentroids();

            this.AssignObservations();
            foreach (var centroid in this.Centroids)
            {
                var centroidNumber = this.Centroids.IndexOf(centroid);
                SSE = this.CalculateSSE(centroidNumber);
                if (temp < SSE)
                {
                    temp = SSE;
                    BestCluster = centroidNumber;
                }

                if (results.FirstOrDefault(q => q.Centroid == centroidNumber) == null)
                {
                    results.Add(new Result
                    {
                        Centroid = centroidNumber,
                        SSE = SSE
                    });
                }
            }

            this.Print();

            Console.WriteLine("-----------------------------------------------\n");
            Console.WriteLine("Best cluster is cluster " + BestCluster);
        }

        private void GenerateCentroids()
        {
            var random = new Random();

            while (this.Centroids.Count < this.Clusters)
            {
                var randomIndex = random.Next(0, this.Data.Count);
                var centroid = this.Data.ElementAtOrDefault(randomIndex);

                if (!this.Centroids.Contains(centroid))
                {
                    this.Centroids.Add(centroid);
                }
            }
        }

        private void AssignObservations()
        {
            for (int i = 0; i < this.Iterations; i++)
            {
                var currentCentroids = this.Centroids;
                foreach (var vector in this.Data)
                {
                    var smallestDistance = double.PositiveInfinity;
                    foreach (var centroid in this.Centroids)
                    {
                        var distance = new Euclidian().Calculate(vector, centroid);
                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                        }

                        if (distance < vector.Distance || !vector.Distance.HasValue)
                        {
                            vector.Distance = distance;
                            vector.Centroid = this.Centroids.IndexOf(centroid);
                        }
                    }
                }

                this.UpdateCentroids();

                if (StoppedChanging(currentCentroids))
                {
                    Console.WriteLine("Finished in " + i + " iterations");
                    break;
                }
            }
        }

        private void UpdateCentroids()
        {
            for (int currentCentroid = 0; currentCentroid < this.Clusters; currentCentroid++)
            {
                var clusterPoints = this.Data.Where(q => q.Centroid == currentCentroid).ToList();
                var newCluster = new Vector(this.Data.First().Points.Count);

                this.Centroids[currentCentroid] = newCluster;
            }
        }

        private bool StoppedChanging(List<Vector> oldCentroids)
        {
            var stopped = false;
            if (this.Centroids.Except(oldCentroids).ToList() == oldCentroids)
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

        private void Print()
        {
            Console.WriteLine("K-means\n");
            Console.WriteLine(" - Amount of iterations: " + this.Iterations);
            Console.WriteLine(" - Amount of clusters: " + this.Clusters);
            Console.WriteLine("-----------------------------------------------\n");

            for (int centroid = 0; centroid < this.Centroids.Count; centroid++)
            {
                var cluster = this.Data.Where(q => q.Centroid == centroid).ToList();

                Console.WriteLine("Cluster " + centroid + " contains " + cluster.Count + " vectors.");
                Console.WriteLine("Sum of squared errors for cluster " + centroid + ": " + this.CalculateSSE(centroid) + "\n");
            }
        }
    }
}
