using Day00;

namespace Day11
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
            var monkeys = ParseMonkeys(list);
            const int roundCount = 20;
            for (int i = 1; i <= roundCount; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.items.Count > 0)
                    {
                        var item = monkey.items.Dequeue();
                        item = monkey.operation(item, monkey.operationValue >= 0 ? monkey.operationValue : item); // Increase worry level. if operation value is negative, use item value (old*old).
                        monkey.numberOfTimesInspectedAnItem++; // Increase number of times inspected an item.
                        item /= 3; // Decrease worry level.
                        var monkeyToTransferItemTo = monkey.Test(item); //determine which monkey to send items to
                        monkeys[monkeyToTransferItemTo].items.Enqueue(item); //send item to monkey
                    }
                }
            }

            return monkeys.OrderByDescending(m => m.numberOfTimesInspectedAnItem).Take(2).Select(x => x.numberOfTimesInspectedAnItem).Aggregate((current, i) => current * i);
        }
        private static List<Monkey> ParseMonkeys(string[] list)
        {

            List<Monkey> monkeys = new();
            Monkey monkey = new Monkey();
            foreach (var s in list)
            {
                if (string.IsNullOrEmpty(s))
                {
                    monkeys.Add(monkey);
                    monkey = new Monkey();
                }
                if (s.Contains("Starting items: "))
                {
                    var split = s.Split("Starting items: ")[1].Split(", ");
                    //parse split to ints and add them to monkey
                    foreach (var s1 in split)
                    {
                        monkey.items.Enqueue(int.Parse(s1));
                    }
                }
                if (s.Contains("Operation: new = old "))
                {
                    var split = s.Split("Operation: new = old ")[1].Split(" ");
                    monkey.operation = split[0] == "+" ? Monkey.Add : Monkey.Multiply;
                    var parse = int.TryParse(split[1], out int n);
                    if (parse)
                        monkey.operationValue = n;
                }
                if (s.Contains("Test"))
                {
                    var split = s.Split("Test: divisible by ")[1];
                    monkey.testBase = int.Parse(split);
                }
                if (s.Contains("true"))
                {
                    var split = s.Split("If true: throw to monkey ")[1];
                    monkey.testTrueMonkey = int.Parse(split);
                }
                if (s.Contains("false"))
                {
                    var split = s.Split("If false: throw to monkey ")[1];
                    monkey.testFalseMonkey = int.Parse(split);
                }
            }
            monkeys.Add(monkey);
            return monkeys;
        }

        protected override long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            var monkeys = ParseMonkeys(list);
            const int roundCount = 10000;
            var lcd = monkeys.Select(m => m.testBase).Aggregate((a, b) => a * b);

            for (int i = 1; i <= roundCount; i++)
            {
                foreach (var monkey in monkeys)
                {
                    while (monkey.items.Count > 0)
                    {
                        var item = monkey.items.Dequeue();
                        item = monkey.operation(item, monkey.operationValue >= 0 ? monkey.operationValue : item); // Increase worry level. if operation value is negative, use item value (old*old).
                        monkey.numberOfTimesInspectedAnItem++; // Increase number of times inspected an item.
                        item = item % lcd;
                        var monkeyToTransferItemTo = monkey.Test(item); //determine which monkey to send items to
                        monkeys[monkeyToTransferItemTo].items.Enqueue(item); //send item to monkey
                    }
                }
            }

            return monkeys.OrderByDescending(m => m.numberOfTimesInspectedAnItem).Take(2).Select(x => x.numberOfTimesInspectedAnItem).Aggregate((current, i) => current * i);
        }
    }
    public class Monkey
    {
        public Queue<long> items { get; set; } = new Queue<long>();
        public Func<long, long, long> operation { get; set; }
        public int operationValue { get; set; } = -1;
        public int testBase { get; set; }
        public int testTrueMonkey { get; set; }
        public int testFalseMonkey { get; set; }
        public long numberOfTimesInspectedAnItem { get; set; } = 0;

        public int Test(long input)
        {
            return input % testBase == 0 ? testTrueMonkey : testFalseMonkey;
        }


        public static long Add(long a, long b)
        {
            return a + b;
        }
        public static long Multiply(long a, long b)
        {
            return a * b;
        }
    }
}
