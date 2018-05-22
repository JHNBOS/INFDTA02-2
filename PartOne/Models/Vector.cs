using System.Collections.Generic;

namespace PartOne.Models
{
    public class Vector
    {
        private Dictionary<int, int> Points { get; set; }
        private Vector Centroid { get; set; }

        #region Constructors

        public Vector()
        {
            this.Points = new Dictionary<int, int>();
        }

        public Vector(Dictionary<int, int> points)
        {
            this.Points = points;
        }

        #endregion

        #region Getters and Setters

        public int Size()
        {
            return this.Points.Count;
        }

        public Dictionary<int, int> GetPoints()
        {
            return this.Points;
        }

        public void AddPoint(int key, int point)
        {
            this.Points.Add(key, point);
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
