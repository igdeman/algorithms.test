using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Vertex : MonoBehaviour
{
    public int Id;
    public bool IsVisited;
    public float DistanceFromStart = float.MaxValue;
    public Vertex ParentVertex;
    public Vertex[] AdjacencyList;

    public TextMesh TextField;

    private List<Vertex> adjacencyList = new List<Vertex>();
    private string text;

    void Update()
    {
        bool changed = transform.hasChanged;
        if (isListChanged())
        {
            adjacencyList = new List<Vertex>(AdjacencyList);
            changed = true;
        }
        if (text != TextField.text)
        {
            text = TextField.text;
            changed = true;
        }
        transform.hasChanged = changed;
    }

    bool isListChanged()
    {
        if (AdjacencyList != null && AdjacencyList.Length != adjacencyList.Count)
            return true;
        for (int i = 0; i < AdjacencyList.Length; i++)
        {
            if (AdjacencyList[i] != adjacencyList[i])
                return true;
        }
        return false;
    }
}