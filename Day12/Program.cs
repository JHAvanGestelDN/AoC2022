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
            CharMap map = new CharMap(list.Length, list[0].Length);
            CharGenericNode start = null;
            CharGenericNode end = null;
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
            end.Value = '{'; //ASCII charcter after z
            //fill neighbours
            map.FillNeighbours();

            List<CharGenericNode> visited = new List<CharGenericNode>();
            PriorityQueue<CharGenericNode, int> queue = new PriorityQueue<CharGenericNode, int>();
            visited.Add(start);
            start.cost = 0;
            start.Neighbours.ForEach(n =>
            {
                n.previous= start;
                n.cost = start.cost+1;
                queue.Enqueue(n, n.cost);
            });
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Value == 'k' )
                    Console.WriteLine(" ");
                foreach (var charGenericNode in node.Neighbours.Where(n=>!visited.Contains(n)))
                {

                    int tempDistance = node.cost + 1;
                    if (tempDistance < charGenericNode.cost)
                    {
                        
                        charGenericNode.previous = node;
                        charGenericNode.cost = tempDistance;
                        queue.Enqueue(charGenericNode, charGenericNode.cost);
                    }
                }
                visited.Add(node);
                if (node == end)
                    break;
            }
            
            
        
        return end.cost;
        }
    

    override protected long SolveTwo()
    {
        throw new System.NotImplementedException();
    }
}
}
