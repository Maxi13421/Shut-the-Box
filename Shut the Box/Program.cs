

using System.Collections;
using System.Globalization;
using System.Numerics;
using Shut_the_Box;





class Program
{
    static void Main(string[] args)
    {

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
            index++;
            int cur = queue[0];
            int curcopy = cur;
            int tmp = 0;
            for (int aaa = 0; aaa < 12; aaa++)
            {
                tmp += curcopy % 2;
                curcopy = curcopy >> 1;
            }


            queue.RemoveAt(0);
            List<List<int>> possiblePre = Game.GetPossiblePrecessors(cur);
            while (possiblePre.Count != 0)
            {

                List<int> cur2 = possiblePre.First();
                if (!queue.Contains(Game.GetPrecessor(cur, cur2)))
                {
                    queue.Add(Game.GetPrecessor(cur, cur2));
                }

                possiblePre.RemoveAt(0);
                if (states[Game.GetPrecessor(cur, cur2)] == null)
                {
                    states[Game.GetPrecessor(cur, cur2)] = new Node(0);
                }

                if (states[Game.GetPrecessor(cur, cur2)].successors[Game.GetSumFromFlipCombination(cur2)] == -1 ||
                    states[cur].probability > //Flip to < for worst strategy
                    states[states[Game.GetPrecessor(cur, cur2)].successors[Game.GetSumFromFlipCombination(cur2)]]
                        .probability)
                {
                    states[Game.GetPrecessor(cur, cur2)].successors[Game.GetSumFromFlipCombination(cur2)] = cur;
                    states[Game.GetPrecessor(cur, cur2)].UpdateProb(states);
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
                        if (states[states[aaa].successors[aab]].probability !=
                            states[states[aaa].successors[aab]].probability)
                        {
                            Console.WriteLine();
                        }
                    }
                }
            }
        }

        int wins = 0;
        Random random = new Random();
        int numberGames = 100000;
        for (int aaa = 0; aaa < numberGames; aaa++)
        {
            List<int> curStates = new List<int>();
            List<int> curRolls = new List<int>();
            int curState = 0;
            while (curState != 4095)
            {
                int roll = random.Next(1, 7) + random.Next(1, 7);
                if (states[curState].successors[roll] == -1)
                {
                    break;
                }

                curState = states[curState].successors[roll];
                curStates.Add(curState);
                curRolls.Add(roll);
                if (curState == 4095)
                {
                    wins++;
                    for (int aab = 0; aab < curStates.Count; aab++)
                    {
                        Console.Write(curRolls[aab] + " " + intToString(curStates[aab]) + "  ");
                    }

                    Console.WriteLine();
                }
            }

        }

        Console.WriteLine(((double)wins) / numberGames);
        Console.WriteLine(wins);
        Console.WriteLine(numberGames);
        Console.WriteLine(states[0].probability);

    }
    
    public static String intToString(int integer)
    {
        String output = "";
        for (int aaa = 31; aaa > 19; aaa--)
        {
            //Console.WriteLine(integer + "   " + );
            if ((integer >> (31 - aaa) & 1) == 1)
            {
                output += "1";
            }
            else
            {
                output += "0";
            }
        }

        return output;
    }
    
}