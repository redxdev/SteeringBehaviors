using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class PathFollowingBehavior : MonoBehaviour
{
    public PathNode firstNode;
    public float lookAhead = 5;
    public float weight = 1;

    private SteeringManager steering;
    private CharacterController controller;

    private PathNode currentNode = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
        controller = GetComponent<CharacterController>();
        currentNode = firstNode;
    }

    void Update()
    {
        if (firstNode == null || firstNode.nextNode == null || currentNode == null || currentNode.nextNode == null)
            return;

        Vector3 predict = controller.velocity;
        predict = predict.normalized * lookAhead;
        Vector3 predictLoc = transform.position + predict;

        PathNode targetNode = null;
        Vector3 targetNormal = transform.position;
        float targetDistance = 0;

        Vector3 currentStart = currentNode.transform.position;
        Vector3 currentEnd = currentNode.nextNode.transform.position;
        Vector3 currentNormal = GetNormalPoint(predictLoc, currentStart, currentEnd);
        float currentDist = Vector3.Distance(predictLoc, currentNormal);

        Debug.DrawLine(predictLoc, currentNormal, Color.red);

        if (currentNode.nextNode.nextNode != null)
        {
            Vector3 nextStart = currentEnd;
            Vector3 nextEnd = currentNode.nextNode.nextNode.transform.position;
            Vector3 nextNormal = GetNormalPoint(predictLoc, nextStart, nextEnd);
            float nextDist = Vector3.Distance(predictLoc, nextNormal);

            Debug.DrawLine(predictLoc, nextNormal, Color.yellow);

            if (currentDist < nextDist)
            {
                targetNode = currentNode;
                targetNormal = currentNormal;
                targetDistance = currentDist;
            }
            else
            {
                targetNode = currentNode.nextNode;
                targetNormal = nextNormal;
                targetDistance = nextDist;
            }
        }
        else
        {
            targetNode = currentNode;
            targetNormal = currentNormal;
            targetDistance = currentDist;
        }

        if (targetNode == null)
            return;

        currentNode = targetNode;

        if (targetDistance > currentNode.pathRadius)
        {
            Vector3 dv = targetNormal - transform.position;
            dv = dv.normalized * steering.maxSpeed;
            dv -= controller.velocity;
            dv.y = 0;

            steering.AddForce(weight, dv);
        }
    }

    private Vector3 GetNormalPoint(Vector3 p, Vector3 start, Vector3 end)
    {
        Vector3 a = p - start;
        Vector3 b = end - start;

        b.Normalize();
        b *= Vector3.Dot(a, b);

        Vector3 normal = start + b;

        float startToEndDist = Vector3.Distance(start, end);

        if (Vector3.Distance(start, normal) > startToEndDist)
            normal = end;
        else if (Vector3.Distance(end, normal) > startToEndDist)
            normal = start;

        return normal;
    }
}
