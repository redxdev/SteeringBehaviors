using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class SeekBehavior : AbstractSteeringBehavior
{
    public Transform target = null;

    public float weight = 1;

    public float speed = 1;

    private SteeringManager steering = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        steering.AddBehaviorTick(weight, this);
    }

    public override Vector3 RunBehavior()
    {
        Vector3 dv = target.position - transform.position;
        dv = dv.normalized * steering.maxForce * speed;
        dv -= rigidbody.velocity;
        dv.y = 0;

        return dv;
    }
}
