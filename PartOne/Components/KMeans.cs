using PartOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PartOne.Components
{
    public class KMeans
    {
        private List<Vector> Vectors { get; set; }
        private List<Vector> Centroids { get; set; }
        private int Clusters { get; set; }
        private int Loops { get; set; }

        public KMeans(List<Vector> vectors, int clusters, int loops)
        {
            this.Vectors = vectors;
            this.Clusters = clusters;
            this.Loops = loops;
        }

        public List<Vector> GetVectors()
        {
            return this.Vectors;
        }

        private void GenerateCentroids(int clusters)
        {
            for (int i = 0; i < clusters; i++)
            {
                Centroids.Add(GetRandomVector());
            }
        }

        private Vector GetRandomVector()
        {
            Random random = new Random();
            int randomNumber = random.Next(1, this.Vectors.Count);

            return this.Vectors[randomNumber];
        }


        public void AssignObservations()
        {
            // Generate centroids
            GenerateCentroids(this.Clusters);

            for (int i = 0; i < this.Loops; i++)
            {
                foreach (var vector in this.Vectors)
                {
                    var distances = new Dictionary<Vector, double>();
                    foreach (var centroid in this.Centroids)
                    {
                        //Calculate Euclidian distance between each cluster center
                        var distance = new Euclidian().Calculate(vector, centroid);
                        distances.Add(centroid, distance);
                    }

                    //Assign to cluster with smallest distance
                    var smallestDistance = distances.OrderBy(o => o.Value).FirstOrDefault();
                    vector.SetCentroid(smallestDistance.Key);
                }
            }

        }
    }
}
