using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

public class MainClass
{

    public static string MinWindowSubstring(string[] strArr)
    {
        // [ "a**ab**", "ab" ], "ab"
        // [ "a**ba*c*a*", "abac" ], "ba*c*a"
        // [ "0123456789"]

        // [0-a, 3-b, 4-a, 6-c ] --> "a**ba*c" != "abc*a"
        // solution
        // getting all position for each char and its amount of repetions 
        // [a{2}[0, 4, 8] b{1}[3] c{1}[6]] 

        // separate unique values into a sorted list
        // [[3]{1}b [6]{1}c]   [a{2}[0, 4, 8]]

        // For each duplicated item pickup a value that decrease the distance compared to min or max value in the sorted list
        // Or if it can be placed in the middle
        // for each duplicated char
        //  first exists some index inside the range of unique indexes

        //  yes => place on sorted List of Unique indexes and remove index from duplicated list as well as reduce counter of repetitions
        // [[3]{1}b [4]{1}a [6]{1}c]   [a{1}[0, 8]]

        /// no => pick an index which will have less distance compare to first and last value of Unique indexers
        /// [[3]{1}b [4]{1}a [6]{1}c]   [a{1}[0, 8]]
        /// 0 < 3 true => d=3
        /// 
        /// 8 < 3 false =>
        /// 8 > 6 true => d=8-6=2
        /// 
        /// [0]d=3  y [8]d=2
        /// 
        /// then pick the least distance and add to Unique indexers
        /// [[3]{1}b [4]{1}a [6]{1}c [8]{1}a]   [a{0}[0]]
        /// 
        /// Since there is no more to process then return result
        /// [[3]{1}b [4]{1}a [6]{1}c [8]{1}a]   [a{0}[0]] == ba*c*a
        /// 
        
        string text = strArr[0];
        var characters = strArr[1].ToList();
        string minWindowString = null;
        var newText = text.ToList();
        int minIndex = int.MaxValue;
        int maxIndex = int.MinValue;
        Dictionary<char, (int, List<int>)> indexers = CreateIndexers(newText, characters);

        SortedList<int, char> sortedIndexers = new SortedList<int, char>();
        foreach (var idx in indexers)
        {
            if (idx.Value.Item1 == 1 && idx.Value.Item2.Count == 1)
            {
                sortedIndexers.Add(idx.Value.Item2.First(), idx.Key);
            }
        }

        minIndex = sortedIndexers.GetKeyAtIndex(0);
        maxIndex = sortedIndexers.GetKeyAtIndex(sortedIndexers.Count - 1);

        while(indexers.Count > 0)
        {
            var idx = indexers.First();
            var character = idx.Key;
            var count = idx.Value.Item1;
            var indexes = idx.Value.Item2;
            if (count <= 1 && indexes.Count <= 1)
            {
                indexers.Remove(character);
                continue;
            }
            
            SortedList<int, int> distances = new SortedList<int, int>();

            while (indexes.Count > 0)
            {
                int index = indexes.First();

                if (index > minIndex && index < maxIndex)
                {
                    sortedIndexers.Add(index, character);
                    indexes.Remove(index);
                    count--;
                }
                else
                {
                    int distance;
                    if (index < minIndex)
                    {
                        distance = minIndex - index;
                        distances.Add(distance, index);
                    }
                            
                    if(index > maxIndex)
                    {
                        distance = index - maxIndex;
                        distances.Add(distance, index);
                    }
                    
                    indexes.Remove(index);
                }
            }
            
            // add as many indexes are as count value of repetitions
            while (count > 0 && distances.Count > 0)
            {
                var distance = distances.First();
                sortedIndexers.Add(distance.Value, character);
                distances.Remove(distance.Key);
                count--;
            }

            
            indexers.Remove(character);
        }

        minWindowString = text.Substring(sortedIndexers.First().Key, sortedIndexers.Last().Key - sortedIndexers.First().Key + 1);
        return minWindowString;
    }

    public static Dictionary<char, (int, List<int>)> CreateIndexers(List<char> newText, List<char> characters)
    {
        Dictionary<char, (int, List<int>)> indexers = new Dictionary<char, (int, List<int>)>();
        for (int i = 0; i < characters.Count; i++)
        {
            char c = characters[i];
            List<int> charIndexes = new List<int>();

            if (indexers.ContainsKey(c))
            {
                int charCount = indexers[c].Item1;
                charIndexes = indexers[c].Item2;
                indexers[c] = (++charCount, charIndexes);
            }
            else
            {
                charIndexes = newText.Select((charToEvaluate, i) => c == charToEvaluate ? i : -1).Where(i => i != -1).ToList();
                indexers.Add(c, (1, charIndexes));
            }
        }

        return indexers;
    }

    private static void PrintIndexers(Dictionary<char, (int, List<int>)> indexers)
    {
        foreach (var idx in indexers)
        {

            var c = idx.Key;
            var count = idx.Value.Item1;
            var indexes = idx.Value.Item2;
            var strIndexes = string.Join(',', indexes);
            string output = $"{c}->{count}[{strIndexes}]\n";
            Console.WriteLine(output);
        }
    }

    static void Main()
    {

        // keep this function call here
        //Console.WriteLine(MinWindowSubstring(Console.ReadLine().Split(' ')));
        var indexers = CreateIndexers("a**ba*c*a*".ToList(), "abac".ToList());
        PrintIndexers(indexers);
        Console.ReadKey();
    }

}