using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SteeringManager))]
public class FlockingBehavior : MonoBehaviour
{
    public float neighborhood = 30f;
    public LayerMask searchLayer;

    public float weight = 1f;

    public float separationWeight = 1f;
    public float alignmentWeight = 1f;
    public float cohesionWeight = 1f;

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
        Vector3 cohesionForce = Vector3.zero;
        Vector3 alignmentForce = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborhood,
            searchLayer.value);

        // alignment
        foreach (Collider collider in hitColliders)
        {
            if (collider == this.collider)
                continue;

            Debug.DrawLine(collider.transform.position, transform.position, Color.blue);

            alignmentForce += collider.transform.forward;
        }

        alignmentForce = alignmentForce.normalized*steering.maxSpeed - controller.velocity;


        // cohesion
        foreach (Collider collider in hitColliders)
        {
            if (collider == this.collider)
                continue;

            cohesionForce += collider.transform.position;
        }

        cohesionForce /= hitColliders.Length;
        cohesionForce = (cohesionForce - transform.position).normalized*steering.maxSpeed;
        
        // separation
        foreach (Collider collider in hitColliders)
        {
            separationForce += (transform.position - collider.transform.position).normalized / Vector3.Distance(collider.transform.position, transform.position);
        }

        Vector3 calcForce = alignmentForce*alignmentWeight + cohesionForce*cohesionWeight;

        steering.AddForce(weight, calcForce.normalized * steering.maxSpeed);
    }
}