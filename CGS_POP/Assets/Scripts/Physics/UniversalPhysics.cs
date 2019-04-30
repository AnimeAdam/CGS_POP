using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class UniversalPhysics : MonoBehaviour
{
    //Gravity Settings
    public Vector3 gravity;
    public float gravityModifier = 1f;

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

    /// <summary>
    /// Apply Gravity to the object every frame, when on ground
    /// </summary>
    void ApplyGravity()
    {
        if (!cc.isGrounded)
        {
            velocity = (gravity * rb.mass) * Time.deltaTime;
        }

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