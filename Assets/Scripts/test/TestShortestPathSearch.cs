using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Graph))]
public class TestShortestPathSearch : MonoBehaviour
{
    public int StartIndex;
    public int EndIndex;

    private List<GameObject> edgeRenderers = new List<GameObject>();

    void Start()
    {
        Search();
    }

    //private void Update()
    //{
    //    if (transform.hasChanged)
    //    {
    //        transform.hasChanged = false;
    //        Search();
    //    }
    //}

    void Search()
    {
        Edge[] result = ShortestPathSearch.Search(GetComponentsInChildren<Vertex>(), GetComponent<Graph>().Edges, StartIndex, EndIndex);
        ClearPath();
        DrawPath(result);
    }

    void DrawPath(Edge[] edges)
    {
        Debug.Log("DRAW");
        Material edgeMaterial = GetComponent<Graph>().EdgeMaterial;
        for (int i=0; i<edges.Length; i++)
        {
            Edge e = edges[i];
            GameObject go = new GameObject();
            go.transform.SetParent(transform, false);
            go.name = $"SPS_Edge-{e.v1.Id}-{e.v2.Id}";
            go.transform.SetAsFirstSibling();
            go.transform.position = e.v1.transform.position;

            LineRenderer edge = go.AddComponent<LineRenderer>();
            edge.material = edgeMaterial;
            edge.startColor = edge.endColor = new Color(0, 1, 0);
            edge.widthMultiplier = .5f;
            edge.sortingOrder = -1;
            edge.SetPositions(new Vector3[] {
                e.v1.transform.position,
                e.v2.transform.position
            });

            edgeRenderers.Add(go);
        }
    }

    void ClearPath()
    {
        Debug.Log("CLEAR");
        for (int i = 0; i < edgeRenderers.Count; i++)
            DestroyImmediate(edgeRenderers[i]);
        edgeRenderers = new List<GameObject>();
    }
}
