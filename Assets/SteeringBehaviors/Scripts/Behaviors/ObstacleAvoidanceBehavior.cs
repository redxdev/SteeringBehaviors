using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class ObstacleAvoidanceBehavior : MonoBehaviour
{

    public float weight = 1.0f;

    public float obstacleDistance = 10f;

    private SteeringManager steering = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }

    void FixedUpdate()
    {
        Vector3 calcForce = Vector3.zero;

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (Obstacle obstacle in obstacles)
        {
            if (!obstacle.enabled)
                continue;

            calcForce += AvoidObstacle(obstacle);
        }

        if (calcForce.magnitude > 0)
            steering.AddForce(weight, calcForce);
    }

    private Vector3 AvoidObstacle(Obstacle obst)
    {
        float obRadius = obst.radius;

        Vector3 vecToCenter = obst.transform.position - transform.position;
        vecToCenter.y = 0;
        float dist = vecToCenter.magnitude;

        if (dist > obstacleDistance + obRadius + steering.agentRadius)
            return Vector3.zero;

        if (Vector3.Dot(transform.forward, vecToCenter) < 0)
            return Vector3.zero;

        float rightDotVTC = Vector3.Dot(vecToCenter, transform.right);

        if (Mathf.Abs(rightDotVTC) > steering.agentRadius + obRadius)
            return Vector3.zero;

        Debug.DrawLine(transform.position, obst.transform.position, Color.red);

        if (rightDotVTC > 0)
            return transform.right*-steering.maxSpeed*obstacleDistance/dist;
        else
            return transform.right*steering.maxSpeed*obstacleDistance/dist;
    }
}