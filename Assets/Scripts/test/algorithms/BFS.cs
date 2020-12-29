using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BFS
{
    public static void Search(Vertex[] vertices, Edge[] edges)
    {
        Search(vertices, edges, 0);
    }

    public static void Search(Vertex[] vertices, Edge[] edges, int startIndex)
    {
        List<Vertex> queue = new List<Vertex>() { vertices[startIndex] };
        Vertex v;
        Edge[] adjacencyList;
        Vertex adj;
        int order = 0;

        vertices[0].IsVisited = true;
        vertices[0].TextField.text = $"{vertices[0].Id}\n{order}";

        while (queue.Count > 0)
        {
            v = queue[0];
            adjacencyList = FindAdjacent(v, edges);
            for (int i = 0; i < adjacencyList.Length; i++)
            {
                adj = (adjacencyList[i].v1 != v) ? adjacencyList[i].v1 : adjacencyList[i].v2;
                if (!adj.IsVisited)
                {
                    adj.IsVisited = true;
                    queue.Add(adj);

                    order++;
                    adj.TextField.text = $"{adj.Id}\n{order}";
                    Debug.Log("Visited :: " + adj.Id);
                }

            }
            queue.RemoveAt(0);
        }
    }

    private static Edge[] FindAdjacent(Vertex v, Edge[] edges)
    {
        List<Edge> result = new List<Edge>();
        for (int i = 0; i < edges.Length; i++)
        {
            if (edges[i].v1 == v || edges[i].v2 == v)
                result.Add(edges[i]);
        }
        return result.ToArray();
    }
}
