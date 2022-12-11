using Day00;
namespace Day04
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
            var result = 0;
            foreach (var s in list)
            {
                var elfPartnerRanges = s.Split(',');
                var ranges = elfPartnerRanges.Select(elfPartnerRange => elfPartnerRange.Split('-')).Select(split => new Range(int.Parse(split[0]), int.Parse(split[1]))).ToList();
                var first = ranges.First();
                var second = ranges.Last();
                if (first.Start.Value <= second.Start.Value && first.End.Value >= second.End.Value ||
                    second.Start.Value <= first.Start.Value && second.End.Value >= first.End.Value)
                {
                    result++;
                }
            }

            return result;
        }

        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            var result = 0;
            foreach (var s in list)
            {
                var elfPartnerRanges = s.Split(',');
                var ranges = elfPartnerRanges.Select(elfPartnerRange => elfPartnerRange.Split('-')).Select(split => new Range(int.Parse(split[0]), int.Parse(split[1]))).ToList();
                var first = ranges.First();
                var second = ranges.Last();
                // check if the ranges overlap
                if (first.Start.Value <= second.End.Value && second.Start.Value <= first.End.Value)
                {
                    result++;
                }
            }
            return result;
        }
    }
}
