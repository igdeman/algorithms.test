using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DfsSearch : MonoBehaviour
{
    // Start is called before the first frame update
    int R = 3;
    int C = 3;

    List<string> dictionary = new List<string>{ "GEEKS", "FOR", "QUIZ", "GRQ", "BI" };
    char[,] board = {
        { 'G', 'I', 'Z' },
        { 'U', 'E', 'K' },
        { 'Q', 'S', 'E' }
    };

    void Start()
    {
        Search();
    }

    void Search()
    {
        bool[,] visited = new bool[R, C];
        for (int r = 0; r < R; r++)
        {
            for (int c = 0; c < C; c++)
            {
                DfsLoop(r, c, visited, "");
            }
        }
    }

    void DfsLoop(int row, int col, bool[,] visited, string result)
    {
        visited[row, col] = true;
        result += board[row, col];

        if (dictionary.Contains(result))
            Debug.Log("WORD FOUND: " + result);

        for (int r = row - 1; r <= row + 1 && r < R; r++)
        {
            for (int c = col - 1; c <= col + 1 && c < C; c++)
            {
                if (r >=0 && c >= 0 && !visited[r, c])
                    DfsLoop(r, c, visited, result);
            }
        }

        result = result.Remove(result.Length - 1);
        visited[row, col] = false;
    }
}
