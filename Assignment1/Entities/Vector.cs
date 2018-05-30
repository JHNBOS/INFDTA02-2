﻿using System.Collections.Generic;

namespace Assignment1.Entities
{
    public class Vector
    {
        public List<float> Points { get; set; }
        public int Centroid { get; set; }
        public double? Distance { get; set; }

        #region Constructors

        public Vector()
        {
            this.Points = new List<float>();
        }

        public Vector(int size)
        {
            for (int i = 0; i < size; i++)
            {
                this.Points.Add(0);
            }
        }

        #endregion
    }
}
