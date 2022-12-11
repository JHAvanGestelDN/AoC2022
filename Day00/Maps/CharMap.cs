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
            var sum = 0;
            for (var i = 0; i < Map.GetLength(0); i++)
            {
                for (var j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j] != null && Map[i, j].Value == '#')
                        sum++;
                }
            }

            return sum;
        }
    }
}
