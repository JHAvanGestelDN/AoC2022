using Day00;
using Day00.Maps;
using Day00.Nodes;

namespace Day14
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
            HashSet<Coordinate> coordinates = new HashSet<Coordinate>();
            ParseRocks(list, coordinates);
            int minX;
            (minX, var maxX, var minY, var maxY) = DetermineMinMax(coordinates);

            var map = CreateMap(maxX, minX, maxY, minY, coordinates, out var sandGenerator);

            //game
            int sandAtRest = 0;
            bool done = false;
            while (!done)
            {
                var sandStartCoordinate = new Coordinate(sandGenerator.Coordinate.X, sandGenerator.Coordinate.Y + 1);
                var sand = new CharGenericNode(sandStartCoordinate, 'o');
                bool currentCannotMove = false;
                map.Map[sandStartCoordinate.X, sandStartCoordinate.Y] = sand;
                while (!currentCannotMove)
                {
                    //move current
                    int oldX = sand.Coordinate.X;
                    int oldY = sand.Coordinate.Y;
                    //down
                    if (sand.Coordinate.Y + 1 > maxY)
                    {
                        done = true;
                        break;
                    }
                    if (map.Map[sand.Coordinate.X, sand.Coordinate.Y + 1] == null)
                    {
                        map.Map[sand.Coordinate.X, sand.Coordinate.Y + 1] = sand;
                        sand.Coordinate.Y++;
                    }
                    //down left
                    else if (sand.Coordinate.X - 1 < 0)
                    {
                        done = true;
                        break;
                    }
                    else if (map.Map[sand.Coordinate.X - 1, sand.Coordinate.Y + 1] == null)
                    {
                        map.Map[sand.Coordinate.X - 1, sand.Coordinate.Y + 1] = sand;
                        sand.Coordinate.X--;
                        sand.Coordinate.Y++;
                    }
                    //down right
                    else if (sand.Coordinate.X + 1 >= map.Map.Length)
                    {
                        done = true;
                        break;
                    }
                    else if (map.Map[sand.Coordinate.X + 1, sand.Coordinate.Y + 1] == null)
                    {
                        map.Map[sand.Coordinate.X + 1, sand.Coordinate.Y + 1] = sand;
                        sand.Coordinate.X++;
                        sand.Coordinate.Y++;
                    }
                    else
                    {
                        currentCannotMove = true;
                    }
                    if (!currentCannotMove)
                        map.Map[oldX, oldY] = null!;
                }
                if (!done)
                    sandAtRest++;
            }
            return sandAtRest;
        }
        private static CharMap CreateMap(int maxX, int minX, int maxY, int minY, HashSet<Coordinate> coordinates, out CharGenericNode sandGenerator)
        {

            CharMap map = new CharMap(maxX - minX + 1, maxY - minY + 1);
            foreach (var coordinate in coordinates)
            {
                coordinate.X -= minX;
                coordinate.Y -= minY;
                map.FillMap(coordinate, new CharGenericNode(coordinate, '#'));
            }
            sandGenerator = new CharGenericNode(new Coordinate(500 - minX, 0 - minY), '+');
            map.FillMap(sandGenerator.Coordinate, sandGenerator);
            return map;
        }
        private static Tuple<int, int, int, int> DetermineMinMax(HashSet<Coordinate> coordinates)
        {

            int minX = 500, maxX = 500;
            int minY = 0, maxY = 0;
            foreach (var coordinate in coordinates)
            {
                if (coordinate.X < minX)
                    minX = coordinate.X;
                if (coordinate.X > maxX)
                    maxX = coordinate.X;
                if (coordinate.Y < minY)
                    minY = coordinate.Y;
                if (coordinate.Y > maxY)
                    maxY = coordinate.Y;
            }
            return new Tuple<int, int, int, int>(minX, maxX, minY, maxY);
        }
        private void ParseRocks(IEnumerable<string> list, ISet<Coordinate> coordinates)
        {

            foreach (var line in list)
            {
                var split = line.Split(" -> ");
                var first = split[0];
                var firstCoordinate = ParseCoordinate(first);
                coordinates.Add(firstCoordinate);
                //iterate over split starting from index 1
                for (int i = 1; i < split.Length; i++)
                {
                    //determine difference
                    var to = ParseCoordinate(split[i]);
                    var range = Coordinate.DetermineRange(firstCoordinate, to);
                    foreach (var coordinate in range)
                    {
                        coordinates.Add(coordinate);
                    }
                    firstCoordinate = to;

                }

            }
        }
        private static Coordinate ParseCoordinate(string input)
        {
            var split = input.Split(",");
            var coordinate = new Coordinate(int.Parse(split[0]), int.Parse(split[1]));
            return coordinate;
        }

        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            HashSet<Coordinate> coordinates = new HashSet<Coordinate>();
            ParseRocks(list, coordinates);
            var (item1, item2, minY, item4) = DetermineMinMax(coordinates);
            var minX = item1 - 200; //randomly chosen to make sure we have enough space
            var maxX = item2 + 200; //randomly chosen to make sure we have enough space
            var maxY = item4 + 2; //added two because of the way the game works

            var map = CreateMap(maxX, minX, maxY, minY, coordinates, out var sandGenerator);

            //fill bottom row with sand
            for (int i = 0; i < map.Map.GetLength(0); i++)
            {
                map.Map[i, maxY] = new CharGenericNode(new Coordinate(i, maxY), '#');
            }

            //game
            int sandAtRest = 0;
            bool done = false;
            while (!done)
            {
                var sandStartCoordinate = new Coordinate(sandGenerator.Coordinate.X, sandGenerator.Coordinate.Y);
                var sand = new CharGenericNode(sandStartCoordinate, 'o');
                bool currentCannotMove = false;
                map.Map[sandStartCoordinate.X, sandStartCoordinate.Y] = sand;
                while (!currentCannotMove)
                {
                    //move current
                    int oldX = sand.Coordinate.X;
                    int oldY = sand.Coordinate.Y;
                    //down

                    if (map.Map[sand.Coordinate.X, sand.Coordinate.Y + 1] == null)
                    {
                        map.Map[sand.Coordinate.X, sand.Coordinate.Y + 1] = sand;
                        sand.Coordinate.Y++;
                    }
                    //down left

                    else if (map.Map[sand.Coordinate.X - 1, sand.Coordinate.Y + 1] == null)
                    {
                        map.Map[sand.Coordinate.X - 1, sand.Coordinate.Y + 1] = sand;
                        sand.Coordinate.X--;
                        sand.Coordinate.Y++;
                    }
                    //down right
                    else if (map.Map[sand.Coordinate.X + 1, sand.Coordinate.Y + 1] == null)
                    {
                        map.Map[sand.Coordinate.X + 1, sand.Coordinate.Y + 1] = sand;
                        sand.Coordinate.X++;
                        sand.Coordinate.Y++;
                    }
                    else
                    {
                        currentCannotMove = true;
                        //check start 
                        if (map.Map[sandGenerator.Coordinate.X, sandGenerator.Coordinate.Y] != null &&
                            map.Map[sandGenerator.Coordinate.X - 1, sandGenerator.Coordinate.Y + 1] != null &&
                            map.Map[sandGenerator.Coordinate.X + 1, sandGenerator.Coordinate.Y + 1] != null)
                        {
                            done = true;
                            sandAtRest++;
                            break;
                        }


                    }
                    if (!currentCannotMove)
                        map.Map[oldX, oldY] = null;
                }
                if (!done)
                    sandAtRest++;

            }
            return sandAtRest;
        }
    }
}
