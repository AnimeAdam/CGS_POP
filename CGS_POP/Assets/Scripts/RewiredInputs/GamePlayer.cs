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
    public float jumpHeight = 0.6f;
    public float jumpSpeed = 0.1f;
    [SerializeField]protected float jumping;
    private bool jumpState = false;

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

    private bool blueLift;
    private bool greenLift;
    private bool redLift;
    private bool yellowLift;

    //Collider for detecting shapes at a distance
    private Collider[] areaOfInfluence;
    public float areaOfInfluenceRadius = 3f;

    static public bool P1T = false;
    static public bool P2T = false;
    static public bool P3T = false;
    static public bool P4T = false;

	public GameObject[] blueShapes = new GameObject[20];
	public GameObject[] greenShapes = new GameObject[20];
	public GameObject[] redShapes = new GameObject[20];
	public GameObject[] yellowShapes = new GameObject[20];
	public ObjectController[] blueComps = new ObjectController[20];
	public ObjectController[] greenComps = new ObjectController[20];
	public ObjectController[] redComps = new ObjectController[20];
	public ObjectController[] yellowComps = new ObjectController[20];
	public bool shapeCheck = false;

	int i = 0;

	UniversalPhysics uPhysics;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        uPhysics = GetComponent<UniversalPhysics>();

		blueShapes = GameObject.FindGameObjectsWithTag("Blue");
		greenShapes = GameObject.FindGameObjectsWithTag("Green");
		redShapes = GameObject.FindGameObjectsWithTag("Red");
		yellowShapes = GameObject.FindGameObjectsWithTag("Yellow");
	}

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void FindObjectsInRange()
    {
        //areaOfInfluence = Physics.OverlapSphere(transform.position, areaOfInfluenceRadius);
        areaOfInfluence = RotaryHeart.Lib.PhysicsExtension.Physics.OverlapSphere(transform.position,
            areaOfInfluenceRadius, -1, RotaryHeart.Lib.PhysicsExtension.Physics.PreviewCondition.Editor);
        GameObject orb = (GameObject)Resources.Load("SphereOfInfluence");
        orb.transform.localScale = new Vector3(areaOfInfluenceRadius*2, areaOfInfluenceRadius*2, areaOfInfluenceRadius*2);

        Instantiate(orb, transform.position,Quaternion.identity);
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
        }

        // Process actions
        if (jump)
        {
            if (jumping == 0f)
            {
                jumping = jumpHeight;
                jumpState = true;
            }
        }
        if (jumpState)
        {
            Jump();
        }

		//when a colour is pressed, the playerId is used to iterate over an array of that colour shapes and trigger the boolean associated with that playerId

		if (blue)
		{
			switch (playerId)
			{
				case 0:
					foreach (GameObject blueShape in blueShapes)
					{
						blueComps[i] = blueShape.GetComponent<ObjectController>();
						blueComps[i].P1T = true;
						i++;
					}
					break;
				case 1:
					foreach (GameObject blueShape in blueShapes)
					{
						blueComps[i] = blueShape.GetComponent<ObjectController>();
						blueComps[i].P2T = true;
						i++;
					}
					break;
				case 2:
					foreach (GameObject blueShape in blueShapes)
					{
						blueComps[i] = blueShape.GetComponent<ObjectController>();
						blueComps[i].P3T = true;
						i++;
					}
					break;
				case 3:
					foreach (GameObject blueShape in blueShapes)
					{
						blueComps[i] = blueShape.GetComponent<ObjectController>();
						blueComps[i].P4T = true;
						i++;
					}
					break;

			}
		}

		if (green)
		{
			switch (playerId)
			{
				case 0:
					foreach (GameObject greenShape in greenShapes)
					{
						i = 0;
						greenComps[i] = greenShape.GetComponent<ObjectController>();
						greenComps[i].P1T = true;
						i++;
					}
					break;
				case 1:
					foreach (GameObject greenShape in greenShapes)
					{
						i = 0;
						greenComps[i] = greenShape.GetComponent<ObjectController>();
						greenComps[i].P2T = true;
						i++;
					}
					break;
				case 2:
					foreach (GameObject greenShape in greenShapes)
					{
						i = 0;
						greenComps[i] = greenShape.GetComponent<ObjectController>();
						greenComps[i].P3T = true;
						i++;
					}
					break;
				case 3:
					foreach (GameObject greenShape in greenShapes)
					{
						i = 0;
						greenComps[i] = greenShape.GetComponent<ObjectController>();
						greenComps[i].P4T = true;
						i++;
					}
					break;

			}

		}

		if (red)
		{
			switch (playerId)
			{
				case 0:
					foreach (GameObject redShape in redShapes)
					{
						i = 0;
						redComps[i] = redShape.GetComponent<ObjectController>();
						redComps[i].P1T = true;
						i++;
					}
					break;
				case 1:
					foreach (GameObject redShape in redShapes)
					{
						i = 0;
						redComps[i] = redShape.GetComponent<ObjectController>();
						redComps[i].P2T = true;
						i++;
					}
					break;
				case 2:
					foreach (GameObject redShape in redShapes)
					{
						i = 0;
						redComps[i] = redShape.GetComponent<ObjectController>();
						redComps[i].P3T = true;
						i++;
					}
					break;
				case 3:
					foreach (GameObject redShape in redShapes)
					{
						i = 0;
						redComps[i] = redShape.GetComponent<ObjectController>();
						redComps[i].P4T = true;
						i++;
					}
					break;
			}
		}

		if (yellow)
		{
			switch (playerId)
			{
				case 0:
					foreach (GameObject yellowShape in yellowShapes)
					{
						i = 0;
						yellowComps[i] = yellowShape.GetComponent<ObjectController>();
						yellowComps[i].P1T = true;
						i++;
					}
					break;
				case 1:
					foreach (GameObject yellowShape in yellowShapes)
					{
						i = 0;
						yellowComps[i] = yellowShape.GetComponent<ObjectController>();
						yellowComps[i].P2T = true;
						i++;
					}
					break;
				case 2:
					foreach (GameObject yellowShape in yellowShapes)
					{
						i = 0;
						yellowComps[i] = yellowShape.GetComponent<ObjectController>();
						yellowComps[i].P3T = true;
						i++;
					}
					break;
				case 3:
					foreach (GameObject yellowShape in yellowShapes)
					{
						i = 0;
						yellowComps[i] = yellowShape.GetComponent<ObjectController>();
						yellowComps[i].P4T = true;
						i++;
					}
					break;
			}
		}

		if (blueLift)
		{
			switch (playerId)
			{
				case 0:
					foreach (ObjectController blueControl in blueComps)
					{
						i = 0;
						blueComps[i].P1T = false;
						i++;
					}
					break;
				case 1:
					foreach (ObjectController blueControl in blueComps)
					{
						i = 0;
						blueComps[i].P2T = false;
						i++;
					}
					break;
				case 2:
					foreach (ObjectController blueControl in blueComps)
					{
						i = 0;
						blueComps[i].P3T = false;
						i++;
					}
					break;
				case 3:
					foreach (ObjectController blueControl in blueComps)
					{
						i = 0;
						blueComps[4].P1T = false;
						i++;
					}
					break;
			}
		}

		if (greenLift)
		{
			switch (playerId)
			{
				case 0:
					foreach (ObjectController greenControl in greenComps)
					{
						i = 0;
						greenComps[i].P1T = false;
						i++;
					}
					break;
				case 1:
					foreach (ObjectController greenControl in greenComps)
					{
						i = 0;
						greenComps[i].P2T = false;
						i++;
					}
					break;
				case 2:
					foreach (ObjectController greenControl in greenComps)
					{
						i = 0;
						greenComps[i].P3T = false;
						i++;
					}
					break;
				case 3:
					foreach (ObjectController greenControl in greenComps)
					{
						i = 0;
						greenComps[i].P4T = false;
						i++;
					}
					break;
			}
		}

		if (redLift)
		{
			switch (playerId)
			{
				case 0:
					foreach (ObjectController redControl in redComps)
					{
						i = 0;
						redComps[i].P1T = false;
						i++;
					}
					break;

				case 1:
					foreach (ObjectController redControl in redComps)
					{
						i = 0;
						redComps[i].P2T = false;
						i++;
					}
					break;

				case 2:
					foreach (ObjectController redControl in redComps)
					{
						i = 0;
						redComps[i].P3T = false;
						i++;
					}
					break;

				case 3:
					foreach (ObjectController redControl in redComps)
					{
						i = 0;
						redComps[i].P4T = false;
						i++;
					}
					break;
			}
		}

		if (yellowLift)
		{
			switch (playerId)
			{
				case 0:
					foreach (ObjectController yellowControl in yellowComps)
					{
						i = 0;
						yellowComps[i].P1T = false;
						i++;
					}
					break;

				case 1:
					foreach (ObjectController yellowControl in yellowComps)
					{
						i = 0;
						yellowComps[i].P2T = false;
						i++;
					}
					break;

				case 2:
					foreach (ObjectController yellowControl in yellowComps)
					{
						i = 0;
						yellowComps[i].P3T = false;
						i++;
					}
					break;

				case 3:
					foreach (ObjectController yellowControl in yellowComps)
					{
						i = 0;
						yellowComps[i].P4T = false;
						i++;
					}
					break;
			}
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
        }
    }

    ///// <summary>
    ///// Does the ability assigned to that player
    ///// </summary>
    //void DoAbility()
    //{
    //    switch (playerId)
    //    {
    //        case 0:
    //            AbilityGrow();
    //            break;
    //        case 1:
    //            AbilityPull();
    //            break;
    //        case 2:
    //            AbilitySwitch();
    //            break;
    //        case 3:
    //            AbilityFloat();
    //            break;
    //    }
    //}

    ///// <summary>
    ///// The ability to grow shapes
    ///// </summary>
    //void AbilityGrow()
    //{
    //    P1T = true;
    //}

    ///// <summary>
    ///// The ability to pull the shapes toward the player
    ///// </summary>
    //void AbilityPull ()
    //{
    //    FindObjectsInRange();
    //    P2T = true;
    //}

    ///// <summary>
    ///// The ability to switch the shape with another one
    ///// </summary>
    //void AbilitySwitch()
    //{
    //    P3T = true;
    //}

    ///// <summary>
    ///// The ability to float shapes
    ///// </summary>
    //void AbilityFloat()
    //{
    //    P4T = true;
    //}
}