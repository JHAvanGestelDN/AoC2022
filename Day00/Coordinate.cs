namespace Day00
{
    public sealed class Coordinate
    {
        //2D coordinate
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
            Z = 0;
        }

        //3D coordinate
        public Coordinate(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }
        public int Y { get; }
        public int Z { get; }

        protected bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Coordinate)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = X;
                hashCode = hashCode * 397 ^ Y;
                hashCode = hashCode * 397 ^ Z;
                return hashCode;
            }
        }
    }
}
