using Day00;
using Day00.Maps;

namespace Day09
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

            Coordinate head = new Coordinate(0, 0);
            Coordinate tail = new Coordinate(0, 0);
            HashSet<Coordinate> tailCoordinates = new HashSet<Coordinate>();
            tailCoordinates.Add(new Coordinate(0, 0));

            foreach (var s in list)
            {
                Move m = new Move(s);
                for (int i = 0; i < m.distance; i++)
                {
                    head = m.ExecuteMove(head);
                    tail = Move.DeterminePosition(tail, head);
                    tailCoordinates.Add(tail);
                }
            }

            return tailCoordinates.Count;
        }

        protected override long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            Coordinate head = new Coordinate(0, 0);
            Coordinate one = new Coordinate(0, 0);
            Coordinate two = new Coordinate(0, 0);
            Coordinate three = new Coordinate(0, 0);
            Coordinate four = new Coordinate(0, 0);
            Coordinate five = new Coordinate(0, 0);
            Coordinate six = new Coordinate(0, 0);
            Coordinate seven = new Coordinate(0, 0);
            Coordinate eight = new Coordinate(0, 0);
            Coordinate nine = new Coordinate(0, 0);
            HashSet<Coordinate> tailCoordinates = new HashSet<Coordinate>
            {
            new Coordinate(0, 0)
            };


            foreach (var s in list)
            {
                Move m = new Move(s);
                for (int i = 0; i < m.distance; i++)
                {
                    head = m.ExecuteMove(head);
                    one = Move.DeterminePosition(one, head);
                    two = Move.DeterminePosition(two, one);
                    three = Move.DeterminePosition(three, two);
                    four = Move.DeterminePosition(four, three);
                    five = Move.DeterminePosition(five, four);
                    six = Move.DeterminePosition(six, five);
                    seven = Move.DeterminePosition(seven, six);
                    eight = Move.DeterminePosition(eight, seven);
                    nine = Move.DeterminePosition(nine, eight);
                    tailCoordinates.Add(nine);
                }
            }

            return tailCoordinates.Count;
        }
    }
    public class Move
    {
        public Direction direction { get; }
        public int distance { get; }

        public Move(string input)
        {
            var split = input.Split(' ');
            direction = input[0] == 'R' ? Direction.RIGHT : input[0] == 'L' ? Direction.LEFT : input[0] == 'U' ? Direction.UP : Direction.DOWN;
            distance = int.Parse(split[1]);
        }
        public Coordinate ExecuteMove(Coordinate coordinate)
        {
            //modify coordinate based on direction
            int y = coordinate.Y;
            int x = coordinate.X;
            switch (direction)
            {
                case Direction.UP:
                    y++;
                    break;
                case Direction.DOWN:
                    y--;
                    break;
                case Direction.LEFT:
                    x--;
                    break;
                case Direction.RIGHT:
                    x++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return new Coordinate(x, y);
        }
        public static Coordinate DeterminePosition(Coordinate tail, Coordinate head)
        {
            //determine if tail is within one coordinate of head
            var touching = HeadAndTailTouching(tail, head);
            if (touching)
                return tail;

            var xDif = tail.X - head.X;
            var yDif = tail.Y - head.Y;
            int newX = tail.X;
            int newY = tail.Y;
            //move diagonally
            if (xDif != 0 && yDif != 0)
            {
                newX += xDif < 0 ? 1 : -1;
                newY += yDif < 0 ? 1 : -1;

            }
            //move horizontally
            else if (xDif != 0)
            {
                newX += xDif < 0 ? 1 : -1;
            }
            //move vertically
            else if (yDif != 0)
            {
                newY += yDif < 0 ? 1 : -1;
            }

            return new Coordinate(newX, newY);

        }
        private static bool HeadAndTailTouching(Coordinate tail, Coordinate head)
        {

            bool touching = tail.X == head.X && tail.Y == head.Y;


            if (tail.X == head.X)
            {
                if (tail.Y == head.Y + 1 || tail.Y == head.Y - 1)
                {
                    touching = true;
                }
            }
            else if (tail.Y == head.Y)
            {
                if (tail.X == head.X + 1 || tail.X == head.X - 1)
                {
                    touching = true;
                }
            }
            else
            {
                //check if tail is diagonally of head
                if (tail.X == head.X + 1 && tail.Y == head.Y + 1)
                {
                    touching = true;
                }
                else if (tail.X == head.X - 1 && tail.Y == head.Y - 1)
                {
                    touching = true;
                }
                else if (tail.X == head.X + 1 && tail.Y == head.Y - 1)
                {
                    touching = true;
                }
                else if (tail.X == head.X - 1 && tail.Y == head.Y + 1)
                {
                    touching = true;
                }
            }


            return touching;
        }
    }
}
