using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    void Start()
    {
        // # Test
        BinarySearchTest();

        // # Test
        UnionFindTest();
    }

    void BinarySearchTest()
    {
        Debug.Log("Binary Search Test");

        int key = 99;
        int[] input = new int[100000000];
        int inputValue = 0;
        for (int i = 0; i < input.Length; i++)
        {
            input[i] = inputValue;
            //inputValue += Random.Range(1, 5);
            inputValue += 5;
        }

        float start = Time.realtimeSinceStartup;
        bool result = BinarySearch.Search(input, key);
        Debug.Log($"{key} exist in the input array - {result}. \n Execution time: {Time.realtimeSinceStartup - start}");
    }

    void UnionFindTest()
    {
        Debug.Log("Union Find Test");

        UnionFind uf = new UnionFind(7);
        uf.Union(2, 3);
        uf.Union(4, 6);
        uf.Union(6, 5);
        uf.Union(0, 1);
        uf.Union(1, 3);

        Debug.Log($"is connected 0 - 5 = {uf.IsConnected(0, 5)}");
        Debug.Log($"is connected 4 - 6 = {uf.IsConnected(4, 6)}");
    }
}
