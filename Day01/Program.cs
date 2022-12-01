using Day00;

namespace Day01
{
    internal class Program : Base
    {
        public static void Main(string[] args)
        {
            Program p = new Program();
        }

        protected override long SolveOne()
        {
            var list = ReadFileToArray(PathOne);
            long max = -1;
            long current = 0;
            foreach (var s in list)
            {
                if (string.IsNullOrEmpty(s))
                {
                    max = current > max ? current : max;
                    current = 0;
                    continue;
                }
                current += long.Parse(s);
            }
            return max;
        }

        protected override long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            var sortedList = new SortedSet<long>();
            long current = 0;
            foreach (var s in list)
            {
                if (string.IsNullOrEmpty(s))
                {
                    sortedList.Add(current);
                    current = 0;
                    continue;
                }
                current += long.Parse(s);
            }
            return sortedList.Skip(sortedList.Count - 3).Sum();
            //todo rework to LINQ
        }
    }
   
}
