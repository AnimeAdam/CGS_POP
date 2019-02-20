using System.Collections;
using System.Collections.Generic;
using Rewired;
using UnityEngine;

public class UniversalPhysics : MonoBehaviour
{
    private Vector3 gravity;
    public float gravityModifier = 1f;
    protected Vector3 gravityAcceleration;

    [SerializeField]protected bool grounded;
    protected Vector3 groundNormal;
    protected Rigidbody rb;
    public Collider col;
    protected CharacterController cc;
    public Vector3 velocity;
    protected RaycastHit[] hitBuffer = new RaycastHit[20];

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cc = GetComponent<CharacterController>();
        col = GetComponent<Collider>();

        SetGravity();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = cc.isGrounded;
    }

    void FixedUpdate()
    {
        ApplyGravity();
    }

    void ApplyGravity()
    {
        velocity = (gravity * rb.mass) * Time.deltaTime;

        cc.Move(velocity);
    }

    void SetGravity()
    {
        gravity = Physics.gravity * gravityModifier;
    }
}
