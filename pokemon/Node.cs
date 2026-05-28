class Node
{ 
    double[] inputs;
    double[] outputs;
    double[][] weights;
    double[] biasis;

    private static Random rng = new Random();

    public Node (Cichnamon a, Cichnamon b)
    {
        this.inputs = [0, 0];
        this.outputs = [0, 0, 0];
        this.biasis = [rng.NextDouble(), rng.NextDouble(), rng.NextDouble()];
        for (int x; x < 1; x++)
        {
            for (int y; y < 2; y++)
            {
                this.weights[x][y] = rng.NextDouble();
            }
        }
    }

    public double[] DoStuff(Cichnamon a, Cichnamon b)
    {
        this.inputs = [a.CurrentHealth / a.MaxHealth, b.CurrentHealth / b.MaxHealth];
        this.outputs = [weights[0][0] * inputs[0] + weights[1][0] * inputs[1] + biasis[0],
                        weights[0][1] * inputs[0] + weights[1][1] * inputs[1] + biasis[1],
                        weights[0][2] * inputs[0] + weights[1][2] * inputs[1] + biasis[2]];
        foreach (var x in outputs)
        {
            if (x < 0)
            {
                x = 0;
            }
        }
        return outputs;


    }
}
