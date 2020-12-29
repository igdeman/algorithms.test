using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class TestBFS : MonoBehaviour
{
    void Start()
    {
        Graph graph = GetComponent<Graph>();
        BFS.Search(GetComponentsInChildren<Vertex>(), graph.Edges);
    }
}
