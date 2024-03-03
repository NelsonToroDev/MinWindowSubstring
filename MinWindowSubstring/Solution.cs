using System;

public class Solution
{

    public static string MinWindowSubstring(string[] strArr)
    {
        //text "a**ba*c*a*", block "ba*c*a", criteria "ba*c*a"
        Func<string, string, bool> match = (block, criteria) =>
        {
            for (int aa = 0; aa < block.Length; aa++)
            {
                var index = criteria.IndexOf(block[aa]);
                if (index >= 0)
                {
                    criteria = criteria.Remove(index, 1);
                    if (index == 0 && criteria.Length == 0)
                        return true;
                }
            }
            return false;
        };

        var text = strArr[0];
        var criteria = strArr[1];
        for (int blockSize = criteria.Length; blockSize <= text.Length; blockSize++)
        {
            for (int strPos = 0; strPos <= text.Length - blockSize; strPos++)
            {
                var candidate = text.Substring(strPos, blockSize);
                if (match(candidate, criteria))
                    return candidate;
            }
        }
        return "";
    }

}