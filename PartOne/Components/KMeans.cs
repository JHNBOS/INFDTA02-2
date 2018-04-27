using PartOne.Models;
using System;
using System.Collections.Generic;

namespace PartOne.Components
{
    public class KMeans
    {
        public List<Vector> Vectors { get; set; }

        public Point GetRandomPoints()
        {
            Random random = new Random();
            int randomCustomer = random.Next(1, this.Vectors.Count);
<<<<<<< HEAD
            int randomOffer = random.Next(1, this.Vectors.FirstOrDefault(q => q.Id == randomCustomer).Points.Count);

            return new Point(randomCustomer, randomOffer);
        }

        public List<Vector> AssignObservations(Point[] clusterCenters)
        {
            var clusters = new List<Vector>();
            var euclidian = new Euclidian();

            foreach (var vector in this.Vectors)
            {
                foreach (var point in vector.Points)
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
                    point.Cluster = smallestDistance.Key;
                }
            }

            //If over half of the observations have the same centroid, then assign this centroid as cluster of the vector
            foreach (var vector in this.Vectors)
            {
                var one = vector.Points.Where(q => q.Cluster == clusterCenters[0]).ToList().Count;
                var two = vector.Points.Where(q => q.Cluster == clusterCenters[1]).ToList().Count;
                var three = vector.Points.Where(q => q.Cluster == clusterCenters[2]).ToList().Count;
                var arr = new int[3];
                arr[0] = one;
                arr[1] = two;
                arr[2] = three;

                var highest = arr.Max();
                var index = arr.ToList().IndexOf(highest);

                if (highest >= 16)
                {
                    vector.Cluster = clusterCenters[index];
                    clusters.Add(vector);
                }
            }

            return clusters;
        }
=======
            int randomOffer = random.Next(1, this.Vectors[randomCustomer].Offers.Count);

            return new Point(randomCustomer, randomOffer);
        }
>>>>>>> parent of 62d9efd... Assign to cluster
    }
}
