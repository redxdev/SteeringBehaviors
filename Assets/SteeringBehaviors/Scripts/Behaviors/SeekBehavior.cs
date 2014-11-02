using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class SeekBehavior : MonoBehaviour, ISteeringBehavior
{
    public Transform target = null;

    public float priority = 1.0f;

    public bool cancelLowerPriority = false;

    private SteeringManager steering = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }
    void FixedUpdate()
    {
        if (target == null)
            return;

        steering.AddBehaviorTick(priority, this);
    }

    public BehaviorResult RunBehavior()
    {
        Vector3 dv = target.position - transform.position;
        dv = dv.normalized*steering.maxVelocity;
        dv -= rigidbody.velocity;
        dv.y = 0;
        steering.Velocity += dv;

        return cancelLowerPriority ? BehaviorResult.CancelLowPriority : BehaviorResult.Continue;
    }
}
