using Assignment1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1.Components.Algorithmes
{
    public class Kmeans
    {
        private List<Vector> Data { get; set; }
        private List<Cluster> Centroids { get; set; }
        private int Iterations { get; set; }
        private int Clusters { get; set; }

        public Kmeans(List<Vector> data, int iterations, int clusters)
        {
            this.Data = data;
            this.Centroids = new List<Cluster>();
            this.Iterations = iterations;
            this.Clusters = clusters;
        }

        private void GenerateClusters()
        {
            var random = new Random();
            for (int position = 0; position < this.Clusters; position++)
            {
                var randomIndex = random.Next(0, (this.Data.Count - 1));
                var randomVector = this.Data.ElementAtOrDefault(randomIndex);

                var centroid = new Cluster();
                centroid.Number = position;
                centroid.Data = randomVector.Points;

                this.Centroids.Insert(position, centroid);
            }
        }

        public void AssignObservations()
        {
            this.GenerateClusters();

            int currentIteration = 0;
            while (currentIteration < this.Iterations)
            {
                foreach (var vector in this.Data)
                {
                    var currentCluster = 0;
                    var smallestDistance = double.PositiveInfinity;

                    for (int cluster = 0; cluster < this.Clusters; cluster++)
                    {
                        var distance = new Euclidian().Calculate(vector, this.Centroids.ElementAt(cluster));
                        vector.Distance = distance;

                        if (distance < smallestDistance)
                        {
                            smallestDistance = distance;
                            currentCluster = cluster;
                        }
                    }

                    vector.Cluster = currentCluster;
                    vector.Distance = smallestDistance;
                }

                currentIteration++;
            }
        }

        private void UpdateCentroid()
        {

        }

    }
}
