namespace Day00.Nodes
{
    public class GenericNode<T, V>
    {
        protected GenericNode(Coordinate coordinate, V value)
        {
            Coordinate = coordinate;
            Value = value;
        }

        public Coordinate Coordinate { get; }
        public List<T> Neighbours { get; } = new List<T>();
        public V Value { get; }

        public void AddNeighbours(T[,] map)
        {
            if (Coordinate.X > 0)
                Neighbours.Add(map[Coordinate.X - 1, Coordinate.Y]);
            if (Coordinate.X + 1 < map.GetLength(0))
                Neighbours.Add(map[Coordinate.X + 1, Coordinate.Y]);
            if (Coordinate.Y > 0)
                Neighbours.Add(map[Coordinate.X, Coordinate.Y - 1]);
            if (Coordinate.Y + 1 < map.GetLength(1))
                Neighbours.Add(map[Coordinate.X, Coordinate.Y + 1]);
        }

        public void AddNeighboursDiagonal(T[,] map)
        {
            AddNeighbours(map);

            //top left 
            if (Coordinate.X > 0 && Coordinate.Y > 0)
                Neighbours.Add(map[Coordinate.X - 1, Coordinate.Y - 1]);
            //top right 
            if (Coordinate.X > 0 && Coordinate.Y + 1 < map.GetLength(1))
                Neighbours.Add(map[Coordinate.X - 1, Coordinate.Y + 1]);

            //bottom left 
            if (Coordinate.X + 1 < map.GetLength(0) && Coordinate.Y > 0)
                Neighbours.Add(map[Coordinate.X + 1, Coordinate.Y - 1]);
            //bottom right 
            if (Coordinate.X + 1 < map.GetLength(0) && Coordinate.Y + 1 < map.GetLength(1))
                Neighbours.Add(map[Coordinate.X + 1, Coordinate.Y + 1]);
        }

        protected bool Equals(GenericNode<T, V> other)
        {
            return Equals(Coordinate, other.Coordinate);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((GenericNode<T, V>)obj);
        }

        public override int GetHashCode()
        {
            return Coordinate != null ? Coordinate.GetHashCode() : 0;
        }

        public static bool operator ==(GenericNode<T, V> left, GenericNode<T, V> right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(GenericNode<T, V> left, GenericNode<T, V> right)
        {
            return !Equals(left, right);
        }
    }
}
