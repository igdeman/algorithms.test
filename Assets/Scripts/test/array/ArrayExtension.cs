using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public static class ArrayExtension
{
    public static T[] FindMissing1<T>(T[] range, T[] values)
    {
        List<T> result = Enumerable.Except<T>(range, values).ToList<T>();
        return result.ToArray<T>();
    }

    public static T[] FindMissing2<T>(T[] range, T[] values)
    {
        List<T> result = new List<T>();
        Dictionary<T, T> hash = new Dictionary<T, T>(values.Length);
        for (int i = 0; i < values.Length; i++)
            hash.Add(values[i], values[i]);

        for (int i = 0; i < range.Length; i++)
        {
            if (!hash.ContainsKey(range[i]))
                result.Add(range[i]);
        }

        return result.ToArray<T>();
    }

    public static T[] FindDuplicates<T>(T[] values)
    {
        List<T> result = new List<T>();
        Dictionary<T, T> hash = new Dictionary<T, T>();
        for (int i = 0; i < values.Length; i++)
        {
            if (hash.ContainsKey(values[i]))
                result.Add(values[i]);
            else
                hash.Add(values[i], values[i]);
        }
        return result.ToArray();
    }

    public static int[] FindSmallestAndLargestNumbers(int[] values)
    {
        int smallest = int.MaxValue;
        int largest = int.MinValue;
        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] < smallest)
                smallest = values[i];
            if (values[i] > largest)
                largest = values[i];
        }
        return new int[] { smallest, largest };
    }

    public static void FindPairsOfSum(int[] values, int sum)
    {
        for (int i = 0; i < values.Length; i++)
        {
            for (int r = 0; r < values.Length; r++)
            {
                if (i != r)
                {
                    if (values[i] + values[r] == sum)
                        Debug.Log($"{values[i]} + {values[r]} = {sum}");
                }
            }
        }
    }
}
