using Day00;
namespace Day07
{
    internal class Program : Base
    {
        private readonly List<Node> _directories = new List<Node>();
        public static void Main(string[] args)
        {
            new Program();
        }

        override protected long SolveOne()
        {
            var list = ReadFileToArray(PathOne).ToList();
            var root = CreateFile(list);
            DetermineDirSizes(root);

            return _directories.Where(d => d.size <= 100000).Sum(d => d.size);
        }

        private static void DetermineDirSizes(Node root)
        {
            if (root.size != 0 || root.directories.Count == 0 && root.files.Count == 0)
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


        private Node CreateFile(IEnumerable<string> commands)
        {
            var parent = new Node(null!, "root", 0);
            _directories.Add(parent);
            return CreateFile(parent, commands.Skip(1).ToList()).GetRoot();

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
                split[2] == ".." ? current.parent : current.directories.FirstOrDefault(node => node.name == split[2])!,
                newCommands);
            }
            if (split[0] == "$" && split[1] == "ls")
            {
                return CreateFile(current, newCommands);
            }


            if (split[0] == "dir")
            {

                var dir = new Node(current, split[1], 0);
                _directories.Add(dir);
                current.directories.Add(dir);
                return CreateFile(current, newCommands);
            }

            if (long.TryParse(split[0], out var n))
            {
                var file = new Node(current, split[1], n);
                current.files.Add(file);
                return CreateFile(current, newCommands);
            }

            return current;
        }


        override protected long SolveTwo()
        {
            const long fileSystemSize = 70000000;
            var usedSpace = _directories.First(d => d.name == "root").size;
            const long neededSpace = 30000000;
            var minDirectorySize = (fileSystemSize - usedSpace - neededSpace) * -1;
            var potentialDirectoriesToDelete = _directories.Where(d => d.size >= minDirectorySize).OrderBy(d => d.size).ToList();

            return potentialDirectoriesToDelete.First().size;
        }
    }

    public class Node
    {
        public Node(Node parent, string name, long size)
        {
            this.parent = parent;
            this.name = name;
            this.size = size;
            directories = new List<Node>();
            files = new List<Node>();
        }
        public Node parent { get; }
        public string name { get; }
        public long size { get; set; }
        public List<Node> directories { get; }
        public List<Node> files { get; }

        public Node GetRoot()
        {
            return parent == null ? this : parent.GetRoot();
        }
    }
}
