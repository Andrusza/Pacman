namespace Silverlight3dApp.Utility
{
    public class Position2D
    {
        public int X;
        public int Y;

        public Position2D()
        {
        }

        public Position2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        //public static Position2D operator +(Position2D a, Position2D b)
        //{
        //    return new Position2D(a.X + b.X, a.Y + b.Y);
        //}

        //public static Position2D operator *(Position2D a, Position2D b)
        //{
        //    return new Position2D(a.X * b.X, a.Y * b.Y);
        //}

        //public static Position2D operator /(Position2D a, int b)
        //{
        //    return new Position2D(a.X / b, a.Y / b);
        //}
    }
}