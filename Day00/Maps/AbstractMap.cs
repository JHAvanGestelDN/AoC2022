using System;
using Day00.Nodes;

namespace Day00
{
    public abstract class AbstractMap<T, V> where T : GenericNode<T, V>
    {
        protected AbstractMap(int x, int y)
        {
            Map = new T[x, y];
        }

        public T[,] Map { get; }

        public void Print()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Console.Write(Map[i, j] != null ? Map[i, j].Value : ' ');
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static void AddNeighbours(T[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j].AddNeighboursDiagonal(map);
                }
            }
        }

        public void FillMap(Coordinate coordinate, T t)
        {
            Map[coordinate.X, coordinate.Y] = t;
        }
    }
}