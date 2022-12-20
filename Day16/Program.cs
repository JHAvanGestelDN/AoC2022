using System.Runtime.CompilerServices;
using Day00;

namespace Day16
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
            HashSet<Valve> valves = new HashSet<Valve>();
            Parse(list, valves);
            Game g = new Game();
            g.valves = valves;
            g.currentValve = valves.First(v => v.Name == "AA");
            g.Run();
            throw new System.NotImplementedException();
        }
        private static void Parse(string[] list, HashSet<Valve> valves)
        {

            foreach (var s in list)
            {
                var splitString = s.Contains("tunnels") ? "; tunnels lead to valves " : "; tunnel leads to valve ";
                var split = s.Split(splitString);
                var valveSplit = split[0].Split(" has flow rate=");
                var name = valveSplit[0].Remove(0, 6);
                var flow = int.Parse(valveSplit[1]);
                Valve v = new Valve()
                {
                Name = name,
                Flow = flow
                };
                valves.Add(v);
                var tunnels = split[1].Split(", ");
                foreach (var t in tunnels)
                {
                    Valve vt = new Valve()
                    {
                    Name = t
                    };
                    //see if hashset has vt
                    if (valves.Contains(vt))
                    {
                        vt = valves.First(x => x.Name == t);
                    }
                    v.Neighbours.Add(vt);

                }
            }
        }

        override protected long SolveTwo()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Game
    {
        public HashSet<Valve> valves = new HashSet<Valve>();
        public Valve currentValve { get; set; }
        public bool ended { get { return turn <= 0; } }
        public int turn = 30;
        public int pressure = 0;
        public static Game bestState = new Game();

        public List<Action> GenerateActions()
        {
            List<Action> actions = new List<Action>();
            foreach (var n in currentValve.Neighbours)
            {
                actions.Add(new Action()
                {
                actionName = n.Name
                });
            }
            if (!currentValve.Openend)
                actions.Add(new Action() { actionName = "Open" });
            return actions;
        }
        public void Run()
        {
            var actions = GenerateActions();
            foreach (var a in actions)
            {
                var newGame = Clone();
                newGame.pressure += newGame.valves.Where(v => v.Openend).Sum(v => v.Flow);
                newGame.DoAction(a);
                if (newGame.ended)
                {
                    bestState = newGame.pressure > bestState.pressure ? newGame : bestState;
                    continue;
                }
                newGame.Run();
            }
        }

        public Game Clone()
        {
            Game g = new Game
            {
            valves = new HashSet<Valve>(valves),
            currentValve = currentValve,
            turn = turn,
            pressure = pressure
            };
            return g;
        }
        public void DoAction(Action action)
        {
            if (action.actionName == "Open")
            {
                currentValve.Openend = true;
                turn--;
                return;
            }
            var valve = valves.First(x => x.Name == action.actionName);
            currentValve = valve;
            turn--;
        }
    }
}
public class Action
{
    public int cost { get; set; } = 1;
    public string actionName { get; set; }
}

public class Valve
{
    public string Name { get; set; }
    public int Flow { get; set; }
    public HashSet<Valve> Neighbours { get; set; } = new HashSet<Valve>();

    public bool Openend { get; set; }
    protected bool Equals(Valve other)
    {
        return Name == other.Name;
    }
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != this.GetType())
            return false;
        return Equals((Valve)obj);
    }
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}
