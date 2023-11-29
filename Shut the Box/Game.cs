using System.Collections.Specialized;

namespace Shut_the_Box;

public class Game
{
    public static double[] propabilities = new double[] {0,0,1d/36,1d/18, 1d/12,1d/9,5d/36,1d/6,5d/36,1d/9,1d/12,1d/18,1d/36};
    public static List<List<int>> GetPossiblePrecessors(int state)
    {
        int copy = state;
        List<List<int>> output = new List<List<int>>();
        List<int> posFlips = new List<int>();
        for (int i = 0;copy != 0;i++)
        {
            if (copy % 2 == 1)
            {
                posFlips.Add(i);
            }

            copy = copy >> 1;
        }

        if (posFlips.Count == 0)
        {
            return output;
        }

        for (int aaa = 0; aaa < 1 << (posFlips.Count); aaa++)
        {
            List<int> posCombination = new List<int>();
            int sum = 0;
            for (int aab = 0; aab < posFlips.Count; aab++)
            {
                
                if ((aaa & (1<<aab)) == 1<<aab)
                {
                    posCombination.Add(posFlips[aab]);
                    sum += posFlips[aab] + 1;
                }
            }

            if (sum >= 2 && sum <= 12 // && posCombination.Count<=2 //Max 2 flips
                )
            {
                output.Add(new List<int>(posCombination));
            }
        }

        
        return output;
    }

    public static int GetPrecessor(int cur, List<int> flips)
    {
        for(int aaa=0;aaa<flips.Count;aaa++)
        {
            cur -= 1 << flips[aaa];
        }

        return cur;
    }

    public static int GetSumFromFlipCombination(List<int> flips)
    {
        int cur = 0;
        for(int aaa=0;aaa<flips.Count;aaa++)
        {
            cur += flips[aaa]+1;
        }

        return cur;
    }
    
    
    
}