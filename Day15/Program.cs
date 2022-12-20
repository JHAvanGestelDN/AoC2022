using System.Diagnostics;
using Day00;

namespace Day15
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
            //int row = 10;
            int row = 2000000;

            var sensors = Parse(list, out var beacons);
            var blockedCoordinates = new HashSet<Coordinate>();
            foreach (var coordinate in sensors.Select(sensor => sensor.GenerateCoordinatesWhereNoBeaconCanExits(row)).SelectMany(x => x))
            {
                blockedCoordinates.Add(coordinate);
            }
            foreach (var beacon in beacons)
            {
                blockedCoordinates.Remove(beacon.Coordinate);
            }

            return blockedCoordinates.Count(c => c.Y == row);
        }
        private static List<Sensor> Parse(string[] list, out List<Beacon> beacons)
        {

            List<Sensor> sensors = new List<Sensor>();
            beacons = new List<Beacon>();
            foreach (var s in list)
            {
                var split = s.Split(": closest beacon is at x=");
                var firstPart = split[0].Split("Sensor at x=")[1].Split(", y=");
                int x = int.Parse(firstPart[0]);
                int y = int.Parse(firstPart[1]);
                var secondPart = split[1].Split(", y=");
                int bx = int.Parse(secondPart[0]);
                int by = int.Parse(secondPart[1].Split(", z=")[0]);
                Coordinate sensorCoordinate = new Coordinate(x, y);
                Coordinate beaconCoordinate = new Coordinate(bx, by);
                Sensor sensor = new Sensor()
                {
                Coordinate = sensorCoordinate,
                };
                sensors.Add(sensor);
                Beacon beacon = new Beacon()
                {
                Coordinate = beaconCoordinate,
                };
                int indexOfBeacon = beacons.IndexOf(beacon);
                if (indexOfBeacon >= 0)
                {
                    beacon = beacons[indexOfBeacon];
                    sensor.Beacon = beacon;
                    beacon.Sensors.Add(sensor);
                    continue;
                }
                sensor.Beacon = beacon;
                beacon.Sensors.Add(sensor);
                beacons.Add(beacon);
            }
            return sensors;
        }

        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            //int searchSpaceLimit = 20;
            const int searchSpaceLimit = 4000000;

            var sensors = Parse(list, out var beacons);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i <= searchSpaceLimit; i++)
            {
                var rowRanges = new List<Range>();
                foreach (var sensor in sensors)
                {
                    var range = sensor.GenerateRange(i, searchSpaceLimit);
                    if (range != null)
                        rowRanges.Add(range);
                }
                var mergedRanges = MergeRanges(rowRanges);
                var count = mergedRanges.Sum(r => r.Size());
                if (count != searchSpaceLimit + 1)
                {
                    var end = mergedRanges.First().end + 1;
                    return 4000000L * end + i;
                }
                if (i % 100000 == 0)
                    Console.WriteLine($"Currently at {i} in {sw.ElapsedMilliseconds}ms");
            }
            sw.Stop();


            return 0;
        }
        private static List<Range> MergeRanges(IEnumerable<Range> rowRanges)
        {
            List<Range> mergedRanges = new List<Range>();


            foreach (var range in rowRanges.OrderBy(r => r.start))
            {
                Range first = mergedRanges.FirstOrDefault()!;
                if (mergedRanges.Count == 0)
                {
                    mergedRanges.Add(range);
                    continue;
                }
                if (range.start <= first.end + 1 && range.end > first.end)
                {
                    mergedRanges.First().end = range.end;
                    continue;
                }
                if (range.start >= first.start && range.end <= first.end)
                {
                    continue;
                }
                mergedRanges.Add(range);
            }

            return mergedRanges;
        }
    }
    public class Sensor
    {
        public Beacon Beacon { get; set; }
        public Coordinate Coordinate { get; set; }

        public int CalcManhattanDistance()
        {
            return Math.Abs(Coordinate.X - Beacon.Coordinate.X) + Math.Abs(Coordinate.Y - Beacon.Coordinate.Y);
        }
        public Range GenerateRange(int row, int limit)
        {
            int manhattanDistance = CalcManhattanDistance();
            if (row < Coordinate.Y - manhattanDistance || row > Coordinate.Y + manhattanDistance)
                return null;

            var x = Coordinate.X - (manhattanDistance - Math.Abs(Coordinate.Y - row));
            x = Math.Max(0, x);
            var y = Coordinate.X + (manhattanDistance - Math.Abs(Coordinate.Y - row));
            y = Math.Min(limit, y);
            return new Range(x, y);
        }

        public HashSet<Coordinate> GenerateCoordinatesWhereNoBeaconCanExits(int row)
        {
            int manhattanDistance = CalcManhattanDistance();
            HashSet<Coordinate> coordinates = new HashSet<Coordinate>();

            if (Coordinate.Y + manhattanDistance < row || Coordinate.Y - manhattanDistance > row)
                return coordinates; //filter out

            for (int x = Coordinate.X - manhattanDistance; x <= Coordinate.X + manhattanDistance; x++)
            {
                var c = new Coordinate(x, row);
                if (Math.Abs(c.X - Coordinate.X) + Math.Abs(c.Y - Coordinate.Y) <= manhattanDistance)
                    coordinates.Add(c);
            }
            return coordinates;
        }
    }
    public class Range
    {
        public int start { get; set; }
        public int end { get; set; }
        public Range(int start, int end)
        {
            this.start = start;
            this.end = end;
        }
        public int Size()
        {
            return end - start + 1;
        }
    }
    public class Beacon
    {
        public List<Sensor> Sensors { get; set; } = new List<Sensor>();
        public Coordinate Coordinate { get; set; }
        protected bool Equals(Beacon other)
        {
            return Coordinate.Equals(other.Coordinate);
        }
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Beacon)obj);
        }
        public override int GetHashCode()
        {
            return Coordinate.GetHashCode();
        }
    }
}
