using System.Text.Json;
using System.Text.Json.Nodes;
using Day00;

namespace Day13
{
    internal class Program : Base
    {
        public static void Main(string[] args)
        {
            new Program();
        }

        override protected long SolveOne()
        {
            var list = ReadFileToArray(PathOneSample);
            string? first = null;
            string? second = null;
            int result = 0;
            int pairIndex = 0;
            foreach (string s in list)
            {
                if (string.IsNullOrEmpty(s))
                {

                    var pairsInOrder = InOrder(JsonArray.Parse(first), JsonArray.Parse(second));
                    result += pairsInOrder ? pairIndex : 0;
                    first = null;
                    second = null;
                    pairIndex++;
                    continue;
                }
                if (first == null)
                    first = s;
                else
                    second = s;
            }

            return result;
        }
        public bool InOrder(JsonNode first, JsonNode second)
        {
            bool result = false;

            //deterime if both values are lists
            if (first[0].GetType().AssemblyQualifiedName.Contains("Array") && second[0].GetType().AssemblyQualifiedName.Contains("Array"))
            {
                Console.WriteLine("lists");
            }

            //determine if both values are integers
            if (first[0].GetType().AssemblyQualifiedName.Contains("Value") && second[0].GetType().AssemblyQualifiedName.Contains("Value"))
            {
                int firstValue = first[0].GetValue<int>();
                int secondValue = second[0].GetValue<int>();
                if (firstValue > secondValue)
                    return false;
                if (firstValue < secondValue)
                    return true;
                if (firstValue == secondValue)
                {
                    //create a new json object with evertyhing but the first value
                    
                    return InOrder(newFirst, newSecond);
                }
            }

            // determine if one value is an integer and the other is a list
            if (first[0].GetType().AssemblyQualifiedName.Contains("Value") && second[0].GetType().AssemblyQualifiedName.Contains("Array"))
            {
                Console.WriteLine("first int second list list");
            }
            if (first[0].GetType().AssemblyQualifiedName.Contains("Array") && second[0].GetType().AssemblyQualifiedName.Contains("Value"))
            {
                Console.WriteLine("first int second list list");
            }


            return result;
        }


        override protected long SolveTwo()
        {
            throw new System.NotImplementedException();
        }
    }
}
