using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class SeekBehavior : MonoBehaviour
{
    public Transform target = null;

    public float weight = 1.0f;

    private SteeringManager steering = null;

    private CharacterController controller = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (target == null)
            return;

        Vector3 dv = target.position - transform.position;
        dv = dv.normalized*steering.maxSpeed;
        dv -= controller.velocity;
        dv.y = 0;

        steering.AddForce(weight, dv);
    }
}
