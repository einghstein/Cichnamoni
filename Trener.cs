
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

        nodes.Add(new Node(Weights, [0.0, 0.0, 0.0]));
        nodes.Add(new Node(Weights, [0.0, 0.0, 0.0]));
        nodes.Add(new Node(Weights, [0.0, 0.0, 0.0]));
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
        // 1. Initialize an array to accumulate the total scores for each action
        double[] totalOutputs = { 0, 0, 0 };
        bool hasValidData = false;

        foreach (Node node in nodes)
        {
            double[] values = node.DoStuff(this.ActiveCichnamon, cichnamon);

            // Filter out negative values
            double[] validValues = values.Where(v => v >= 0).ToArray();

            // If this specific node has no valid values, skip it and move to the next node
            if (validValues.Length == 0)
            {
                Console.WriteLine("A node returned no valid positive values to normalize. Skipping node.");
                continue;
            }

            double min = validValues.Min();
            double max = validValues.Max();

            // Normalize and add to our running total
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] < 0)
                {
                    // Ignored values add 0 to the total score for this action
                    // (Using -1 here would penalize the action across nodes)
                    totalOutputs[i] += 0;
                }
                else if (max == min)
                {
                    totalOutputs[i] += 1.0;
                }
                else
                {
                    totalOutputs[i] += (values[i] - min) / (max - min);
                }
            }
            hasValidData = true;
        }

        // 2. Safety check: Ensure at least one node processed successfully
        if (!hasValidData)
        {
            Console.WriteLine($"Trainer is stoned so {ActiveCichnamon} did nothing");
            return;
        }

        // 3. Find the maximum value in the accumulated totalOutputs array
        double overallMax = totalOutputs.Max();
        int maxIndex = Array.IndexOf(totalOutputs, overallMax);

        // 4. Execute logic based on which position holds the biggest number
        switch (maxIndex)
        {
            case 0:
                ActiveCichnamon.PerformBasicAttack(cichnamon);
                break;
            case 1:
                ActiveCichnamon.PerformSpecialAttack(cichnamon);
                break;
            case 2:
                ActiveCichnamon.Heal(20);
                break;
            default:
                Console.WriteLine("Invalid action index calculated.");
                break;
        }
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
