using UnityEngine;
using System.Collections;

public enum BehaviorResult
{
    CancelLowPriority,
    Continue
}
public interface ISteeringBehavior
{
    BehaviorResult RunBehavior();
}
