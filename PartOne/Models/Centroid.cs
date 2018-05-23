namespace PartOne.Models
{
    public class Centroid
    {
        public int Number { get; set; }
        public Vector Data { get; set; }

        #region Consructors

        public Centroid()
        {

        }

        public Centroid(Vector vector, int number)
        {
            this.Data = vector;
            this.Number = number;
        }

        #endregion
    }
}
