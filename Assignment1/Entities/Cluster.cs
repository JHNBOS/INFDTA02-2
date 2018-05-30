using System.Collections.Generic;

namespace Assignment1.Entities
{
    public class Cluster
    {
        public int Id { get; set; }
        public List<Vector> Points { get; set; }

        #region Constructors

        public Cluster()
        {
            this.Points = new List<Vector>();
        }

        public Cluster(int id)
        {
            this.Id = id;
            this.Points = new List<Vector>();
        }

        #endregion
    }
}
