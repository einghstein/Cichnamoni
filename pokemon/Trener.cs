
public class Trainer
{
    public string Name { get; set; }
    public int Level { get; set; }
    public List<Cichnamon> Cichnamons { get; set; }
    public Cichnamon ActiveCichnamon { get; set; }
    public List<Node> nodes { get; set; }

    public Trainer(string name)
{
    Name = name;
    Level = 1;
    Cichnamons = new List<Cichnamon>();
    nodes = new List<Node>();

        // 1. Vytvoříme hlavní pole o velikosti 1
        double[][] Weights = new double[2][];
        Weights[0] = new double[3]; // Automaticky obsahuje [0.0, 0.0]
        Weights[1] = new double[3];

        nodes.Add(new Node(Weights, [0.0, 1.0, 0.0]));
}


    public void ChooseActiveCichnamon(int index)
    {
        if (index >= 0 && index < Cichnamons.Count)
        {
            ActiveCichnamon = Cichnamons[index];
        }
    }

    public void turn(Cichnamon cichnamon)
    {

    }

    public void AddCichnamon(Cichnamon cichnamon)
    {
        Cichnamons.Add(cichnamon);
    }

    public bool HasAliveCichnamons()
    {
        return Cichnamons.Any(c => c.IsAlive());
    }
}
