using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class SeekBehavior : AbstractSteeringBehavior
{
    public Transform target = null;

    [Tooltip("Weight curve, based on distance")]
    public AnimationCurve weight = AnimationCurve.Linear(0f, 1f, 100f, 1f);

    [Tooltip("Speed curve, based on distance")]
    public AnimationCurve speed = AnimationCurve.Linear(0f, 1f, 10f, 1f);

    private SteeringManager steering = null;

    private float calcDistance = 0f;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        calcDistance = Vector3.Distance(transform.position, target.position);

        steering.AddBehaviorTick(weight.Evaluate(calcDistance), this);
    }

    public override Vector3 RunBehavior()
    {
        Vector3 dv = target.position - transform.position;
        dv = dv.normalized * steering.maxForce * speed.Evaluate(calcDistance);
        dv -= rigidbody.velocity;
        dv.y = 0;

        return dv;
    }
}
