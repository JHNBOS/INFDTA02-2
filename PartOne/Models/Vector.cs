using System.Collections.Generic;

namespace PartOne.Models
{
    public class Vector
    {
<<<<<<< HEAD
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
=======
        public int Customer { get; set; }
        public Dictionary<int, int> Offers { get; set; }

        public Vector()
        {
            this.Offers = new Dictionary<int, int>();
        }
>>>>>>> parent of 62d9efd... Assign to cluster
    }
}
