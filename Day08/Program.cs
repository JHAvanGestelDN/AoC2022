using Day00;
using Day00.Maps;

namespace Day08
{
    internal class Program : Base
    {
        public static void Main(string[] args)
        {
            Program p = new();
        }

        protected override long SolveOne()
        {
            var list = ReadFileToArray(PathOne);
            IntMap map = IntMap.CreateMap(list);

            return map.CountVisible();

        }

        protected override long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            IntMap map = IntMap.CreateMap(list);

            return map.DetermineNodeWithHighestScore();
        }
    }
}
