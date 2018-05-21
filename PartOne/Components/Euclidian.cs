using PartOne.Models;
using System;

namespace PartOne.Components
{
    public class Euclidian
    {
        public double Calculate(Vector vectorA, Vector vectorB)
        {
            double similarity = 0;
            for (int i = 0; i < vectorA.Size(); i++)
            {
                similarity += Math.Pow(vectorA.GetPoints()[i] - vectorB.GetPoints()[i], 2);
            }

            return Math.Sqrt(similarity);
        }
    }
}
