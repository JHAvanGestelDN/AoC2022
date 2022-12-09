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
            var list = ReadFileToArray(PathOne).ToList();
            Node root = CreateFile(list);
            DetermineDirSizes(root);

            return directories.Where(d => d.size <= 100000).Sum(d => d.size);
        }

        public void DetermineDirSizes(Node root)
        {
            if (root.size != 0 || (root.directories.Count == 0 && root.files.Count == 0))
                return;

            if (root.directories.Count == 0)
            {
                root.size = root.files.Sum(f => f.size);
                return;
            }
            root.size = root.files.Sum(f => f.size);
            foreach (var dir in root.directories)
            {
                DetermineDirSizes(dir);
                root.size += dir.size;
            }
        }
        public List<Node> directories = new();


        private Node CreateFile(IEnumerable<string> commands)
        {
            Node parent = new Node(null, "root", 0);
            directories.Add(parent);
            return CreateFile(parent, commands.Skip(1).ToList()).getRoot();

        }

        private Node CreateFile(Node current, IReadOnlyCollection<string> commands)
        {
            if (commands.Count == 0)
                return current;
            //execute commands
            var split = commands.First().Split(' ');
            var newCommands = commands.Skip(1).ToList();
            if (split[0] == "$" && split[1] == "cd")
            {

                return CreateFile(
                split[2] == ".." ? current.parent : current.directories.FirstOrDefault(n => n.name == split[2])!,
                newCommands);
            }
            if (split[0] == "$" && split[1] == "ls")
            {
                return CreateFile(current, newCommands);
            }


            if (split[0] == "dir")
            {

                Node dir = new Node(current, split[1], 0);
                directories.Add(dir);
                current.directories.Add(dir);
                return CreateFile(current, newCommands);
            }

            if (long.TryParse(split[0], out long n))
            {
                Node file = new Node(current, split[1], n);
                current.files.Add(file);
                return CreateFile(current, newCommands);
            }

            return current;
        }


        protected override long SolveTwo()
        {
            const long fileSystemSize = 70000000;
            long usedSpace = directories.First(d => d.name == "root").size;
            const long neededSpace = 30000000;
            long minDirectorySize = (fileSystemSize - usedSpace - neededSpace) * -1;
            List<Node> potentialDirectoriesToDelete = directories.Where(d => d.size >= minDirectorySize).OrderBy(d => d.size).ToList();

            return potentialDirectoriesToDelete.First().size;
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
        public string toString()
        {
            return name;
        }
    }
}
