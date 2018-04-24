using PartOne.Models;
using System;
using System.Collections.Generic;

namespace PartOne.Components
{
    public class KMeans
    {
        public List<Vector> Vectors { get; set; }

        public Point GetRandomPoints()
        {
            Random random = new Random();
            int randomCustomer = random.Next(1, this.Vectors.Count);
            int randomOffer = random.Next(1, this.Vectors[randomCustomer].Offers.Count);

            return new Point(randomCustomer, randomOffer);
        }
    }
}
