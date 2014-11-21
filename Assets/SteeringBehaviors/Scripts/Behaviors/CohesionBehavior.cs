using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SteeringManager))]
public class CohesionBehavior : MonoBehaviour
{
    public float neighborhood = 30f;
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
        Vector3 cohesionForce = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborhood,
            searchLayer.value);

        if (hitColliders.Length == 0)
            return;

        // cohesion
        foreach (var c in hitColliders)
        {
            if (c == this.collider)
                continue;

            cohesionForce += c.transform.position;
        }

        cohesionForce /= hitColliders.Length;
        cohesionForce = cohesionForce - transform.position;
        cohesionForce = cohesionForce.normalized*steering.maxSpeed - controller.velocity;

        steering.AddForce(weight, cohesionForce);
    }
}