using Day00;
namespace Day09
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

            var head = new Coordinate(0, 0);
            var tail = new Coordinate(0, 0);
            var tailCoordinates = new HashSet<Coordinate>
            {
            new Coordinate(0, 0)
            };

            foreach (var s in list)
            {
                var m = new Move(s);
                for (var i = 0; i < m.distance; i++)
                {
                    head = m.ExecuteMove(head);
                    tail = Move.DeterminePosition(tail, head);
                    tailCoordinates.Add(tail);
                }
            }

            return tailCoordinates.Count;
        }

        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            var head = new Coordinate(0, 0);
            var one = new Coordinate(0, 0);
            var two = new Coordinate(0, 0);
            var three = new Coordinate(0, 0);
            var four = new Coordinate(0, 0);
            var five = new Coordinate(0, 0);
            var six = new Coordinate(0, 0);
            var seven = new Coordinate(0, 0);
            var eight = new Coordinate(0, 0);
            var nine = new Coordinate(0, 0);
            var tailCoordinates = new HashSet<Coordinate>
            {
            new Coordinate(0, 0)
            };


            foreach (var s in list)
            {
                var m = new Move(s);
                for (var i = 0; i < m.distance; i++)
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
        public Move(string input)
        {
            var split = input.Split(' ');
            direction = input[0] == 'R' ? Direction.Right : input[0] == 'L' ? Direction.Left : input[0] == 'U' ? Direction.Up : Direction.Down;
            distance = int.Parse(split[1]);
        }
        private Direction direction { get; }
        public int distance { get; }
        public Coordinate ExecuteMove(Coordinate coordinate)
        {
            //modify coordinate based on direction
            var y = coordinate.Y;
            var x = coordinate.X;
            switch (direction)
            {
                case Direction.Up:
                    y++;
                    break;
                case Direction.Down:
                    y--;
                    break;
                case Direction.Left:
                    x--;
                    break;
                case Direction.Right:
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
            var newX = tail.X;
            var newY = tail.Y;
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

            var touching = tail.X == head.X && tail.Y == head.Y;


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
