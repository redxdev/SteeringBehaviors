using UnityEngine;
using System.Collections;

public interface ISteeringBehavior
{
    /// <summary>
    /// Runs the behavior.
    /// </summary>
    /// <returns>Normalized force vector. This may exceed magnitude 1 if desired.</returns>
    Vector3 RunBehavior();
}
