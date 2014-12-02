using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public float distance = 10.0f;
    public float height = 2.5f;
    public float heightDamping = 4.0f;
    public float positionDamping = 4.0f;
    public float rotationDamping = 4.0f;

    private CameraTarget[] targets = null;
    private int targetId = 0;

    void Start()
    {
        targets = FindObjectsOfType<CameraTarget>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetId++;
            targetId = targetId%targets.Length;
        }
    }

    void LateUpdate()
    {
        if (targets.Length == 0)
            return;

        CameraTarget target = targets[targetId];

        // Early out if we don't have a target
        if (!target)
            return;

        float dt = Time.deltaTime;
        float wantedHeight = target.transform.position.y + height;
        float currentHeight = transform.position.y;

        // Damp the height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDamping * dt);

        // Set the position of the camera 
        Vector3 wantedPosition = target.transform.position - target.transform.forward * distance;
        transform.position = Vector3.Lerp(transform.position, wantedPosition, positionDamping * dt);

        // adjust the height of the camera
        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        // look at the target

        transform.forward = Vector3.Lerp(transform.forward, target.transform.position - transform.position, rotationDamping * dt);

    }
}
