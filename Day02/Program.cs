using Day00;

namespace Day02
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
            int score = 0;
            foreach (var s in list)
            {
                var split = s.Split(" ");
                score += new RockPaperScissors(split[0], split[1]).points;
            }
            return score;
        }

        protected override long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            int score = 0;
            foreach (var s in list)
            {
                var split = s.Split(" ");
                score += new RockPaperScissors(split[0], split[1],true).points;
            }
            return score;
        }
    }
}
