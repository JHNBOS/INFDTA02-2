namespace PartOne.Models
{
    public class Point
    {
        private int X { get; set; }
        private int Y { get; set; }

        public Point(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public void SetX(int x)
        {
            this.X = x;
        }

        public void SetY(int y)
        {
            this.Y = y;
        }

        public int GetX()
        {
            return this.X;
        }

        public int GetY()
        {
            return this.Y;
        }
    }
}
