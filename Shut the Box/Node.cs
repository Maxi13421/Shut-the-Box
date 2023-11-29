using System.Numerics;

namespace Shut_the_Box;



public class Node
{
    public double probability = 0;

    public int[] successors = new int[]{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1};

    public Node(double probability)
    {
        this.probability = probability;
    }

    public void UpdateProb(Node[] states)
    {
        double sum = 0;
        for (int aaa = 0; aaa < 13; aaa++)
        {
            if (successors[aaa] != -1)
            {
                sum += states[successors[aaa]].probability*Game.propabilities[aaa];
            }
        }

        probability = sum;
    }
}