using System.Numerics;

namespace Shut_the_Box;

public class Edge
{
    public double prob;

    public int destination;

    public Edge(int destination, double prob)
    {
        this.prob = prob;
        this.destination = destination;
    }
}