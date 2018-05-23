using Assignment1.Entities;
using System;

namespace Assignment1.Components.Algorithmes
{
    public class Euclidian
    {
        public double Calculate(Vector vectorA, Vector vectorB)
        {
            double similarity = 0;
            for (int i = 0; i < vectorA.Points.Count; i++)
            {
                similarity += Math.Pow(vectorA.Points[i] - vectorB.Points[i], 2);
            }

            return Math.Sqrt(similarity);
        }
    }
}
