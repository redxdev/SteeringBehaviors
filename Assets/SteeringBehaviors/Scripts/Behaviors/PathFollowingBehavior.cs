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

    void Start()
    {
        steering = GetComponent<SteeringManager>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (firstNode == null || firstNode.nextNode == null)
            return;

        Vector3 predict = controller.velocity;
        predict = predict.normalized * lookAhead;
        Vector3 predictLoc = transform.position + predict;

        PathNode currentNode = firstNode;
        PathNode target = null;
        Vector3 targetNormal = Vector3.zero;
        float targetDistance = float.MaxValue;
        while (currentNode != null && currentNode.nextNode != null && !currentNode.endChain)
        {
            Vector3 start = currentNode.transform.position;
            Vector3 end = currentNode.nextNode.transform.position;

            Vector3 normalPoint = GetNormalPoint(predictLoc, start, end);
            if (normalPoint.x < start.x || normalPoint.x > end.x)
            {
                normalPoint = end;
            }

            float distance = Vector3.Distance(transform.position, normalPoint);
            if (target == null || distance < targetDistance)
            {
                target = currentNode;
                targetNormal = normalPoint;
                targetDistance = distance;
            }

            currentNode = currentNode.nextNode;
        }

        if (targetDistance > target.pathRadius)
        {
            Vector3 dv = targetNormal - transform.position;
            dv = dv.normalized * steering.maxSpeed;
            dv -= controller.velocity;
            dv.y = 0;

            steering.AddForce(weight, dv);
        }
    }

    private Vector3 GetNormalPoint(Vector3 local, Vector3 start, Vector3 end)
    {
        Vector3 a = local - start;
        Vector3 b = end - start;

        b.Normalize();
        b *= Vector3.Dot(a, b);
        return end + b;
    }
}
