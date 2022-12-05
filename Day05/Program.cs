﻿using Day00;

namespace Day05
{
    internal class Program : Base
    {
        public static void Main(string[] args)
        {
            Program p = new();
        }

        protected override long SolveOne()
        {
            var list = ReadFileToArray(PathOne).ToList();
            var stackHeadings = list.FirstOrDefault(s => s.Count() > 1 && s[1] == '1');
            var stackHeadingsIdx = list.IndexOf(stackHeadings);
            var numberOfStacks = int.Parse(stackHeadings.Trim().Split(' ').Last());

            //create a list with stack and fill the list with the amount of stacks we have just determined.
            List<Stack<string>> stacks = new();
            for (int i = 0; i < numberOfStacks; i++)
                stacks.Add(new Stack<string>());

            ParseStacks(stackHeadingsIdx, list, stacks);

            var moves = ParseMoves(stackHeadingsIdx, list);

            //execute moves
            foreach (var move in moves)
            {
                for (int i = 0; i < move.Count; i++)
                {
                    int indexOffset = 1;
                    var item = stacks[move.From - indexOffset].Pop(); //pop the item onto a tmp variable
                    stacks[move.To - indexOffset].Push(item); // push te tmp to the destination stack.
                }
            }

            //print the top of each stack
            PrintTopOfStacks(stacks);
            return 0;
        }
        private static void PrintTopOfStacks(List<Stack<string>> stacks)
        {
            foreach (var stack in stacks)
            {
                Console.Write(stack.Peek());
            }
            Console.WriteLine();
        }
        private static List<Move> ParseMoves(int stackHeadingsIdx, IReadOnlyList<string> list)
        {
            List<Move> moves = new List<Move>();
            for (int i = stackHeadingsIdx + 2; i < list.Count; i++) //moves start 2 lines below the stackheading
            {
                string s = list[i];
                var split = s.Split(" from ");
                var subsplit = split[1].Split(" to ");

                moves.Add(new Move
                {
                Count = int.Parse(split[0].Split("move ")[1]), // remove 'move ',
                From = int.Parse(subsplit[0]),
                To = int.Parse(subsplit[1])
                }
                );
            }
            return moves;
        }
        private static void ParseStacks(int stackHeadingsIdx, IReadOnlyList<string> list, IReadOnlyList<Stack<string>> stacks)
        {
            //Iterate the list starting from the stackheading -1 because thats where the bottom crates are.
            for (int j = stackHeadingsIdx - 1; j >= 0; j--)
            {
                string s = list[j];
                for (int i = 1; i < s.Length; i += 4) //skip first character and increment bij 4. Basically we skip the: '] [' part. 
                {
                    if (s[i] == ' ') //at that location there is no crate
                        continue;
                    //determine which stack to put the item in
                    var idx = i / 4; //each stack has 4 characters '[x] ' 
                    stacks[idx].Push(s[i].ToString());
                }
            }
        }

        protected override long SolveTwo()
        {
            var list = ReadFileToArray(PathOne).ToList();
            var stackHeadings = list.FirstOrDefault(s => s.Count() > 1 && s[1] == '1');
            var stackHeadingsIdx = list.IndexOf(stackHeadings);
            var numberOfStacks = int.Parse(stackHeadings.Trim().Split(' ').Last());

            //create a list with stack and fill the list with the amount of stacks we have just determined.
            List<Stack<string>> stacks = new();
            for (int i = 0; i < numberOfStacks; i++)
                stacks.Add(new Stack<string>());

            ParseStacks(stackHeadingsIdx, list, stacks);

            var moves = ParseMoves(stackHeadingsIdx, list);

            //execute moves
            foreach (var move in moves)
            {
                Stack<string> tmp = new Stack<string>();
                for (int i = 0; i < move.Count; i++)
                {
                    int indexOffset = 1;
                    var item = stacks[move.From - indexOffset].Pop();
                    tmp.Push(item);
                }
                while (tmp.Count > 0)
                {
                    stacks[move.To - 1].Push(tmp.Pop());
                }
            }

            PrintTopOfStacks(stacks);
            return 0;
        }
    }

    public class Move
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Count { get; set; }
    }
}
