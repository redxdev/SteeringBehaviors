using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SteeringManager : MonoBehaviour
{
    private struct BehaviorTick
    {
        public float Weight { get; set; }
        public ISteeringBehavior Behavior { get; set; }
    }

    public float maxForce = 15f;

    private LinkedList<BehaviorTick> behaviors = new LinkedList<BehaviorTick>();

    void FixedUpdate()
    {
        float totalWeight = 0;
        foreach(BehaviorTick tick in behaviors)
        {
            totalWeight += tick.Weight;
        }

        Vector3 appliedForce = Vector3.zero;

        foreach (BehaviorTick tick in behaviors)
        {
            appliedForce += tick.Behavior.RunBehavior() * (tick.Weight / totalWeight);
        }

        behaviors.Clear();

        Debug.Log("Applied force for " + gameObject.name + ": " + appliedForce);

        rigidbody.AddForce(appliedForce);
    }

    public void AddBehaviorTick(float weight, ISteeringBehavior behavior)
    {
        if (weight <= 0)
            return;

        behaviors.AddLast(new BehaviorTick() { Weight = weight, Behavior = behavior });
    }
}
