using Assignment1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1.Components.Algorithmes
{
    public class Kmeans
    {
        private List<Vector> Data { get; set; }
        private List<Cluster> Clusters { get; set; }
        private int Iterations { get; set; }
        private int K { get; set; }

        public Kmeans(List<Vector> data, int iterations, int k)
        {
            this.Data = data;
            this.Clusters = new List<Cluster>();
            this.Iterations = iterations;
            this.K = k;
        }

        public void Run(int times)
        {
            this.GenerateClusters();

            for (int i = 0; i < times; i++)
            {
                this.AssignObservations();
            }
        }

        private void GenerateClusters()
        {
            var random = new Random();
            for (int i = 0; i < this.K; i++)
            {
                var randomIndex = random.Next(0, this.Data.Count);
                var centroid = this.Data.ElementAtOrDefault(randomIndex);

                var cluster = new Cluster(i, centroid);
                if (!this.Clusters.Contains(cluster))
                {
                    this.Clusters.Add(cluster);
                }
            }
        }

        private void AssignObservations()
        {
            int currentIteration = 0;
            while (currentIteration < this.Iterations)
            {
                foreach (var vector in this.Data)
                {
                    var smallestDistance = double.PositiveInfinity;
                    foreach (var cluster in this.Clusters)
                    {
                        var distance = new Euclidian().Calculate(vector, cluster.Centroid);
                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                        }

                        if (distance < vector.Distance || !vector.Distance.HasValue)
                        {
                            vector.Distance = distance;
                            if (!cluster.Points.Contains(vector))
                            {
                                this.Clusters.ForEach(f =>
                                {
                                    if (f.Points.Contains(vector))
                                    {
                                        f.Points.Remove(vector);
                                    }
                                });

                                cluster.Points.Add(vector);
                            }
                        }
                    }
                }

                currentIteration++;
            }

            this.UpdateCentroids();
        }

        private void UpdateCentroids()
        {
            var meansList = new Dictionary<Vector, float>();
            var average = 0f;

            foreach (var cluster in this.Clusters)
            {
                foreach (var vector in cluster.Points)
                {
                    var mean = vector.Points.Sum() / vector.Points.Count;
                    if (!meansList.ContainsKey(vector))
                    {
                        meansList.Add(vector, mean);
                    }
                    else if (meansList.ContainsKey(vector))
                    {
                        if (mean < meansList[vector])
                        {
                            meansList[vector] = mean;
                        }
                    }

                    average += mean;
                }

                var closest = meansList.OrderBy(item => Math.Abs(average - item.Value)).First();

                // Update centroid of current cluster
                cluster.Centroid = closest.Key;
            }
        }

    }
}
