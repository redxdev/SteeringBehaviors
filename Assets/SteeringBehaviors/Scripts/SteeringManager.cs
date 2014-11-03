using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

class PriorityComparer : IComparer<float>
{
    public int Compare(float x, float y)
    {
        int result = y.CompareTo(x);
        if (result == 0)
            return 1;
        return result;
    }
}

public class SteeringManager : MonoBehaviour
{
    public float maxVelocity = 5f;

    private SortedList<float, ISteeringBehavior> behaviors = new SortedList<float, ISteeringBehavior>(new PriorityComparer());

    private Vector3 force = new Vector3();

    public Vector3 Force
    {
        get { return force; }
        set { force = value; }
    }

    void FixedUpdate()
    {
        foreach (ISteeringBehavior behavior in behaviors.Values)
        {
            if (behavior.RunBehavior() == BehaviorResult.CancelLowPriority)
                break;
        }

        behaviors.Clear();

        rigidbody.velocity += Force;
        Force = Vector3.zero;

        if (maxVelocity > 0)
        {
            float oldY = rigidbody.velocity.y;
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, oldY, rigidbody.velocity.z);
        }
    }

    public void AddBehaviorTick(float priority, ISteeringBehavior behavior)
    {
        if (priority < 0)
            return;

        behaviors.Add(priority, behavior);
    }
}
