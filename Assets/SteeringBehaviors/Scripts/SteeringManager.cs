using System;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class SteeringManager : MonoBehaviour
{
    public struct BehaviorForce
    {
        public float Weight { get; set; }
        public Vector3 Force { get; set; }
    }

    public float maxSpeed = 12f;

    public float maxForce = 12f;

    public float agentRadius = 1.0f;

    private LinkedList<BehaviorForce> forces = new LinkedList<BehaviorForce>();

    private CharacterController controller = null;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float totalWeight = 0;
        foreach(BehaviorForce force in forces)
        {
            totalWeight += force.Weight;
        }

        Vector3 appliedForce = Vector3.zero;

        foreach (BehaviorForce force in forces)
        {
            Vector3 apply = force.Force;
            apply *= force.Weight/totalWeight;
            appliedForce += apply;
        }

        appliedForce = Vector3.ClampMagnitude(appliedForce, maxForce);

        velocity += appliedForce*Time.deltaTime;
        velocity.y = 0;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        if (velocity != Vector3.zero)
            transform.forward = velocity.normalized;

        velocity.y = -20.0f*Time.deltaTime;

        controller.Move(velocity*Time.deltaTime);

        forces.Clear();

        Debug.DrawRay(transform.position, transform.forward * 5);
    }

    public void AddForce(float weight, Vector3 force)
    {
        forces.AddLast(new BehaviorForce() {Weight = weight, Force = force});
    }
}
