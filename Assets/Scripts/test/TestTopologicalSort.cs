using System.Collections.Generic;
using UnityEngine;

public class TestTopologicalSort : MonoBehaviour
{
    public Vertex StartNode;

    void Start()
    {
        List<Vertex> topologicalOrder = TopologicalSort.Sort(StartNode, GetComponent<Graph>().Edges);
        for (int i = 0; i < topologicalOrder.Count; i++)
            topologicalOrder[i].TextField.text = i.ToString();
    }
}