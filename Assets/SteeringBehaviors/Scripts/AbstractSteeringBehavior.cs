using UnityEngine;
using System.Collections;

public abstract class AbstractSteeringBehavior : MonoBehaviour, ISteeringBehavior
{
    public abstract Vector3 RunBehavior();
}
