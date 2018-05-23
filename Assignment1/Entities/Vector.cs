using System.Collections.Generic;

namespace Assignment1.Entities
{
    public class Vector
    {
        public int Id { get; set; }
        public int Centroid { get; set; }
        public List<double> Points { get; set; }

        #region Constructors

        public Vector()
        {
            this.Points = new List<double>();
        }

        #endregion
    }
}
