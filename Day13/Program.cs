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
            var list = ReadFileToArray(PathOne).ToList();
            list.Add("");
            string? first = null;
            string? second = null;
            int result = 0;
            int pairIndex = 1;
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
            if (first.AsArray().Count == 0 && second.AsArray().Count == 0)
                return true;
            if (first.AsArray().Count == 0 && second.AsArray().Count > 0)
                return true;
            if (first.AsArray().Count > 0 && second.AsArray().Count == 0)
                return false;

            //deterime if both values are lists
            if (first[0].GetType().AssemblyQualifiedName.Contains("Array") && second[0].GetType().AssemblyQualifiedName.Contains("Array"))
            {
                
                var x = JsonArray.Parse(first[0].ToJsonString());
                var y = JsonArray.Parse(second[0].ToJsonString());
                var compare = InOrder(x, y);
                if (!compare)
                    return false;
                first.AsArray().Remove(first[0]);
                second.AsArray().Remove(second[0]);
                return InOrder(first, second);
                

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

                    first.AsArray().Remove(first[0]);
                    second.AsArray().Remove(second[0]);
                    return InOrder(first, second);
                }
            }

            // determine if one value is an integer and the other is a list
            if (first[0].GetType().AssemblyQualifiedName.Contains("Value") && second[0].GetType().AssemblyQualifiedName.Contains("Array"))
            {
                var x = $"[{first[0]}]";
                first[0] = JsonArray.Parse(x);
                return InOrder(first, second);
            }
            if (first[0].GetType().AssemblyQualifiedName.Contains("Array") && second[0].GetType().AssemblyQualifiedName.Contains("Value"))
            {
                var x = $"[{second[0]}]";
                second[0] = JsonArray.Parse(x);
                return InOrder(first, second);
            }


            return result;
        }


        override protected long SolveTwo()
        {
            throw new System.NotImplementedException();
        }
    }
}
