using System;

namespace Assignment2.Entities
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

        public Ind(int value)
        {
            this.Binary = Convert.ToString(value, 2);
        }

        #endregion

        #region Getters

        public int Value()
        {
            return Convert.ToInt32(this.Binary, 2);
        }

        #endregion
    }
}
