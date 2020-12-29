using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Graph : MonoBehaviour
{
    public Color EdgeColor;
    public Material EdgeMaterial;
    public Edge[] Edges;

    Vertex[] vertices;
    Material lastMaterial;
    Color lastColor;
    List<GameObject> edgeGo = new List<GameObject>();

    private void Awake()
    {
        if (Application.isPlaying)
        {
            LineRenderer[] edges = GetComponentsInChildren<LineRenderer>();
            for (int i = 0; i < edges.Length; i++)
            {
                DestroyImmediate(edges[i].gameObject);
            }
        }
    }

    void Update()
    {
        //if(!Application.isPlaying)
        //{
            vertices = GetComponentsInChildren<Vertex>();

            bool update = false;
            if (transform.hasChanged)
            {
                transform.hasChanged = false;
                update = true;
            }
            if (lastColor != EdgeColor)
            {
                update = true;
            }
            else if (lastMaterial != EdgeMaterial)
            {
                update = true;
            }
            else
            {
                for (int i = 0; i < vertices.Length; i++)
                {
                    Vertex v = vertices[i];
                    if (v.transform.hasChanged)
                    {
                        update = true;
                        v.transform.hasChanged = false;
                    }
                }
            }

            lastColor = EdgeColor;
            lastMaterial = EdgeMaterial;

            if (update)
            {
                ClearEdges();
                DrawEdges();
                UpdateVertices();
            }
        //}
    }

    void UpdateVertices()
    {
        if (!Application.isPlaying)
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vertex v = vertices[i];
                v.TextField.text = v.Id.ToString();
            }
        }
    }

    void DrawEdges()
    {
        if (Edges != null)
        {
            for (int i = 0; i < Edges.Length; i++)
            {
                if (Edges[i] != null && Edges[i].v1 != null && Edges[i].v2 != null)
                {
                    GameObject go = new GameObject();
                    go.name = $"{Edges[i].v1.Id}-{Edges[i].v2.Id}";
                    go.transform.SetParent(gameObject.transform);
                    go.transform.SetAsFirstSibling();
                    go.transform.position = Edges[i].v1.transform.position;

                    LineRenderer edge = go.AddComponent<LineRenderer>();
                    edge.material = lastMaterial;
                    edge.startColor = edge.endColor = lastColor;
                    edge.widthMultiplier = .2f;
                    edge.SetPositions(new Vector3[] {
                        Edges[i].v1.transform.position,
                        Edges[i].v2.transform.position
                    });

                    edgeGo.Add(go);
                }
            }
        }
    }

    void ClearEdges()
    {
        for (int i = 0; i < edgeGo.Count; i++)
            DestroyImmediate(edgeGo[i]);
        edgeGo.Clear();
        if (!Application.isPlaying)
        {
            LineRenderer[] edges = GetComponentsInChildren<LineRenderer>();
            for (int i = 0; i < edges.Length; i++)
            {
                DestroyImmediate(edges[i].gameObject);
            }
        }
    }
}
