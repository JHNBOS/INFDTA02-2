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
        private int K { get; set; }
        private double SSE { get; set; }
        private Cluster Best { get; set; }
        private List<Result> Results { get; set; }

        public Kmeans(List<Vector> data, int iterations, int k, int cluster)
        {
            this.Data = data;
            this.Clusters = cluster;
            this.Centroids = new List<Vector>();
            this.Iterations = iterations;
            this.K = k;
        }

        //public List<Result> Run(int times)
        //{
        //    var results = new List<Result>();

        //    var temp = 0d;
        //    this.GenerateClusters();

        //    for (int i = 0; i < times; i++)
        //    {
        //        this.AssignObservations();
        //        foreach (var cluster in this.Clusters)
        //        {
        //            SSE = this.CalculateSSE(cluster);
        //            if (temp < SSE)
        //            {
        //                temp = SSE;
        //                Best = cluster;
        //            }

        //            if (results.FirstOrDefault(q => q.Cluster == cluster) == null)
        //            {
        //                results.Add(new Result
        //                {
        //                    Cluster = cluster,
        //                    SSE = SSE
        //                });
        //            }

        //        }
        //    }

        //    Console.WriteLine("Best cluster is " + Best.Id);
        //    return results.OrderBy(o => o.SSE).Take(4).ToList();
        //}

        private void GenerateCentroids()
        {
            var random = new Random();
            for (int i = 0; i < this.K; i++)
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

        private double CalculateSSE(Cluster cluster)
        {
            double SSE = 0;
            foreach (var point in cluster.Points)
            {
                SSE += Math.Pow(point.Distance.Value, 2);
            }

            return SSE;
        }
    }
}
