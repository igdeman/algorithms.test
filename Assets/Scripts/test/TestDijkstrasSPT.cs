using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDijkstrasSPT : MonoBehaviour
{

    public int StartIndex = 0;

    void Start()
    {
        Graph g = GetComponent<Graph>();
        Edge[] SPT = DijkstrasSPT.Search(GetComponentsInChildren<Vertex>(), g.Edges, StartIndex);

        LineRenderer[] existingEdges = GetComponentsInChildren<LineRenderer>();
        for (int i = 0; i < existingEdges.Length; i++)
        {
            //existingEdges[i].gameObject.SetActive(false);
            existingEdges[i].gameObject.name = "Edge-" + existingEdges[i].gameObject.name;
            existingEdges[i].startColor = existingEdges[i].endColor = new Color(
                existingEdges[i].startColor.r,
                existingEdges[i].startColor.g,
                existingEdges[i].startColor.b,
                .2f
                );
            //Destroy(existingEdges[i].gameObject);
        }

        for (int i = 0; i < SPT.Length; i++)
        {
            if (SPT[i] != null && SPT[i].v1 != null && SPT[i].v2 != null)
            {
                GameObject go = new GameObject();
                go.name = $"{SPT[i].v1.Id}-{SPT[i].v2.Id}";
                go.transform.SetParent(gameObject.transform);
                go.transform.SetAsFirstSibling();
                go.transform.position = SPT[i].v1.transform.position;

                LineRenderer edge = go.AddComponent<LineRenderer>();
                edge.material = g.EdgeMaterial;
                edge.startColor = edge.endColor = new Color(0, 1, 0);
                edge.widthMultiplier = .5f;
                edge.sortingOrder = -1;
                edge.SetPositions(new Vector3[] {
                    SPT[i].v1.transform.position,
                    SPT[i].v2.transform.position
                });
            }
        }
    }
}
