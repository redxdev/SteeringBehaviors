using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringManager))]
public class SeekBehavior : MonoBehaviour
{
    public Transform target = null;

    public float weight = 1.0f;

    private SteeringManager steering = null;

    private CharacterController controller = null;

    private ObstacleAvoidanceBehavior oab = null;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
        controller = GetComponent<CharacterController>();
        oab = GetComponent<ObstacleAvoidanceBehavior>();
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

        // if the target is behind us, disable the obstacle avoidance behavior
        if (oab != null && Vector3.Dot(transform.forward, target.transform.position - transform.position) < 0)
        {
            oab.enabled = false;
        }
        else if (oab != null)
        {
            oab.enabled = true;
        }
    }
}
