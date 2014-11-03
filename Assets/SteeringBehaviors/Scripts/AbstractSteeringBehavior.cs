using UnityEngine;
using System.Collections;

public abstract class AbstractSteeringBehavior : MonoBehaviour, ISteeringBehavior
{
    [Tooltip("When true, cancels lower priority behaviors")]
    public bool cancelLowPriority = false;

    public virtual BehaviorResult RunBehavior()
    {
        return cancelLowPriority ? BehaviorResult.CancelLowPriority : BehaviorResult.Continue;
    }
}
