using Assignment1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assignment1.Components.Algorithmes
{
    public class Kmeans
    {
        private List<Vector> Data { get; set; }
        private List<Vector> Centroids { get; set; }
        private int Iterations { get; set; }
        private int Clusters { get; set; }

        public Kmeans(List<Vector> data, int iterations, int clusters)
        {
            this.Data = data;
            this.Centroids = new List<Vector>();
            this.Iterations = iterations;
            this.Clusters = clusters;
        }

        private void GenerateCentroids()
        {
            for (int position = 0; position < this.Clusters; position++)
            {
                var randomIndex = new Random().Next(0, (this.Data.Count - 1));

                var centroid = this.Data.ElementAtOrDefault(randomIndex);
                this.Centroids.Insert(position, centroid);
            }
        }

        public void AssignObservations()
        {
            this.GenerateCentroids();

            foreach (var vector in this.Data)
            {
                var smallestDistance = double.PositiveInfinity;
                for (int cluster = 0; cluster < this.Clusters; cluster++)
                {
                    var distance = new Euclidian().Calculate(vector, this.Centroids.ElementAt(cluster));
                    if (distance < smallestDistance)
                    {
                        smallestDistance = distance;
                        vector.Centroid = cluster;
                    }
                }
            }
        }

        private void UpdateCentroid()
        {

        }

    }
}
