// MyCharacter.cs - A simple example showing how to get input from Rewired.Player

using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class GamePlayer : MonoBehaviour
{

    // The Rewired player id of this character
    public int playerId = 0;

    // The movement speed of this character
    public float moveSpeed = 3.0f;

    // Jump Acceleration
    public float jumpSpeed = 1f;
    protected float jumping;

    private Player player; // The Rewired Player
    private CharacterController cc;
    private Rigidbody rb;

    //Input Listeners
    private Vector3 moveVector;
    private bool jump;
    private bool blue;
    private bool green;
    private bool red;
    private bool yellow;

    //Collider for detecting shapes at a distance
    private Collider[] areaOfInfluence;
    public float areaOfInfluenceRadius = 3f;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        cc = GetComponent<CharacterController>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        ProcessInput();
        FindObjectsInRange();
    }

    private void FindObjectsInRange()
    {
        //areaOfInfluence = Physics.OverlapSphere(transform.position, areaOfInfluenceRadius);
        areaOfInfluence = RotaryHeart.Lib.PhysicsExtension.Physics.OverlapSphere(transform.position,
            areaOfInfluenceRadius, -1, RotaryHeart.Lib.PhysicsExtension.Physics.PreviewCondition.Editor);
    }

    /// <summary>
    /// Gets the inputs from Rewired
    /// </summary>
    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("MoveHorizontal"); // get input by name or action id
        jump = player.GetButtonDown("Jump");
        blue = player.GetButton("Blue");
        green = player.GetButton("Green");
        red = player.GetButton("Red");
        yellow = player.GetButton("Yellow");
    }

    /// <summary>
    /// Processes inputs into actions
    /// </summary>
    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            cc.SimpleMove(moveVector * moveSpeed);
        }

        // Process actions
        if (jump)
        {
            if (cc.isGrounded)
            {
                StartCoroutine("Jump");
            }
        }

        if (blue || green || red || yellow)
        {
            DoAbility();
        }
    }

    /// <summary>
    /// Will make the player Jump
    /// </summary>
    IEnumerator Jump()
    {
        jumping = jumpSpeed;
        while (jumping > 0.3f)
        {
            jumping -= jumpSpeed * Time.deltaTime;
            cc.Move(new Vector3(0f, jumping, 0f));
            yield return null;
        }
    }

    /// <summary>
    /// Does the ability assigned to that player
    /// </summary>
    void DoAbility()
    {
        switch (playerId)
        {
            case 0:
                AbilityGrow();
                break;
            case 1:
                AbilityPull();
                break;
            case 2:
                AbilityPull();
                break;
            case 3:
                AbilitySwitch();
                break;
        }
    }

    /// <summary>
    /// The ability to grow shapes
    /// </summary>
    void AbilityGrow()
    {

    }

    /// <summary>
    /// The ability to pull the shapes toward the player
    /// </summary>
    void AbilityPull ()
    {

    }

    /// <summary>
    /// The ability to switch the shape with another one
    /// </summary>
    void AbilitySwitch()
    {

    }

    /// <summary>
    /// The ability to float shapes
    /// </summary>
    void AbilityFloat()
    {

    }
}