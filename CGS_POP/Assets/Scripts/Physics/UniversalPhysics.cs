using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class UniversalPhysics : MonoBehaviour
{
    public float gravityModifier = 1f;

    protected bool grounded;
    protected Vector3 groundNormal;
    protected Rigidbody rb;
    protected CharacterController cc;
    public Vector3 velocity;
    protected RaycastHit[] hitBuffer = new RaycastHit[20];

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    protected float gravityAcceleration = 0f;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (!cc.isGrounded)
        {
            ApplyGravity();
        }
        else
        {
            gravityAcceleration = 0f;
        }
    }

    void ApplyGravity()
    {
        velocity = Vector3.zero;
        if (gravityAcceleration < rb.mass)
        {
            gravityAcceleration += rb.mass * Time.deltaTime;
        }
        velocity = (Physics.gravity * gravityAcceleration) * Time.deltaTime;
        cc.Move(velocity);
    }
}
