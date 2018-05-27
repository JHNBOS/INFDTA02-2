using System.Collections.Generic;

namespace Assignment1.Entities
{
    public class Vector
    {
        public List<float> Points { get; set; }
        public double? Distance { get; set; }

        #region Constructors

        public Vector()
        {
            this.Points = new List<float>();
        }

        #endregion
    }
}
