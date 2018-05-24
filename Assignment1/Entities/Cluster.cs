using System.Collections.Generic;

namespace Assignment1.Entities
{
    public class Cluster
    {
        public int Number { get; set; }
        public List<float> Data { get; set; }

        public Cluster()
        {
            this.Data = new List<float>();
        }
    }
}
