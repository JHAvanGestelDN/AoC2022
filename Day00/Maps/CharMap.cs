using System;
using Day00.Nodes;

namespace Day00.Maps
{
    public class CharMap : AbstractMap<CharGenericNode, char>
    {
        public CharMap(int x, int y) : base(x, y)
        {
        }

        public int CountValues()
        {
            int sum = 0;
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] != null && Map[i, j].Value == '#')
                        sum++;
                }
            }

            return sum;
        }

 
    }
}