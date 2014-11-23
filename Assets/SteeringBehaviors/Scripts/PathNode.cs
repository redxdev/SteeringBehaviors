using UnityEngine;
using System.Collections;

public class PathNode : MonoBehaviour
{
    public PathNode nextNode;

    public float pathRadius = 5;

    [Tooltip("Check this if it is the end of a loop")]
    public bool endChain = false;

    void Update()
    {
        if (nextNode != null)
        {
            Debug.DrawLine(transform.position, nextNode.transform.position, Color.green);
        }
    }
}
