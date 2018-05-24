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
        private double temp { get; set; }
        private List<Cluster> Best { get; set; }

        public Kmeans(List<Vector> data, int iterations, int clusters)
        {
            this.Data = data;
            this.Centroids = new List<Cluster>();
            this.Best = new List<Cluster>();
            this.temp = 0.0;
            this.Iterations = iterations;
            this.Clusters = clusters;
        }

        public void Run(int times)
        {
            var sse = 0d;
            for (int i = 0; i < times; i++)
            {
                this.AssignObservations();
                sse = this.CalculcateSSE();

                if (sse < temp)
                {
                    temp = sse;
                    Best = this.Centroids;
                }
            }

            DisplayResults(sse);
        }

        private void DisplayResults(double sse)
        {
            var result = new List<Dictionary<int, float>>();
            foreach (var cluster in this.Centroids)
            {
                var dict = new Dictionary<int, float>();
                for (var i = 0; i < this.Iterations; i++)
                {
                    dict.Add(i, 0);

                    foreach (var vector in cluster.Data)
                    {
                        dict[i] += vector;
                    }
                }

                result.Add(dict);
            }
            Console.WriteLine(sse);
            for (var i = 0; i < result.Count; i++)
            {
                Console.WriteLine("Cluster " + i);
                Console.WriteLine("-------------");
                var sortedDict = from entry in result[i]
                                 orderby entry.Value descending
                                 select entry;
                foreach (var it in sortedDict)
                {
                    if (it.Value > 3)
                    {
                        Console.WriteLine("Offer " + it.Key + ": " + it.Value + "x");
                    }
                }
                Console.WriteLine(" ");
            }
        }

        private void GenerateClusters()
        {
            var random = new Random();
            for (int position = 0; position < this.Clusters; position++)
            {
                var randomIndex = random.Next(0, this.Data.Count);
                var randomVector = this.Data.ElementAtOrDefault(randomIndex);

                var centroid = new Cluster();
                centroid.Number = position;
                centroid.Data = randomVector.Points;

                this.Centroids.Insert(position, centroid);
            }
        }

        private void AssignObservations()
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

                this.UpdateCentroid();

                currentIteration++;
            }

            for (var i = 0; i < this.Data.Count; i++)
            {
                this.Centroids[this.Data[i].Cluster].Data.Add(this.Data[i].Points[i]);
            }
        }

        private double CalculcateSSE()
        {
            double SSE = 0;
            for (var i = 0; i < this.Centroids.Count; i++)
            {
                foreach (var cus in this.Centroids[i].Data)
                {
                    SSE += Math.Pow(cus, 2);
                }
            }

            return SSE;
        }

        private void UpdateCentroid()
        {
            var isMoving = true;
            var groupedData = new Dictionary<int, List<float[]>>();
            
            for (var cluster = 0; cluster < this.Centroids.Count; cluster++)
            {
                groupedData.Add(cluster, new List<float[]>());
            }

            for (var i = 0; i < this.Data.Count; i++)
            {
                groupedData[this.Data[i].Cluster].Add(this.Data[i].Points.ToArray());
            }

            foreach (var key in groupedData.Keys)
            {
                var oldCentroid = this.Centroids[key];
                var newPoints = new float[this.Clusters];

                for (var i = 0; i < groupedData[key].Count; i++)
                {
                    var data = groupedData[key][i];
                    for (var j = 0; j < data.Length; j++)
                    {
                        newPoints[i] += data[j];
                    }
                }

                float divideBy = groupedData[key].Count;
                for (var i = 0; i < newPoints.Length; i++)
                {
                    newPoints[i] = newPoints[i] / divideBy;
                }

                var newCluster = new Cluster();
                newCluster.Data = newPoints.ToList();
                newCluster.Number = key;
                newCluster.IsMoving = true;
                this.Centroids[key] = newCluster;

                if (IsEqual(newCluster.Data, oldCentroid.Data))
                {
                    newCluster.IsMoving = false;
                }
            }

            var check = 0;
            for (var i = 0; i < this.Centroids.Count; i++)
            {
                if (this.Centroids[i].IsMoving == false)
                {
                    check++;
                }
            }

            if (check >= 4)
            {
                isMoving = false;
            }
        }

        private bool IsEqual(List<float> a, List<float> b)
        {
            for (var i = 0; i < a.Count; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;
        }

    }
}
