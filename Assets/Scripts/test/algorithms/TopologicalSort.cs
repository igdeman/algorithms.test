using System.Collections.Generic;

public static class TopologicalSort
{
    public static List<Vertex> Sort(Vertex startNode, Edge[] edges)
    {
        List<Vertex> topologicalOrder = new List<Vertex>();
        DFS(startNode, edges, ref topologicalOrder);
        return topologicalOrder;
    }

    private static void DFS(Vertex v, Edge[] edges, ref List<Vertex> topologicalOrder)
    {
        Edge[] adjacencyList = FindAdjacent(v, edges);
        for (int i = 0; i < adjacencyList.Length; i++)
        {
            Vertex adj = adjacencyList[i].v2;
            if (!adj.IsVisited)
            {
                adj.IsVisited = true;
                Edge[] subAdjacencyList = FindAdjacent(adj, edges);
                if (subAdjacencyList.Length > 0)
                {
                    DFS(adj, edges, ref topologicalOrder);
                }
                topologicalOrder.Insert(0, adj);
            }
        }
        if (!v.IsVisited)
        {
            topologicalOrder.Insert(0, v);
            v.IsVisited = true;
        }
    }

    private static Edge[] FindAdjacent(Vertex v, Edge[] edges)
    {
        List<Edge> result = new List<Edge>();
        for (int i = 0; i < edges.Length; i++)
        {
            if (edges[i].v1 == v && !edges[i].v2.IsVisited)
                result.Add(edges[i]);
        }
        return result.ToArray();
    }
}