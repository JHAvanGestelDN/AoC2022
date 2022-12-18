using Day00;
using Day00.Maps;
using Day00.Nodes;

namespace Day12
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
            var map = CreateMap(list, out var start, out var end);
            map.FillNeighbours();

            return Pathfinding(start, end);
        }
        private static int Pathfinding(CharGenericNode? start, CharGenericNode? end)
        {

            HashSet<CharGenericNode> openList = new HashSet<CharGenericNode>();
            HashSet<CharGenericNode> closedList = new HashSet<CharGenericNode>();
            start.cost = 0;
            start.heuristic = start.CalculateHeuristic(end);

            openList.Add(start);
            while (openList.Count > 0)
            {
                CharGenericNode node = openList.OrderBy(n => n.heuristic + n.cost).First();
                openList.Remove(node);
                closedList.Add(node);
                if (node == end)
                    break;
                foreach (var neighbour in node.Neighbours)
                {
                    if (closedList.Contains(neighbour))
                        continue;
                    int potentialBetterCost = node.cost + 1;
                    neighbour.heuristic = neighbour.CalculateHeuristic(end);
                    if (neighbour.cost > potentialBetterCost)
                    {
                        neighbour.cost = potentialBetterCost;
                        neighbour.previous = node;
                    }
                    openList.Add(neighbour);
                }
            }
            return end.cost;
        }
        private static CharMap CreateMap(string[] list, out CharGenericNode? start, out CharGenericNode? end)
        {

            CharMap map = new CharMap(list.Length, list[0].Length);
            start = null;
            end = null;
            for (var i = 0; i < list.Length; i++)
            {
                string s = list[i];
                for (var j = 0; j < s.Length; j++)
                {
                    var coordinate = new Coordinate(i, j);
                    var node = new CharGenericNode(coordinate, s[j]);
                    map.FillMap(coordinate, node);
                    if (s[j] == 'S')
                        start = node;
                    if (s[j] == 'E')
                        end = node;
                }
            }
            return map;
        }


        override protected long SolveTwo()
        {
            var list = ReadFileToArray(PathOne);
            var map = CreateMap(list, out var start, out var end);
            map.FillNeighbours();
            List<CharGenericNode> lowestElevationNodes = map.GetLowestElevationNodes();
            int lowestCost = int.MaxValue;
            foreach (var lowestElevationNode in lowestElevationNodes)
            {
                map.ResetCost();
                int tmpCost = Pathfinding(lowestElevationNode, end);
                if (tmpCost < lowestCost)
                    lowestCost = tmpCost;
            }

            
            return lowestCost;
        }
    }
}
