using System.Collections.Generic;

namespace PartOne.Models
{
    public class Vector
    {
        public int Id { get; set; }
        public List<Point> Points { get; set; }
        public Point Cluster { get; set; }

        #region Constructors

        public Vector()
        {
            this.Points = new List<Point>();
        }

        public Vector(int id)
        {
            this.Id = id;
            this.Points = new List<Point>();
        }

        #endregion
    }
}
