using UnityEngine;
using System.Collections;

public class TestBehavior : MonoBehaviour
{
    public float weight = 1f;

    private SteeringManager steering;
    private CharacterController controller;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 dv = transform.forward.normalized * steering.maxSpeed;
        dv -= controller.velocity;
        dv.y = 0;
        steering.AddForce(weight, dv);
        return;
    }
}
