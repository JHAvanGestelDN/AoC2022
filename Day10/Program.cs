using Day00;
namespace Day10
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
            var newList = new List<string>
            {
            "noop" // add a dummy value to make the index match the value
            };

            foreach (var s in list)
            {
                if (s != "noop")
                    newList.Add("noop");
                newList.Add(s);
            }
            var cpuValue = 1;
            var result = 0;

            for (var i = 0; i < newList.Count; i++)
            {
                if (i is 20 or 60 or 100 or 140 or 180 or 220)
                {

                    Console.WriteLine($"{i} * {cpuValue} = {i * cpuValue}");
                    result += i * cpuValue;
                }
                var s = newList[i];
                if (s == "noop")
                    continue;
                var split = s.Split(" ");
                cpuValue += int.Parse(split[1]);
            }

            return result;
        }

        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            var newList = new List<string>
            {
            "noop" // add a dummy value to make the index match the value
            };

            foreach (var s in list)
            {
                if (s != "noop")
                    newList.Add("noop");
                newList.Add(s);
            }
            var cpuValue = 1;
            var currentDrawingPixel = 0;
            for (var i = 0; i < newList.Count; i++) //cycle
            {

                if (i > 0)
                {
                    if (cpuValue == currentDrawingPixel || cpuValue - 1 == currentDrawingPixel || cpuValue + 1 == currentDrawingPixel)
                        Console.Write("#");
                    else
                        Console.Write(" ");
                    currentDrawingPixel++;

                }
                if (i is 40 or 80 or 120 or 160 or 200 or 240)
                {
                    Console.WriteLine();
                    currentDrawingPixel = 0;
                }

                var s = newList[i];
                if (s == "noop")
                    continue;
                var split = s.Split(" ");
                cpuValue += int.Parse(split[1]);
            }


            return 0;
        }
    }
}
