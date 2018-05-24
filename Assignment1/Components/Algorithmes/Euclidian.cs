using Assignment1.Entities;
using System;

namespace Assignment1.Components.Algorithmes
{
    public class Euclidian
    {
        public double Calculate(Vector vector, Cluster centroid)
        {
            double similarity = 0;
            for (int i = 0; i < vector.Points.Count; i++)
            {
                similarity += Math.Pow(vector.Points[i] - centroid.Data[i], 2);
            }

            return Math.Sqrt(similarity);
        }
    }
}
