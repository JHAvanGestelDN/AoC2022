using System.Text.RegularExpressions;
using Day00;
namespace Day06
{
    internal class Program : Base
    {
        public static void Main(string[] args)
        {
            new Program();
        }

        override protected long SolveOne()
        {
            var com = ReadFileToArray(PathOne).First();
            var result = 0;
            const string pattern = @"^(?:([A-Za-z])(?!.*\1))*$";
            const int headerLength = 4;
            for (var i = 0; i < com.Length; i++)
            {
                var header = com.Substring(i, headerLength);
                if (!Regex.IsMatch(header, pattern))
                    continue;
                result = i + headerLength;
                break;
            }

            return result;

        }

        override protected long SolveTwo()
        {
            var com = ReadFileToArray(PathOne).First();
            var result = 0;
            const string pattern = @"^(?:([A-Za-z])(?!.*\1))*$";
            const int messageLength = 14;
            for (var i = 0; i < com.Length; i++)
            {
                var header = com.Substring(i, messageLength);
                if (!Regex.IsMatch(header, pattern))
                    continue;
                result = i + messageLength;
                break;
            }

            return result;
        }
    }
}
