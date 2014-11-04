using UnityEngine;
using System.Collections;

public abstract class AbstractSteeringBehavior : MonoBehaviour, ISteeringBehavior
{
    public virtual Vector3 RunBehavior()
    {
        return Vector3.zero;
    }
}
