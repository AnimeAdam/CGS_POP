// MyCharacter.cs - A simple example showing how to get input from Rewired.Player

using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class GamePlayer : MonoBehaviour
{
    //Player Health
    public int playerHealth = 1;

    // Audio Management
    AudioManager audiMan;

    //Particle Systems
    ParticleSystem landCloud;
    public float lengthOfLandingCloud = 30f;
    public ParticleSystem Dust;
    public ParticleSystem Sparkle;
    public ParticleSystem growParticle;
    public ParticleSystem pullParticle;
    public ParticleSystem switchParticle;
    public ParticleSystem floatParticle;
    Vector3 spawnOffset = new Vector3();

    public ParticleSystem player1ability;
    public ParticleSystem player2ability;

    public ParticleSystem player4ability;

    // The Rewired player id of this character
    public int playerId = 0;

    // The movement speed of this character
    public float moveSpeed = 3.0f;
    public float rollingSpeed = 1f;
    private bool stopMoving = false;

    // Jump Stats
    public float jumpHeight = 0.05f;
    public float jumpSpeed = 0.05f;
    [SerializeField] protected float jumping;
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
    private bool menuOpen;
    private bool scoreOpen;

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

    //Bools for effects purposes
    bool P1WasPressed = false;
    bool P2WasPressed = false;
    bool P3WasPressed = false;    //This needs to be given SFX
    bool P4WasPressed = false;

    //Classes
    private UniversalPhysics uPhysics;
    private Player player; // The Rewired Player
    private CharacterController cc;
    private Rigidbody rb;

    //Menu
    private Menus menus;
    [HideInInspector] static public bool menuOpenClose = true;

    //Mesh and Collider nonsense
    private Mesh _mesh;

    //Spawning
    private Vector3 spawnPoints;

    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        cc = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        uPhysics = GetComponent<UniversalPhysics>();
        audiMan = FindObjectOfType<AudioManager>();
        landCloud = GetComponentInChildren<ParticleSystem>();

        //Menus
        menus = FindObjectOfType<Menus>();

        CreateNewMesh();

        spawnPoints = transform.position;
    }

    private void Start()
    {
        ParticleSystem Dust = GetComponent<ParticleSystem>();
        ParticleSystem Sparkle = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        GetInput();
        ProcessInput();
        
        if (playerHealth != 1)
        {
            audiMan.RespawnSFX.Play();
            StartCoroutine("StopMoving");
            Spawning();
            DustSparkle();
        }

        if (P1WasPressed == false) {
            growParticle.Stop();
        }

        if (P2WasPressed == false)
        {
            pullParticle.Stop();
        }

        if (P4WasPressed == false)
        {
            floatParticle.Stop();
        }

    }

    void DustSparkle()
    {
        Sparkle.Play();
        //Just for landCloud
        Vector3 vec3 = new Vector3(transform.position.x, 5000f, transform.position.z);
        Vector3[] verticsPos = GetComponent<MeshFilter>().mesh.vertices;
        for (int i = 0; i < verticsPos.Length; i++)
        {
            verticsPos[i] = transform.TransformPoint(verticsPos[i]);
            if (verticsPos[i].y < vec3.y)
            {
                vec3.y = verticsPos[i].y;
            }
        }
        landCloud.transform.position = vec3;
        landCloud.Play();

    }


    #region Inputs

    /// <summary>
    /// Gets the inputs from Rewired
    /// </summary>
    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter whether the input is coming from a joystick, the keyboard, mouse, or acustom controller.

        if (!stopMoving)
        {
            moveVector.x = player.GetAxis("MoveHorizontal"); // get input by name or action id
            jump = player.GetButtonDown("Jump");
        }

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

        //Menu Button
        menuOpen = player.GetButtonDown("Start");
        scoreOpen = player.GetButtonDown("Select");
    }

    /// <summary>
    /// Processes inputs into actions
    /// </summary>
    private void ProcessInput()
    {
        //Menu Navigation
        if (!menuOpenClose && !menus.scoreMenu.activeSelf) //frames
        {
            if (moveVector.x > 0f && player.GetButtonDown("MoveHorizontal"))
            {
                menus.NavigateLeftRightButton(true, menus.highlightedMenu);
            }
            if (moveVector.x < 0f && player.GetNegativeButtonDown("MoveHorizontal")) // 
            {
                menus.NavigateLeftRightButton(false, menus.highlightedMenu);
            }

            if (blue)
            {
                menus.PressButton();
            }
        }
        else
        {
            if (!stopMoving)
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
                        audiMan.Jump.Play();
                        jumping = jumpHeight;
                        jumpState = true;
                    }
                }

                if (jumpState)
                {
                    Jump();
                }

                if (blue || green || red || yellow)
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

                //Work out the force from the movement from CC then use that for continus force on the egg
                transform.Rotate(new Vector3(0f, 0f, -1f), (moveVector.x * rollingSpeed));

                //uPhysics.gravity.y);
                cc.Move(new Vector3(moveVector.x, jumping, 0f));
                transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            }
        }

        if (menuOpen && !menus.scoreMenu.activeSelf)
        {
            if (menuOpenClose)
            {
                menus.OpenMenu(true, menus.mainMenu);
                menuOpenClose = false;
            }
            else
            {
                if (menus.mainMenu.activeSelf)
                {
                    menus.OpenMenu(false, menus.mainMenu);
                    menuOpenClose = true;
                }
            }
        }

        if (scoreOpen && !menus.mainMenu.activeSelf && !menus.levelSelectMenu.activeSelf && menus.showTimer)
        {
            if (menuOpenClose)
            {
                menus.OpenMenu(true, menus.scoreMenu);
                menuOpenClose = false;
            }
            else
            {
                menus.OpenMenu(false, menus.scoreMenu);
                menuOpenClose = true;
            }
        }
    }

    #endregion

    #region Actions

    /// <summary>
    /// Will make the player Jump
    /// </summary>
    void Jump()
    {
        //if (jumping > 0f)
        jumping += uPhysics.velocity.y * jumpSpeed; //Mathf.Lerp(jumpSpeed * Time.deltaTime, -(uPhysics.velocity.y) * Time.deltaTime, 0.5f);

        if (cc.isGrounded && jumping < 0f)
        {
            jumping = 0f;
            jumpState = false;
            audiMan.Land.Play();
            StartCoroutine("LandingDustCloud");
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
        if (!(GameObject.Find("Ability_Grow(Clone)")))
        {
            spawnOffset = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
            Instantiate(growParticle, spawnOffset, Quaternion.Euler(90, 0, 0));
        }

    }

    /// <summary>
    /// The ability to pull the shapes toward the player
    /// </summary>
    void AbilityPull()
    {
        //FindObjectsInRange();
        P2T = true;
        if (!(GameObject.Find("Ability_Pull(Clone)")))
        {
            spawnOffset = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
            Instantiate(pullParticle, spawnOffset, Quaternion.Euler(90, 0, 0));
        }
    }

    /// <summary>
    /// The ability to switch the shape with another one
    /// </summary>
    void AbilitySwitch()
    {
        P3T = true;
        Instantiate(switchParticle, transform.position, Quaternion.Euler(90, 0, 0));
    }

    /// <summary>
    /// The ability to float shapes
    /// </summary>
    void AbilityFloat()
    {
        P4T = true;
        if (!(GameObject.Find("Ability_Float(Clone)")))
        {
            spawnOffset = new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f);
            Instantiate(floatParticle, spawnOffset, Quaternion.Euler(-90, 0, 0));
        }
    }

    /// <summary>
    /// Finding objects in range for the pull Ability
    /// </summary>
    private void FindObjectsInRange()
    {
        //areaOfInfluence = Physics.OverlapSphere(transform.position, areaOfInfluenceRadius);
        //areaOfInfluence = RotaryHeart.Lib.PhysicsExtension.Physics.OverlapSphere(transform.position, 
        //    areaOfInfluenceRadius, -1, RotaryHeart.Lib.PhysicsExtension.Physics.PreviewCondition.Editor);

        GameObject orb = (GameObject)Resources.Load("SphereOfInfluence");
        orb.transform.localScale = new Vector3(areaOfInfluenceRadius * 2, 
            areaOfInfluenceRadius * 2, areaOfInfluenceRadius * 2);

        Instantiate(orb, transform.position, Quaternion.identity); //REMEMBER as GameObject
    }

    void Spawning()
    {
        moveVector = Vector3.zero;
        uPhysics.velocity = Vector3.zero;
        cc.Move(Vector3.zero);
        transform.position = spawnPoints;
        playerHealth = 1;
    }

    #endregion

    //IEnumerator PlaySteps() {
    //	audiMan.Step_1.pitch = Random.Range(0.5f, 1.5f);
    //	audiMan.Step_1.Play();
    //	yield return new WaitForSeconds(0.6f);
    //}

    #region CreateComponents

    void CreateNewMesh()
    {
        _mesh = new Mesh();
        _mesh.vertices = GetComponent<MeshFilter>().mesh.vertices;
        _mesh.triangles = GetComponent<MeshFilter>().mesh.triangles;
        _mesh.uv = GetComponent<MeshFilter>().mesh.uv;
        _mesh.normals = GetComponent<MeshFilter>().mesh.normals;
        _mesh.colors = GetComponent<MeshFilter>().mesh.colors;
        _mesh.tangents = GetComponent<MeshFilter>().mesh.tangents;

        Vector3[] _vertices = _mesh.vertices;
        for (int i = 0; i < _vertices.Length; i++)
        {
            _vertices[i].x *= 1.4f;
            _vertices[i].y *= 1.4f;
            _vertices[i].z *= 1.4f;
        }
        _mesh.vertices = _vertices;

        MeshCollider _meshCollider = gameObject.AddComponent<MeshCollider>();
        _meshCollider.sharedMesh = _mesh;
        _meshCollider.convex = true;
        _meshCollider.isTrigger = true;
    }

    #endregion

    #region IEnumerator

    IEnumerator LandingDustCloud()  //15 frames
    {
        Vector3 vec3 = new Vector3(transform.position.x, 5000f, transform.position.z);
        Vector3[] verticsPos = GetComponent<MeshFilter>().mesh.vertices;
        landCloud.Play();

        for (int i = 0; i < verticsPos.Length; i++)
        {
            verticsPos[i] = transform.TransformPoint(verticsPos[i]);
            if (verticsPos[i].y < vec3.y)
            {
                vec3.y = verticsPos[i].y;
            }
        }

        for (int i = 0; i < lengthOfLandingCloud; i++)
        {
            landCloud.transform.position = vec3;
            landCloud.transform.rotation = Quaternion.identity;
            yield return 0;
        }

        landCloud.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    IEnumerator StopMoving()
    {
        stopMoving = true;
        for (int i = 0; i < 20; i++)
        {
            if (i == 19)
            {
                stopMoving = false;
            }
            yield return 0;
        }
    }

    #endregion

}