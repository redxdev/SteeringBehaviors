using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class TestBehavior : AbstractSteeringBehavior
{
    public string axisName = "Vertical";

    public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f);

    public float weight = 1f;

    private SteeringManager steering = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }

    void FixedUpdate()
    {
        steering.AddBehaviorTick(weight, this);
    }

    public override Vector3 RunBehavior()
    {
        float axis = Input.GetAxis(axisName);
        return direction * axis * steering.maxForce;
    }
}
