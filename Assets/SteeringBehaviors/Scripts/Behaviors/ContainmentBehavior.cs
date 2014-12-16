using System;
using UnityEngine;
using System.Collections;

public class ContainmentBehavior : MonoBehaviour {
    public PathNode firstNode;
    public float lookAhead = 5;
    public float weight = 1;

    private SteeringManager steering;
    private CharacterController controller;

    void Start()
    {
        steering = GetComponent<SteeringManager>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (firstNode == null || firstNode.nextNode == null)
            return;

        Vector3 predict = controller.velocity;
        predict = predict.normalized * lookAhead;
        Vector3 predictLoc = transform.position + predict;

        PathNode currentNode = firstNode;
        while (currentNode != null && currentNode.nextNode != null && currentNode.endChain == false)
        {
            Vector2 a = new Vector2(currentNode.transform.position.x, currentNode.transform.position.z);
            Vector2 b = new Vector2(currentNode.nextNode.transform.position.x, currentNode.nextNode.transform.position.z);

            Vector3 adjustedPredictLoc = predictLoc - predict.normalized*currentNode.pathRadius;

            float mySide = SideOfLine(a, b, new Vector2(transform.position.x, transform.position.z));
            float futureSide = SideOfLine(a, b, new Vector2(predictLoc.x, adjustedPredictLoc.z));

            if(mySide != 1)
                Debug.Log(mySide);

            if (Math.Abs(mySide - futureSide) > Mathf.Epsilon)
            {
                // going to run into the boundary, let's change direction
                float dx = b.x - a.x;
                float dy = b.y - a.y;
                Vector3 pathNormal;
                if (mySide > 0)
                {
                    pathNormal = new Vector3(-dy, 0, dx);
                }
                else
                {
                    pathNormal = new Vector3(dy, 0, -dx);
                }

                /*float dot = Vector3.Dot(transform.right, pathNormal);
                Vector3 dv = transform.right*dot;
                if (dot < 0)
                {
                    dv = -transform.right;
                }
                else
                {
                    dv = transform.right;
                }*/

                Vector3 dv = predict - ((Vector3.Dot(2*predict, pathNormal))/(pathNormal.sqrMagnitude))*pathNormal;

                Debug.DrawRay(transform.position, dv, Color.magenta);

                dv = dv.normalized*steering.maxSpeed;
                dv -= controller.velocity;
                dv.y = 0;
                steering.AddForce(weight, dv);
                return;
            }

            currentNode = currentNode.nextNode;
        }
    }

    private float SideOfLine(Vector2 l1, Vector2 l2, Vector2 p)
    {
        float a = -(l2.y - l1.y);
        float b = l2.x - l1.x;
        float c = -(a*l1.x + b*l1.y);
        float d = a*p.x + b*p.y + c;
        return Mathf.Sign(d);
    }
}
