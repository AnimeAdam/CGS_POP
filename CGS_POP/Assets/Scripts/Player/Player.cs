using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Xml.Schema;
using UnityEngine;

public class Player : MonoBehaviour
{
    //PlayerStats
    public float jumpAcceleration = 1.0f;       //The acceleration the player jumps up
    public float runningSpeed = 1.0f;           //The speed the player runs
    public bool direction = false;              //The direction the player is facing, false is right; true is left
    public Abilities.Ability ability = Abilities.Ability.NOABILITY;     //The players ability

    public InputsManager _inputsManager;
    public InputsManager.PlayerInfo thisPlayerInfo;

    //GameObject
    private Transform trsm;
    private Rigidbody rb;

    //Actions
    private KeyCode JumpButton;
    private KeyCode BlueButton;
    private KeyCode PinkButton;
    private KeyCode GreenButton;
    private KeyCode RedButton;

    [SerializeField] static public bool P1T = false;
    [SerializeField] static public bool P2T = false;
    [SerializeField] static public bool P3T = false;
    [SerializeField] static public bool P4T = false;

    #region UnityEvents

    void Awake()
    {
        trsm = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //DEBUG
        //Debug.Log(thisPlayerInfo._players);

        AssignedPlayerNo();
        AssignButtons();
    }

    // Update is called once per frame
    void Update()
    {
        ResetAbility();
        DoActions(_inputsManager.keyBeingPressed);
    }

    #endregion

    #region Player

    void AssignedPlayerNo()
    {
        switch (this.gameObject.tag)
        {
            case "Player1":
                thisPlayerInfo = _inputsManager.playersInfo[0];
                break;
            case "Player2":
                thisPlayerInfo = _inputsManager.playersInfo[1];
                break;
            case "Player3":
                thisPlayerInfo = _inputsManager.playersInfo[2];
                break;
            case "Player4":
                thisPlayerInfo = _inputsManager.playersInfo[3];
                break;
        }
    }

    /// <summary>
    /// Assign buttons to each actions          //ADAM feature to add later
    /// </summary>
    void AssignButtons()
    {
        if (thisPlayerInfo._controller == InputsManager.CONTROLLERTYPE.PS)
        {
            JumpButton = thisPlayerInfo._buttons[5];
            BlueButton = thisPlayerInfo._buttons[1];
            PinkButton = thisPlayerInfo._buttons[0];
            GreenButton = thisPlayerInfo._buttons[3];
            RedButton = thisPlayerInfo._buttons[2];
        }

        if (thisPlayerInfo._controller == InputsManager.CONTROLLERTYPE.XB)
        {
            JumpButton = thisPlayerInfo._buttons[5];
            BlueButton = thisPlayerInfo._buttons[0];
            PinkButton = thisPlayerInfo._buttons[2];
            GreenButton = thisPlayerInfo._buttons[3];
            RedButton = thisPlayerInfo._buttons[1];
        }

        if (thisPlayerInfo._controller == InputsManager.CONTROLLERTYPE.KB)
        {
            switch (thisPlayerInfo._players)
            {
                case InputsManager.PLAYERS.P1:
                      JumpButton = KeyCode.Q;
                      BlueButton = KeyCode.E;
                      PinkButton = KeyCode.Z;
                      GreenButton = KeyCode.X;
                      RedButton = KeyCode.C;
                    break;
                case InputsManager.PLAYERS.P2:
                    JumpButton = KeyCode.R;
                    BlueButton = KeyCode.Y;
                    PinkButton = KeyCode.V;
                    GreenButton = KeyCode.B;
                    RedButton = KeyCode.N;
                    break;
                case InputsManager.PLAYERS.P3:
                    JumpButton = KeyCode.U;
                    BlueButton = KeyCode.O;
                    PinkButton = KeyCode.M;
                    GreenButton = KeyCode.Comma;
                    RedButton = KeyCode.Period;
                    break;
                case InputsManager.PLAYERS.P4:
                    JumpButton = KeyCode.Keypad7;
                    BlueButton = KeyCode.Keypad9;
                    PinkButton = KeyCode.Keypad1;
                    GreenButton = KeyCode.Keypad2;
                    RedButton = KeyCode.Keypad3;
                    break;
            }
        }
    }

    #endregion

    #region Actions

    /// <summary>
    /// The player will Jump :)
    /// </summary>
    void Jump()
    {
        rb.AddForce(new Vector3(0f,jumpAcceleration,0f), ForceMode.Impulse);
    }

    /// <summary>
    /// The Player run in either direction :)
    /// </summary>
    void Running()
    {
        Vector3[] _movement = _inputsManager.ReturnMovement(thisPlayerInfo);
        _movement[0].x *= runningSpeed;
        _movement[0].x *= runningSpeed;
        rb.AddForce(new Vector3(_movement[0].x,0f,0f), ForceMode.Impulse);
        rb.AddForce(new Vector3(_movement[1].x, 0f, 0f), ForceMode.Impulse);
    }

    /// <summary>
    /// Activates the players ability
    /// </summary>
    void DoAbility()
    {
        switch (thisPlayerInfo._players)
        {
            case InputsManager.PLAYERS.P1:
                P1T = true;
                break;
            case InputsManager.PLAYERS.P2:
                P2T = true;
                break;
            case InputsManager.PLAYERS.P3:
                P3T = true;
                break;
            case InputsManager.PLAYERS.P4:
                P4T = true;
                break;
            default:
                Debug.Log("Something has gone wrong and the shape won't change");
                break;
        }
    }

    void ResetAbility()
    {
        switch (thisPlayerInfo._players)
        {
            case InputsManager.PLAYERS.P1:
                P1T = false;
                break;
            case InputsManager.PLAYERS.P2:
                P2T = false;
                break;
            case InputsManager.PLAYERS.P3:
                P3T = false;
                break;
            case InputsManager.PLAYERS.P4:
                P4T = false;
                break;
            default:
                Debug.Log("Something has gone wrong and the shape won't change");
                break;
        }
    }

    #endregion

    #region ListenForInputs

    /// <summary>
    /// Check if a list of inputs is being pressed and then do the action
    /// </summary>
    /// <param name="_keyCodes">Set the array of inputs from Unity KeyCode enum</param>
    void DoActions(KeyCode[] _keyCodes)
    {
        if (thisPlayerInfo._controller == InputsManager.CONTROLLERTYPE.PS)
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (_keyCodes[i] == JumpButton)
                {
                    Jump();
                }
                if (_keyCodes[i] == BlueButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == PinkButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == GreenButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == RedButton)
                {
                    DoAbility();
                }
            }
        }
        if (thisPlayerInfo._controller == InputsManager.CONTROLLERTYPE.XB)
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (_keyCodes[i] == JumpButton)
                {
                    Jump();
                }
                if (_keyCodes[i] == BlueButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == PinkButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == GreenButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == RedButton)
                {
                    DoAbility();
                }
            }
        }
        if (thisPlayerInfo._controller == InputsManager.CONTROLLERTYPE.KB)
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (_keyCodes[i] == JumpButton)
                {
                    Jump();
                }
                if (_keyCodes[i] == BlueButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == PinkButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == GreenButton)
                {
                    DoAbility();
                }
                if (_keyCodes[i] == RedButton)
                {
                    DoAbility();
                }
            }
        }
        Running();
    }

    #endregion
}
