using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class ObstacleAvoidanceBehavior : AbstractSteeringBehavior
{

    public float weight = 1.0f;
    public float speed = 1.0f;

    public float obstacleDistance = 10f;

    public LayerMask obstacleLayerMask;

    private SteeringManager steering = null;

    private Vector3 calcForce = Vector3.zero;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }

    void FixedUpdate()
    {
        calcForce = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, obstacleDistance,
            obstacleLayerMask.value);

        foreach (Collider collider in hitColliders)
        {
            Vector3 hit = collider.ClosestPointOnBounds(transform.position);
            Debug.Log(hit);
            Vector3 localHit = this.collider.ClosestPointOnBounds(hit);
            calcForce += AvoidObstacle(collider.gameObject, hit, localHit)*1/Vector3.Distance(localHit, hit);
            Debug.DrawLine(collider.transform.position, transform.position, Color.blue);
        }

        Debug.DrawRay(transform.position, calcForce, Color.red);

        if(calcForce.magnitude > 0)
            steering.AddBehaviorTick(weight, this);
    }

    private Vector3 AvoidObstacle(GameObject obj, Vector3 hit, Vector3 localHit)
    {
        if (Vector3.Distance(hit, localHit) > obstacleDistance)
            return Vector3.zero;

        Vector3 vecToHit = hit - transform.position;
        if (Vector3.Dot(vecToHit, transform.forward) < 0)
            return Vector3.zero;

        float rightDotVTC = Vector3.Dot(vecToHit, transform.right);

        float myRadius = Vector3.Distance(transform.position, localHit);
        float otherRadius = Vector3.Distance(obj.transform.position, hit);

        if (Mathf.Abs(rightDotVTC) > myRadius + otherRadius)
            return Vector3.zero;

        if (rightDotVTC > 0)
            return transform.right*-steering.maxForce*obstacleDistance/vecToHit.magnitude;
        else
            return transform.right*steering.maxForce*obstacleDistance/vecToHit.magnitude;
    }

    public override Vector3 RunBehavior()
    {
        return calcForce;
    }
}
