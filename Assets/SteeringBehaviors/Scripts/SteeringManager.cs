using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class SteeringManager : MonoBehaviour
{
    public float speed = 10f;

    private Vector3 force = new Vector3();

    private SortedDictionary<float, ISteeringBehavior> behaviors = new SortedDictionary<float, ISteeringBehavior>();

    public Vector3 Force
    {
        get { return force; }
        set { force = value; }
    }

    void FixedUpdate()
    {
        foreach (ISteeringBehavior behavior in behaviors.Values)
        {
            if (!behavior.RunBehavior())
                break;
        }

        rigidbody.AddForce(force);
        behaviors.Clear();
    }

    public void AddBehaviorTick(float priority, ISteeringBehavior behavior)
    {
        behaviors.Add(priority, behavior);
    }
}
