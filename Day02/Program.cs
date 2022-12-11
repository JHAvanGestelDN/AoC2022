using Day00;
namespace Day02
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
            return list.Select(s => s.Split(" ")).Select(split => new RockPaperScissors(split[0], split[1]).points).Sum();
        }

        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            return list.Select(s => s.Split(" ")).Select(split => new RockPaperScissors(split[0], split[1], true).points).Sum();
        }
    }
}
