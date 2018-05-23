using System.Collections.Generic;

namespace PartOne.Models
{
    public class Vector
    {
        private List<double> Points { get; set; }

        #region Constructors

        public Vector()
        {
            this.Points = new List<double>();
        }

        public Vector(int dimension)
        {
            this.Points = new List<double>(dimension);
        }

        #endregion

        #region Getters and Setters

        public int Size()
        {
            return this.Points.Count;
        }

        public List<double> GetPoints()
        {
            return this.Points;
        }

        public void AddPoint(double point)
        {
            this.Points.Add(point);
        }

        #endregion
    }
}
