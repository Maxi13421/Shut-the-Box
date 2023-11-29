using System.Numerics;

namespace Shut_the_Box;



public class Node
{
    public double propability;

    public Edge[] successors = new Edge[13];

    public Node(double probability)
    {
        this.propability = probability;
    }

    public void UpdateProb()
    {
        double sum = 0;
        for (int aaa = 0; aaa < 13; aaa++)
        {
            if (successors[aaa] != null)
            {
                sum += successors[aaa].prob;
            }
        }

        propability = sum;
    }
}