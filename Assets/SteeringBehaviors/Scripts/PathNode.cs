using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class PathNode : MonoBehaviour
{
    public PathNode nextNode;

    public float pathRadius = 5;

    public bool endChain = false;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.SetVertexCount(2);
    }

    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);

        if (nextNode != null)
        {
            Debug.DrawLine(transform.position, nextNode.transform.position, Color.green);
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, nextNode.transform.position);
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
