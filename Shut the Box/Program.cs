

using System.Collections;
using System.Globalization;
using System.Numerics;
using Shut_the_Box;

var states = new Node[4096];
states[4095] = new Node(1);
List<int> queue = new List<int>();
queue.Add(4095);

int index = 0;
while (queue.Count != 0)
{
    queue.Sort(((i, i1) =>
    {
        int curcopy = i;
        int tmp = 0;
        for (int aaa = 0; aaa < 12; aaa++)
        {
            tmp += curcopy % 2;
            curcopy = curcopy >> 1;
        } 
        int curcopy2 = i1;
        int tmp2 = 0;
        for (int aaa = 0; aaa < 12; aaa++)
        {
            tmp2 += curcopy2 % 2;
            curcopy2 = curcopy2 >> 1;
        }

        return tmp2 - tmp;
    }));
    //queue.Reverse();
    index++;
    int cur = queue[0];
    int curcopy = cur;
    int tmp = 0;
    for (int aaa = 0; aaa < 12; aaa++)
    {
        tmp += curcopy % 2;
        curcopy = curcopy >> 1;
    }

    if (index % 100 == 0)
    {
        
        //Console.WriteLine(index);
    }
    //Console.WriteLine(String.Join(", ", queue.ToArray()));
    
    queue.RemoveAt(0);
    List<List<int>> possiblePre = Game.GetPossiblePrecessors(cur);
    //Console.WriteLine();
    while(possiblePre.Count!=0)
    {
        
        List<int> cur2 = possiblePre.First();
        if (!queue.Contains(Game.GetPrecessor(cur,cur2)))
        {
            queue.Add(Game.GetPrecessor(cur,cur2));
        }
        possiblePre.RemoveAt(0);
        //Console.WriteLine(cur + " " + String.Join(", ", cur2.ToArray()));
        if (states[Game.GetPrecessor(cur,cur2)] == null)
        {
            states[Game.GetPrecessor(cur,cur2)] = new Node(0);
        }
        Edge newEdge = new Edge(cur, states[cur].propability*Game.propabilities[Game.GetSumFromFlipCombination(cur2)]);
        //Console.WriteLine(Game.GetPrecessor(cur,cur2) + " " + Game.GetSumFromFlipCombination(cur2));
        if (states[Game.GetPrecessor(cur,cur2)].successors[Game.GetSumFromFlipCombination(cur2)] == null || newEdge.prob >
            states[Game.GetPrecessor(cur,cur2)].successors[Game.GetSumFromFlipCombination(cur2)].prob)
        {
            states[Game.GetPrecessor(cur,cur2)].successors[Game.GetSumFromFlipCombination(cur2)] = newEdge;
            states[Game.GetPrecessor(cur,cur2)].UpdateProb();
        }
    }
    

}
Console.WriteLine(states[0].propability);