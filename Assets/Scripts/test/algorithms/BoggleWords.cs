using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TMPro;

public class BoggleWords : MonoBehaviour
{
    public TextMeshProUGUI letter1;
    public TextMeshProUGUI letter2;
    public TextMeshProUGUI letter3;
    public TextMeshProUGUI letter4;
    public TextMeshProUGUI letter5;
    public TextMeshProUGUI letter6;
    public TextMeshProUGUI letter7;
    public TextMeshProUGUI letter8;
    public TextMeshProUGUI letter9;

    static TextMeshProUGUI[,] objects;

    void Awake()
    {
        char[,] boggle = { { 'G', 'I', 'Z' },
                           { 'U', 'E', 'K' },
                           { 'Q', 'S', 'E' } };

        objects = new TextMeshProUGUI[,]{
            { letter1, letter2, letter3 },
            { letter4, letter5, letter6 },
            { letter7, letter8, letter9 },
        };

        //Debug.Log("Following words of dictionary are present");
        float startTime = Time.realtimeSinceStartup;
        float endTime = Time.realtimeSinceStartup;
        setLetters(boggle);
        findWords(boggle);
        endTime = Time.realtimeSinceStartup;
        //Debug.Log(endTime - startTime);
    }

    static readonly String[] dictionary = { "GEEKS", "FOR", "QUIZ", "IES", "BI" };
    static readonly int n = dictionary.Length;
    static readonly int M = 3, N = 3;

    // Prints all words present in dictionary. 
    static async void findWords(char[,] boggle)
    {
        // Mark all characters as not visited 
        bool[,] visited = new bool[M, N];

        // Initialize current string 
        String str = "";

        // Consider every character and look for all words 
        // starting with this character 
        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < N; j++)
            {
                //findWordsUtil(boggle, visited, i, j, str);
                await findWordsUtil(boggle, visited, i, j, str);
            }
        }
    }

    // A recursive function to print all words present on boggle 
    //static void findWordsUtil(char[,] boggle, bool[,] visited, int i, int j, String str, int recursionDepth=0)
    static async Task findWordsUtil(char[,] boggle, bool[,] visited, int i, int j, String str, int recursionDepth = 0)
    {
        if (recursionDepth == 0)
        {
            //Debug.Log("!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! RECURSION DEPTH :: " + recursionDepth);
            //Debug.Log("ROW :: " + i);
            //Debug.Log("COL :: " + j);
        }
        // Mark current cell as visited and 
        // append current character to str 
        visited[i, j] = true;
        str = str + boggle[i, j];

        markVisited(visited);
        markObject(i, j, new Color(0f, 0f, 1f));

        // If str is present in dictionary, 
        // then print it 
        if (isWord(str))
        {
            //Debug.Log(str);
            await Task.Delay(1000*10);
        }

        // Traverse 8 adjacent cells of boggle[i,j] 
        for (int row = i - 1; row <= i + 1 && row < M; row++)
        {
            for (int col = j - 1; col <= j + 1 && col < N; col++)
            {
                if (row >= 0 && col >= 0 && !visited[row, col])
                {
                    //Debug.Log("d=" + recursionDepth + " r=" + row + " c=" + col + " i=" + i + " j=" + j);
                    int depth = recursionDepth + 1;
                    await Task.Delay(400);
                    await findWordsUtil(boggle, visited, row, col, str, depth);
                    //findWordsUtil(boggle, visited, row, col, str, depth);
                }
            }
        }

        // Erase current character from string and  
        // mark visited of current cell as false 
        str = "" + str[str.Length - 1];
        visited[i, j] = false;
        clearVisited();
        //markVisited(visited);
    }

    static void setLetters(char[,] boggle)
    {
        for (int r = 0; r < M; r++)
        {
            for (int c = 0; c < N; c++)
            {
                objects[r, c].text = boggle[r, c].ToString();
            }
        }
    }

    static void markVisited(bool[,] visited)
    {
        for (int r = 0; r < M; r++)
        {
            for (int c = 0; c < N; c++)
            {
                if (visited[r, c])
                    markObject(r, c, new Color(1f, 0f, 0f));
                    //objects[r, c].color = new Color(1f, 0f, 0f);
            }
        }
    }

    static void markObject(int r, int c, Color co)
    {
        objects[r, c].color = co;
    }

    static void clearVisited()
    {
        for (int r = 0; r < M; r++)
        {
            for (int c = 0; c < N; c++)
            {
                objects[r, c].color = new Color(1f, 1f, 1f);
            }
        }
    }
    // A given function to check if a given string  
    // is present in dictionary. The implementation is  
    // naive for simplicity. As per the question  
    // dictionary is given to us. 
    static bool isWord(String str)
    {
        //Debug.Log("CHECK WORD :: " + str);
        // Linearly search all words 
        for (int i = 0; i < n; i++)
            if (str.Equals(dictionary[i]))
                return true;
        return false;
    }
}
