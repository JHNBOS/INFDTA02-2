using System;

namespace Assignment2.Models
{
    public class Ind
    {
        public string Binary { get; set; }

        #region Constructors

        public Ind()
        {

        }

        public Ind(string binary)
        {
            this.Binary = binary;
        }

        public Ind(double value)
        {
            this.Binary = Convert.ToString(value);
        }

        #endregion

        #region Getters

        public double Value()
        {
            double sum = 0;

            var bits = this.Binary.ToCharArray();
            for (int i = 0; i < bits.Length; i++)
            {
                var bit = bits[i];
                if (bit == '1')
                {
                    sum += Math.Pow(2, i);
                }
            }

            return sum;
        }

        #endregion
    }
}
