using System.Text.RegularExpressions;
using Day00;

namespace Day06
{
    internal class Program : Base
    {
        public static void Main(string[] args)
        {
            Program p = new();
        }

        protected override long SolveOne()
        {
            var com = ReadFileToArray(PathOne).First();
            int result = 0;
            const string pattern = @"^(?:([A-Za-z])(?!.*\1))*$";
            const int headerLength = 4;
            for (int i = 0; i < com.Length; i++)
            {
                string header = com.Substring(i, headerLength);
                if (!Regex.IsMatch(header, pattern))
                    continue;
                result = i + headerLength;
                break;
            }

            return result;

        }

        protected override long SolveTwo()
        {
            var com = ReadFileToArray(PathOne).First();
            int result = 0;
            const string pattern = @"^(?:([A-Za-z])(?!.*\1))*$";
            const int messageLength = 14;
            for (int i = 0; i < com.Length; i++)
            {
                string header = com.Substring(i, messageLength);
                if (!Regex.IsMatch(header, pattern))
                    continue;
                result = i + messageLength;
                break;
            }

            return result;
        }
    }
}
