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
    public float velocityScale = 1f;

    public float maxVelocity = -1;

    private Vector3 velocity = new Vector3();

    private SortedList<float, ISteeringBehavior> behaviors = new SortedList<float, ISteeringBehavior>(new PriorityComparer());

    public Vector3 Velocity
    {
        get { return velocity; }
        set { velocity = value; }
    }

    void FixedUpdate()
    {
        foreach (ISteeringBehavior behavior in behaviors.Values)
        {
            if (behavior.RunBehavior() == BehaviorResult.CancelLowPriority)
                break;
        }

        behaviors.Clear();

        rigidbody.velocity += Velocity;
        Velocity = Vector3.zero;

        if (maxVelocity > 0)
        {
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, maxVelocity);
        }
    }

    public void AddBehaviorTick(float priority, ISteeringBehavior behavior)
    {
        behaviors.Add(priority, behavior);
    }
}
