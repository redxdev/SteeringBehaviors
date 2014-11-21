using UnityEngine;
using System.Collections;


[RequireComponent(typeof(SteeringManager))]
public class AlignmentBehavior : MonoBehaviour
{
    public float neighborhood = 30f;
    public LayerMask searchLayer;

    public float weight = 1f;

    private SteeringManager steering = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
    }

    void Update()
    {
        Vector3 alignmentForce = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, neighborhood,
            searchLayer.value);

        if (hitColliders.Length == 0)
            return;

        // alignment
        foreach (var c in hitColliders)
        {
            if (c == this.collider)
                continue;

            alignmentForce += c.transform.forward;
        }

        alignmentForce /= hitColliders.Length;

        steering.AddForce(weight, alignmentForce);
    }
}