using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SteeringManager))]
public class SeparationBehavior : MonoBehaviour
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
        Vector3 separationForce = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborhood,
            searchLayer.value);

        if (hitColliders.Length == 0)
            return;

        // separation
        foreach (var c in hitColliders)
        {
            if (c == this.collider)
                continue;

            Vector3 force = transform.position - c.transform.position;
            force /= Vector3.Distance(c.transform.position, transform.position);
            separationForce += force;
        }

        steering.AddForce(weight, separationForce);
    }
}