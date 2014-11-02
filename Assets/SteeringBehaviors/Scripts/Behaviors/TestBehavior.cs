using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class TestBehavior : MonoBehaviour, ISteeringBehavior
{
    public string axisName = "Vertical";

    public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);

    public float priority = 1.0f;

    public bool cancelLowerPriority = false;

    private SteeringManager steering = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }

    void FixedUpdate()
    {
        steering.AddBehaviorTick(priority, this);
    }

    public BehaviorResult RunBehavior()
    {
        float axis = Input.GetAxis(axisName);
        Vector3 f = direction * axis;

        if (f.magnitude == 0)
            return BehaviorResult.Continue;

        steering.Velocity += f;
        return cancelLowerPriority ? BehaviorResult.CancelLowPriority : BehaviorResult.Continue;
    }
}
