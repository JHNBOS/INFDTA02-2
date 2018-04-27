using PartOne.Models;
using System;

namespace PartOne.Components
{
    public class Euclidian
    {
        public double Calculate(Point pointA, Point pointB)
        {
            double similarity = 0;
            similarity += Math.Pow(pointA.X - pointB.X, 2) + Math.Pow(pointA.Y - pointB.Y, 2);

            return 1 / (1 + Math.Sqrt(similarity));
        }
    }
}
