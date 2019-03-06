// MyCharacter.cs - A simple example showing how to get input from Rewired.Player

using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class GamePlayer : MonoBehaviour
{
	// Audio Management
	AudioManager audiMan;

    // The Rewired player id of this character
    public int playerId = 0;

    // The movement speed of this character
    public float moveSpeed = 3.0f;

    // Jump Stats
    public float jumpHeight = 0.6f;
    public float jumpSpeed = 0.1f;
    [SerializeField]protected float jumping;
    private bool jumpState = false;
    
    //Input Listeners
    private Vector3 moveVector;
    private bool jump;
    private bool blue;
    private bool green;
    private bool red;
    private bool yellow;
    private bool blueLift;
    private bool greenLift;
    private bool redLift;
    private bool yellowLift;

    //Collider for detecting shapes at a distance
    private Collider[] areaOfInfluence;
    public float areaOfInfluenceRadius = 3f;

    //Global senders for abilities
    [HideInInspector] public bool P1T = false;
    [HideInInspector] public bool P2T = false;
    [HideInInspector] public bool P3T = false;
    [HideInInspector] public bool P4T = false;
    [HideInInspector] public bool colourRed = false;
    [HideInInspector] public bool colourGreen = false;
    [HideInInspector] public bool colourYellow = false;
    [HideInInspector] public bool colourBlue = false;

	//Bools for audio purposes
	bool P1WasPressed = false;
	bool P2WasPressed = false;
	bool P3WasPressed = false;
	bool P4WasPressed = false;
	bool playerMoving = false;

    //Classes
	private UniversalPhysics uPhysics;
    private Player player; // The Rewired Player
    private CharacterController cc;
    private Rigidbody rb;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        uPhysics = GetComponent<UniversalPhysics>();
		audiMan = FindObjectOfType<AudioManager>();
	}

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    /// <summary>
    /// Finding objects in range for the pull Ability
    /// </summary>
    private void FindObjectsInRange()
    {
        //areaOfInfluence = Physics.OverlapSphere(transform.position, areaOfInfluenceRadius);
        areaOfInfluence = RotaryHeart.Lib.PhysicsExtension.Physics.OverlapSphere(transform.position,
            areaOfInfluenceRadius, -1, RotaryHeart.Lib.PhysicsExtension.Physics.PreviewCondition.Editor);
        GameObject orb = (GameObject)Resources.Load("SphereOfInfluence");
        orb.transform.localScale = new Vector3(areaOfInfluenceRadius*2, areaOfInfluenceRadius*2, areaOfInfluenceRadius*2);

        Instantiate(orb, transform.position,Quaternion.identity); //REMEMBER as GameObject
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

        if (playerId == 2)
        {
            P3T = false;
            blue = player.GetButtonDown("Blue");
            green = player.GetButtonDown("Green");
            red = player.GetButtonDown("Red");
            yellow = player.GetButtonDown("Yellow");
        }
        else
        {
            blue = player.GetButton("Blue");
            green = player.GetButton("Green");
            red = player.GetButton("Red");
            yellow = player.GetButton("Yellow");
        }

        blueLift = player.GetButtonUp("Blue");
        greenLift = player.GetButtonUp("Green");
        redLift = player.GetButtonUp("Red");
        yellowLift = player.GetButtonUp("Yellow");
    }

    /// <summary>
    /// Processes inputs into actions
    /// </summary>
    private void ProcessInput()
    {
		// Process movement
		if (moveVector.x != 0.0f || moveVector.y != 0.0f)
		{
			moveVector *= moveSpeed;
			playerMoving = true;
		}
		else playerMoving = false;

		// Audio for walking
		if (playerMoving && cc.isGrounded) {
			StartCoroutine(PlaySteps());
		}

        // Process actions
        if (jump)
        {
			if (jumping == 0f)
            {
				audiMan.Jump.Play();
				jumping = jumpHeight;
                jumpState = true;
            }
        }
        if (jumpState)
        {
			Jump();
        }

        if(blue || green || red || yellow)
        {
            DoAbility();
        }

        if (blueLift || greenLift || redLift || yellowLift)
        {
            P1T = false;
            P2T = false;
            P3T = false;
            P4T = false;
            colourBlue = false;
            colourGreen = false;
            colourRed = false;
            colourYellow = false;
        }

        cc.Move(new Vector3 (moveVector.x, jumping, 0f));
    }

    /// <summary>
    /// Will make the player Jump
    /// </summary>
    void Jump()
    {
		//if (jumping > 0f)
		jumping += uPhysics.velocity.y*jumpSpeed; //Mathf.Lerp(jumpSpeed * Time.deltaTime, -(uPhysics.velocity.y) * Time.deltaTime, 0.5f);

        if (cc.isGrounded && jumping < 0f)
        {
            jumping = 0f;
            jumpState = false;
			audiMan.Land.Play();
        }
    }

    /// <summary>
    /// Does the ability assigned to that player
    /// </summary>
    void DoAbility()
    {
        if (blue)
        {
            colourBlue = true;
        }
        if (yellow)
        {
            colourYellow = true;
        }
        if (green)
        {
            colourGreen = true;
        }
        if (red)
        {
            colourRed = true;
        }
        switch (playerId)
        {
            case 0:
				P1WasPressed = true;
                AbilityGrow();
                break;
            case 1:
				P2WasPressed = true;
				AbilityPull();
                break;
            case 2:
				P3WasPressed = true;
				AbilitySwitch();
                break;
            case 3:
				P4WasPressed = true;
				AbilityFloat();
                break;
        }
    }

    /// <summary>
    /// The ability to grow shapes
    /// </summary>
    void AbilityGrow()
    {
        P1T = true;
    }

    /// <summary>
    /// The ability to pull the shapes toward the player
    /// </summary>
    void AbilityPull()
    {
        FindObjectsInRange();
        P2T = true;
    }

    /// <summary>
    /// The ability to switch the shape with another one
    /// </summary>
    void AbilitySwitch()
    {
        P3T = true;
    }

    /// <summary>
    /// The ability to float shapes
    /// </summary>
    void AbilityFloat()
    {
        P4T = true;
    }

	IEnumerator PlaySteps() {
		audiMan.Step_1.pitch = Random.Range(0.5f, 1.5f);
		audiMan.Step_1.Play();
		yield return new WaitForSeconds(0.6f);
	}
}