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
            var list = ReadFileToArray(PathOneSample);
            HashSet<Coordinate> coordinates = new HashSet<Coordinate>();
            ParseRocks(list, coordinates);
            //determine the min x and y coordinates
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
            CharMap map = new CharMap(maxX - minX, maxY - minY);
            foreach (var coordinate in coordinates)
            {
                map.FillMap(coordinate,new CharGenericNode(coordinate,'#'));
            }
            map.Print();


            throw new System.NotImplementedException();
        }
        private void ParseRocks(string[] list, HashSet<Coordinate> coordinates)
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
        public Coordinate ParseCoordinate(string input)
        {
            var split = input.Split(",");
            var coordinate = new Coordinate(int.Parse(split[0]), int.Parse(split[1]));
            return coordinate;
        }

        override protected long SolveTwo()
        {
            throw new System.NotImplementedException();
        }
    }
}
