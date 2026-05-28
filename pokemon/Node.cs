public class Node
{
    double[] inputs;
    double[] outputs;
    double[][] weights;
    double[] biasis;

    private static Random rng = new Random();

    public Node(double[][] TweaksWeights, double[] TweaksBiasis)
    {
        this.weights = TweaksWeights;
        this.biasis = [rng.NextDouble() + TweaksBiasis[0], rng.NextDouble() + TweaksBiasis[1], rng.NextDouble() + TweaksBiasis[2]];
        for (int x = 0; x < 1; x++)
        {
            for (int y = 0; y < 2; y++)
            {
                this.weights[x][y] += rng.NextDouble();
            }
        }
    }

    public double[] DoStuff(Cichnamon a, Cichnamon b)
    {
        this.inputs = [a.CurrentHealth / a.MaxHealth, b.CurrentHealth / b.MaxHealth];
        this.outputs = [weights[0][0] * inputs[0] + weights[1][0] * inputs[1] + biasis[0],
                        weights[0][1] * inputs[0] + weights[1][1] * inputs[1] + biasis[1],
                        weights[0][2] * inputs[0] + weights[1][2] * inputs[1] + biasis[2]];
        for (int i = 0; i < 2; i++)
        {
            if (outputs[i] < 0)
            {
                this.outputs[i] = 0;
            }

        }
        return outputs;


    }
}
