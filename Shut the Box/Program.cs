

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
        //Console.WriteLine(Game.GetPrecessor(cur,cur2) + " " + Game.GetSumFromFlipCombination(cur2));
        if (states[Game.GetPrecessor(cur,cur2)].successors[Game.GetSumFromFlipCombination(cur2)] == -1 || states[cur].probability >
            states[states[Game.GetPrecessor(cur,cur2)].successors[Game.GetSumFromFlipCombination(cur2)]].probability)
        {
            states[Game.GetPrecessor(cur,cur2)].successors[Game.GetSumFromFlipCombination(cur2)] = cur;
            states[Game.GetPrecessor(cur,cur2)].UpdateProb(states);
        }
    }
    

}

for (int aaa = 0; aaa < 4096; aaa++)
{
    if (states[aaa] != null)
    {
        for (int aab = 0; aab < 13; aab++)
        {
            if (states[aaa].successors[aab] != -1)
            {
                if (states[states[aaa].successors[aab]].probability != states[states[aaa].successors[aab]].probability)
                {
                    Console.WriteLine();
                }
            }
        }
    }
}

Console.WriteLine(states[0].probability);