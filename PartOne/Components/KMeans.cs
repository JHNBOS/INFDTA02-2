using PartOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PartOne.Components
{
    public class KMeans
    {
        public List<Vector> Vectors { get; set; }

        public Point GetRandomPoints()
        {
            Random random = new Random();
            int randomCustomer = random.Next(1, this.Vectors.Count);
            int randomOffer = random.Next(0, 32);

            return new Point(randomCustomer, randomOffer);
        }

        public List<Vector> AssignObservations(Point[] clusterCenters)
        {
            var clusters = new List<Vector>();
            var euclidian = new Euclidian();

            Vectors.ForEach(vector =>
            {
                vector.Points.ForEach(point =>
                {
                    var distances = new Dictionary<Point, double>();
                    foreach (var center in clusterCenters)
                    {
                        //Calculate Euclidian distance between each cluster center
                        var distance = euclidian.Calculate(point, center);
                        distances.Add(center, distance);
                    }

                    //Assign to cluster with smallest distance
                    var smallestDistance = distances.OrderBy(o => o.Value).FirstOrDefault();
                    point.Centroid = smallestDistance.Key;
                });
            });

            //If over half of the observations have the same centroid, then assign this centroid as cluster of the vector
            Vectors.ForEach(vector =>
            {
                var divisions = new Dictionary<Point, int>();
                foreach (var center in clusterCenters)
                {
                    var amount = vector.Points.Where(q => q.Centroid == center).ToList().Count;
                    divisions.Add(center, amount);
                }

                var highest = divisions.Values.Max();
                var centroid = divisions.FirstOrDefault(q => q.Value == highest).Key;
                if (highest >= 16)
                {
                    vector.Centroid = centroid;
                    clusters.Add(vector);
                }
            });

            return clusters;
        }
    }
}
