using System.Collections.Generic;
using Day00.Nodes;

namespace Day00.Maps
{
    public class IntMap : AbstractMap<IntNode, int>
    {
        public IntMap(int x, int y) : base(x, y)
        {
        }

        public static IntMap CreateMap(IReadOnlyList<string> input)
        {
            var map = new IntMap(input.Count, input[0].Length);

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    string charValue = input[i][j].ToString();
                    map.Map[i, j] = new IntNode(new Coordinate(i, j), int.Parse(charValue));
                }
            }

            return map;
        }

        public void AddNeighbours()
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    Map[i, j].AddNeighbours(Map);
                }
            }
        }
    }
}