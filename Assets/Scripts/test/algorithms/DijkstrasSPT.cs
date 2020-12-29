using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DijkstrasSPT
{
    public static Edge[] Search(Vertex[] vertices, Edge[] edges, int startIndex = 0)
    {
        List<Edge> SPT = new List<Edge>();
        Vertex v = vertices[startIndex];
        v.DistanceFromStart = 0;
        Edge e;
        int visited = 0;

        while(visited < vertices.Length)
        {
            int index = FindNotVisitedVertexWithMinimalDistance(vertices);
            if (index >= 0 && index < vertices.Length)
            {
                v = vertices[index];
                v.IsVisited = true;

                Edge[] adj = FindAdjacent(v, edges);
                for (int a = 0; a < adj.Length; a++)
                {
                    e = adj[a];
                    if (e.v1 != v)
                    {
                        float d = Vector2.Distance(v.transform.position, e.v1.transform.position);
                        if (!e.v1.IsVisited && v.DistanceFromStart + d < e.v1.DistanceFromStart)
                        {
                            e.v1.DistanceFromStart = v.DistanceFromStart + Vector2.Distance(v.transform.position, e.v1.transform.position);
                            e.v1.ParentVertex = v;
                        }
                    }
                    else
                    {
                        float d = Vector2.Distance(v.transform.position, e.v2.transform.position);
                        if (!e.v2.IsVisited && v.DistanceFromStart + d < e.v2.DistanceFromStart)
                        {
                            e.v2.DistanceFromStart = v.DistanceFromStart + Vector2.Distance(v.transform.position, e.v2.transform.position);
                            e.v2.ParentVertex = v;
                        }
                    }
                }

                if (v.ParentVertex != null)
                {
                    e = new Edge();
                    e.v1 = v;
                    e.v2 = v.ParentVertex;
                    SPT.Add(e);
                }
            }
            visited++;
        }
        return SPT.ToArray();
    }

    private static int FindNotVisitedVertexWithMinimalDistance(Vertex[] vertices)
    {
        int minIndex = -1;
        float minDistance = float.MaxValue;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (!vertices[i].IsVisited)
            {
                if (vertices[i].DistanceFromStart < minDistance)
                {
                    minDistance = vertices[i].DistanceFromStart;
                    minIndex = i;
                }
            }
        }

        return minIndex;
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
