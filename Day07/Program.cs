using System.Collections;
using Day00;

namespace Day07
{
    internal class Program : Base
    {
        public static void Main(string[] args)
        {
            Program p = new();
        }

        protected override long SolveOne()
        {
            var list = ReadFileToArray(PathOneSample).ToList();
            Node root = CreateDirectory(list);

            CalculateSize(root);
            var x =  directorySizes.Where(d => d.Value <= 100000).ToList();
          
            return x.Sum(x=>x.Value);
        }

        public Dictionary<string, long> directorySizes = new Dictionary<string, long>();
        private long CalculateSize(Node n)
        {
            long fileSize = n.files.Sum(f => f.size);
   
            if (n.directories.Count == 0)
            {
                directorySizes[n.name] = fileSize;
                return fileSize;
            }
            else
            {
                long directorySize = 0;
                foreach (var nDirectory in n.directories)
                {
                    directorySize += CalculateSize(nDirectory);
                }
                directorySizes[n.name] = directorySize+ fileSize;
                return directorySize + fileSize;
            }


        }

        private Node CreateDirectory(List<string> commands)
        {
            Node parent = new Node(null, "root", 0);
            return CreateDirectory(parent, commands.Skip(1).ToList()).getRoot();
            
        }

        private Node CreateDirectory(Node current, List<string> commands)
        {
            if (commands.Count == 0)
                return current;
            //execute commands
            var split = commands.First().Split(' ');
            var newCommands = commands.Skip(1).ToList();
            if (split[0] == "$" && split[1] == "cd")
            {
                return CreateDirectory(
                    split[2] == ".." ? current.parent : current.directories.FirstOrDefault(n => n.name == split[2])!,
                    newCommands);
            }
            if (split[0] == "$" && split[1] == "ls")
            {
                return CreateDirectory(current, newCommands);
            }
            

            if (split[0] == "dir")
            {
                current.directories.Add(new Node(current, split[1], 0));
                return CreateDirectory(current, newCommands);
            }

            if (long.TryParse(split[0], out long n))
            {
                current.files.Add(new Node(current, split[1], n));
                return CreateDirectory(current, newCommands);
            }

            return current;
        }


        protected override long SolveTwo()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Node
    {
        public Node parent { get; set; }
        public string name { get; set; }
        public long size { get; set; }
        public List<Node> directories { get; set; }
        public List<Node> files { get; set; }

        public Node(Node parent, string name, long size)
        {
            this.parent = parent;
            this.name = name;
            this.size = size;
            directories = new List<Node>();
            files = new List<Node>();
        }

        public Node getRoot()
        {
            return parent == null ? this : parent.getRoot();
        }
    }
}