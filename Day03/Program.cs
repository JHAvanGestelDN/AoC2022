using Day00;
namespace Day03
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
            long priority = 0;
            foreach (var s in list)
            {
                //split string into two equal parts
                var firstPart = s.Substring(0, s.Length / 2);
                var secondPart = s.Substring(s.Length / 2);
                priority += firstPart.Where(l => secondPart.Contains(l)).Distinct().Select(CalculatePriority).Sum();
            }

            return priority;
        }
        private static int CalculatePriority(char c)
        {
            return char.IsLower(c) ? c - 96 : c - 64 + 26; //ascii values
        }

        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            var grouping = new List<List<string>>();
            for (var i = 0; i < list.Length; i += 3)
            {
                grouping.Add(list.Skip(i).Take(3).ToList());
            }
            long priority = 0;
            foreach (var group in grouping)
            {
                priority += group.First().Where(c => group.All(s => s.Contains(c))).Distinct().Select(CalculatePriority).Sum();
            }
            return priority;

        }
    }
}
