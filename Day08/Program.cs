using Day00;
using Day00.Maps;
namespace Day08
{
    internal class Program : Base
    {
        public static void Main(string[] args)
        {
            new Program();
        }

        override protected long SolveOne()
        {
            var list = ReadFileToArray(PathOne);
            var map = IntMap.CreateMap(list);

            return map.CountVisible();

        }

        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            var map = IntMap.CreateMap(list);

            return map.DetermineNodeWithHighestScore();
        }
    }
}
