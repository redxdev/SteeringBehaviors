using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class FlockingBehavior : AbstractSteeringBehavior
{
    public float neighborhood = 30f;
    public LayerMask searchLayer;

    public float weight = 1f;
    public float speed = 1f;

    public float separationWeight = 1f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;

    private SteeringManager steering = null;
    private Vector3 calcForce = Vector3.zero;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }

    void LateUpdate()
    {
        calcForce = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborhood,
            searchLayer.value);

        foreach (Collider collider in hitColliders)
        {
            Debug.DrawLine(collider.transform.position, transform.position, Color.blue);

            Vector3 force = Vector3.zero;

            force += Separate(collider.gameObject)*separationWeight;
            force += Cohesion(collider.gameObject, hitColliders.Length)*cohesionWeight;
            force += Alignment(collider.gameObject, hitColliders.Length)*alignmentWeight;

            calcForce += force.normalized*steering.maxForce*speed;
        }

        if(calcForce.magnitude > 0)
            steering.AddBehaviorTick(weight, this);
    }

    private Vector3 Separate(GameObject obj)
    {
        Vector3 direction = transform.position - obj.transform.position;
        return direction.normalized;
    }

    private Vector3 Cohesion(GameObject obj, int count)
    {
        Vector3 v = obj.transform.position;
        v /= count;
        v = v - transform.position;
        return v.normalized;
    }

    private Vector3 Alignment(GameObject obj, int count)
    {
        Vector3 v = obj.rigidbody.velocity;
        v /= count;
        return v.normalized;
    }

    public override Vector3 RunBehavior()
    {
        return calcForce;
    }
}
