using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShortestPathSearch
{
    public static Edge[] Search(Vertex[] vertices, Edge[] edges, int StartIndex, int EndIndex)
    {
        List<Edge> result = new List<Edge>();
        Dictionary<int, int> verticesIdMap = new Dictionary<int, int>();
        for (int i = 0; i < vertices.Length; i++)
            verticesIdMap.Add(vertices[i].Id, i);

        vertices[StartIndex].DistanceFromStart = 0f;
        vertices[StartIndex].TextField.text = $"{vertices[StartIndex].Id}\n{0:0.00}\n{0:0.00}";

        for (int i = 0; i < vertices.Length-1; i++)
        {
            Vertex v = vertices[MinimalDistanceIndex(vertices, verticesIdMap)];
            v.IsVisited = true;
            Edge[] adjEdges = FindAdjacent(v, edges);

            //Debug.Log($"Visit vertex {v.Id}");

            for (int a = 0; a < adjEdges.Length; a++)
            {
                Vertex adjacent = (adjEdges[a].v1 != v) ? adjEdges[a].v1 : adjEdges[a].v2;
                int index = verticesIdMap[adjacent.Id];
                float distance = v.DistanceFromStart + Vector2.Distance(v.transform.position, adjacent.transform.position);

                //if (!spt[index] && distance < adjacent.DistanceFromStart)
                if (!adjacent.IsVisited && distance < adjacent.DistanceFromStart)
                {
                    adjacent.ParentVertex = v;
                    adjacent.DistanceFromStart = v.DistanceFromStart + Vector2.Distance(v.transform.position, adjacent.transform.position);
                    adjacent.TextField.text = $"{adjacent.Id}\n{Vector2.Distance(v.transform.position, adjacent.transform.position):0.00}\n{adjacent.DistanceFromStart:0.00}";
                }

                if (index == EndIndex)
                {
                    v = vertices[EndIndex];
                    while (index != StartIndex)
                    {
                        Edge e = new Edge();
                        e.v1 = v;
                        e.v2 = v.ParentVertex;
                        result.Add(e);

                        v = v.ParentVertex;
                        index = verticesIdMap[v.Id];
                    }
                    return result.ToArray();
                }
            }
        }
        return result.ToArray();
    }

    private static int MinimalDistanceIndex(Vertex[] vertices, Dictionary<int, int> verticesIdMap)
    {
        int min_index = -1;
        float min_dist = float.MaxValue;
        for (int i = 0; i < vertices.Length; i++)
        {
            if (vertices[i].DistanceFromStart < min_dist && !vertices[i].IsVisited)
            {
                min_index = i;
                min_dist = vertices[i].DistanceFromStart;
            }
        }
        return min_index;
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
