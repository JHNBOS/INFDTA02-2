using PartOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PartOne.Components
{
    public class KMeans
    {
        public Dictionary<int, List<Point>> Points { get; set; }

        public Point GetRandomPoints()
        {
            Random random = new Random();
            int randomCustomer = random.Next(1, this.Points.Count);
            int randomOffer = random.Next(1, this.Points.FirstOrDefault(q => q.Key == randomCustomer).Value.Count);

            return new Point(randomCustomer, randomOffer);
        }

        public List<Point> AssignObservations(Point[] clusterCenters)
        {
            var clusters = new List<Point>();
            var euclidian = new Euclidian();

            foreach (var customer in this.Points)
            {
                foreach (var point in customer.Value)
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

                    clusters.Add(point);
                }
            }
            return clusters;
        }
    }
}
