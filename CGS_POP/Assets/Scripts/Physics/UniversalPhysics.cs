using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class UniversalPhysics : MonoBehaviour
{
    //Gravity Settings
    private Vector3 gravity;
    public float gravityModifier = 1f;
    //protected Vector3 gravityAcceleration;
    //protected bool grounded;
    //protected Vector3 groundNormal;

    //Components
    protected Rigidbody rb;
    protected CharacterController cc;

    //Applied Velocity
    public Vector3 velocity;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();

        SetGravity();
    }

    // Update is called once per frame
    void Update()
    {
        ApplyGravity();

    }

    void FixedUpdate()
    {

    }


    /// <summary>
    /// Apply Gravity to the object every frame, when on ground
    /// </summary>
    void ApplyGravity()
    {
        velocity = (gravity * rb.mass) * Time.deltaTime;

        cc.Move(velocity);
    }


    /// <summary>
    /// Sets the gravity times the gravityModifier
    /// </summary>
    void SetGravity()
    {
        gravity = Physics.gravity * gravityModifier;
    }
}