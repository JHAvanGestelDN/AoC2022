using System.Text.Json;
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
            List<int> indexes = new List<int>();
            int pairIndex = 1;
            foreach (string s in list)
            {
                if (string.IsNullOrEmpty(s))
                {
                    var pairsInOrder = InOrder(JsonDocument.Parse(first!).RootElement, JsonDocument.Parse(second!).RootElement);
                    if (pairsInOrder < 0)
                        indexes.Add(pairIndex);
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

            return indexes.Sum();
        }
        private int InOrder(JsonElement first, JsonElement second)
        {
            if (first.ValueKind == JsonValueKind.Number && second.ValueKind == JsonValueKind.Number)
                return first.GetInt32() - second.GetInt32();

            //determine if both values are integers
            if (first.ValueKind != JsonValueKind.Number && second.ValueKind != JsonValueKind.Number)
            {
                for (int i = 0; i < first.GetArrayLength(); i++)
                {
                    if (i > second.GetArrayLength() - 1)
                        break;
                    var compare = InOrder(first[i], second[i]);
                    if (compare != 0)
                        return compare;
                }
                return first.GetArrayLength() - second.GetArrayLength();
            }

            // determine if one value is an integer and the other is a list
            if (first.ValueKind == JsonValueKind.Number && second.ValueKind != JsonValueKind.Number)
                return InOrder(JsonDocument.Parse($"[{first.GetInt32()}]").RootElement, second);

            if (first.ValueKind != JsonValueKind.Number && second.ValueKind == JsonValueKind.Number)
                return InOrder(first, JsonDocument.Parse($"[{second.GetInt32()}]").RootElement);

            return -1;
        }


        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne).ToList();

            List<JsonElement> elements = (from s in list where !string.IsNullOrEmpty(s) select JsonDocument.Parse(s).RootElement).ToList();
            
            var two = JsonDocument.Parse("[[2]]").RootElement;
            var six = JsonDocument.Parse("[[6]]").RootElement;
            elements.Add(two);
            elements.Add(six);


            elements.Sort(InOrder);
            int result = 1;
            for (var i = 0; i < elements.Count; i++)
            {
                if (InOrder(elements[i], two) == 0 || InOrder(elements[i], six) == 0)
                    result *= i + 1;
            }


            return result;
        }
    }
}