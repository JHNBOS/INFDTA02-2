using System.Collections.Generic;

namespace PartOne.Models
{
    public class Vector
    {
        public int Customer { get; set; }
        public Dictionary<int, int> Offers { get; set; }

        public Vector()
        {
            this.Offers = new Dictionary<int, int>();
        }
    }
}
