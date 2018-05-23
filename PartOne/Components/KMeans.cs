using PartOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PartOne.Components
{
    public class KMeans
    {
        private List<Vector> Vectors { get; set; }
        private List<Centroid> Centroids { get; set; }
        private int Clusters { get; set; }
        private int Iterations { get; set; }

        public KMeans(List<Vector> vectors, int clusters, int iterations)
        {
            this.Vectors = vectors;
            this.Centroids = new List<Centroid>();
            this.Clusters = clusters;
            this.Iterations = iterations;
        }

        public List<Vector> GetVectors()
        {
            return this.Vectors;
        }

        private void GenerateCentroids()
        {
            for (int i = 0; i < this.Clusters; i++)
            {
                var randomVector = GetRandomVector();
                if (this.Centroids.FirstOrDefault(q => q.Data == randomVector) == null)
                {
                    Centroids.Add(new Centroid(randomVector, i));
                }
            }
        }

        private Vector GetRandomVector()
        {
            Random random = new Random();
            int randomNumber = random.Next(0, this.Vectors.Count - 1);

            return this.Vectors.ElementAtOrDefault(randomNumber);
        }

        public void AssignObservations()
        {
            // Generate centroids
            GenerateCentroids();

            for (int i = 0; i < this.Iterations; i++)
            {
                foreach (var vector in this.Vectors)
                {
                    var lowestDistance = double.PositiveInfinity;
                    foreach (var centroid in this.Centroids)
                    {
                        //Calculate Euclidian distance between each cluster center
                        var distance = new Euclidian().Calculate(vector, centroid.Data);
                        if (distance < lowestDistance)
                        {
                            lowestDistance = distance;
                            client.ClusterNumber = centroid.Number;
                        }
                    }
                }

                this.Vectors = UpdateCentroids();
            }
        }

        private List<Vector> UpdateCentroids()
        {
            Dictionary<Vector, int> means = new Dictionary<Vector, int>();
            var vectors = new List<Vector>();

            foreach (var centroid in this.Centroids)
            {
                var cluster = this.Vectors.Where(q => q.GetCentroid() == centroid).ToList();
                foreach (var vector in cluster)
                {
                    var mean = vector.GetPoints().Sum() / vector.Size();
                    means.Add(vector, mean);
                }

                var average = means.Values.Average();
                var newCentroid = means.OrderBy(item => Math.Abs(item.Value - average)).First().Key;

                foreach (var vector in cluster)
                {
                    vector.SetCentroid(newCentroid);
                    vectors.Add(vector);
                }
            }

            return vectors;
        }

        public Vector Mean(List<Vector> points, int length)
        {
            var newVector = new Vector(length);

            foreach (var vector in points)
            {
                for (int i = 0; i < newVector.GetPoints().Count; i++)
                {
                    newVector.AddPoint(vector.GetPoints()[i]);
                }
            }

            for (int i = 0; i < newVector.GetPoints().Count; i++)
            {
                newVector.GetPoints()[i] = (double)newVector.GetPoints()[i] / points.Count;
            }

            return newVector;
        }

    }
}
