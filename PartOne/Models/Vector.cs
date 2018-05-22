using System.Collections.Generic;

namespace PartOne.Models
{
    public class Vector
    {
        private List<int> Points { get; set; }
        private Vector Centroid { get; set; }

        #region Constructors

        public Vector()
        {
            this.Points = new List<int>();
        }

        public Vector(List<int> points)
        {
            this.Points = points;
        }

        #endregion

        #region Getters and Setters

        public int Size()
        {
            return this.Points.Count;
        }

        public List<int> GetPoints()
        {
            return this.Points;
        }

        public void AddPoint(int point)
        {
            this.Points.Add(point);
        }

        public Vector GetCentroid()
        {
            return this.Centroid;
        }

        public void SetCentroid(Vector centroid)
        {
            this.Centroid = centroid;
        }

        #endregion
    }
}
