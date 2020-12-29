using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayManipulationTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float start;
        float duration;



        // #1 How do you find the missing number in a given integer array of 1 to 100?
        int rangeLength = 1000000;
        int[] range = Enumerable.Range(0, rangeLength).ToArray();
        int[] values = new int[rangeLength / 5];
        int[] missing;
        for (int i = 0; i < values.Length; i ++)
            values[i] = i * 5;

        start = Time.realtimeSinceStartup;
        missing = ArrayExtension.FindMissing1<int>(range, values);
        duration = Time.realtimeSinceStartup - start;
        Debug.Log($"FindMissing1 Total time: {duration:0.0000}");
        Debug.Log($"Total Results: {missing.Length}");

        start = Time.realtimeSinceStartup;
        missing = ArrayExtension.FindMissing2<int>(range, values);
        duration = Time.realtimeSinceStartup - start;
        Debug.Log($"FindMissing2 Total time: {duration:0.0000}");
        Debug.Log($"Total Results: {missing.Length}");



        // #2 How do you find the duplicate number on a given integer array?
        List<int> duplicateValues = new List<int>();
        for (int i = 0; i < 100; i++)
            duplicateValues.Add(i);
        for (int i = 0; i < 20; i++)
            duplicateValues.Add(i+20);

        int[] duplicates = ArrayExtension.FindDuplicates<int>(duplicateValues.ToArray());
        Debug.Log($"Total duplicates: {duplicates.Length}");
        //for (int i = 0; i < duplicates.Length; i++)
        //    Debug.Log($"Duplicate: {duplicates[i]}");



        // #3 How do you find the largest and smallest number in an unsorted integer array?
        int[] smallestAndLargestValues = ArrayExtension.FindSmallestAndLargestNumbers(Enumerable.Range(0, 100).ToArray());
        Debug.Log($"Smallest Integer: {smallestAndLargestValues[0]}");
        Debug.Log($"Largest Integer: {smallestAndLargestValues[1]}");

        // #4 How do you find all pairs of an integer array whose sum is equal to a given number?
        ArrayExtension.FindPairsOfSum(Enumerable.Range(0, 100).ToArray(), 20);
    }
}
