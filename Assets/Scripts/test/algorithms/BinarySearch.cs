using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BinarySearch
{
    public static bool Search(int[] input, int key)
    {
        int l = 0;
        int h = input.Length - 1;
        int i = h / 2;
        while (l != i && h != i)
        {
            if (input[i] == key)
                return true;

            if (input[i] > key)
            {
                h = i;
                i = (h - l) / 2;
            }
            else
            {
                l = i;
                i = (l + h) / 2;
            }
        }
        return false;
    }
}
