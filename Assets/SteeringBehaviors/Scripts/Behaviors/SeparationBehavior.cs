using System.Collections.Generic;
using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SteeringManager))]
public class SeparationBehavior : MonoBehaviour
{
    public float closeRadius = 10f;
    public LayerMask searchLayer;

    public float weight = 1f;

    private SteeringManager steering = null;
    private CharacterController controller = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 separationForce = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, closeRadius,
            searchLayer.value);

        if (hitColliders.Length <= 1)
            return;

        // separation
        foreach (var c in hitColliders)
        {
            if (c == this.collider)
                continue;

            float distance = Vector3.Distance(transform.position, c.transform.position);
            separationForce += (transform.position - c.transform.position);
        }

        separationForce = separationForce.normalized*steering.maxSpeed - controller.velocity;

        steering.AddForce(weight, separationForce);
    }
}