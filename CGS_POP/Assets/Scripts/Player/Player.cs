using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    }

    // Update is called once per frame
    void Update()
    {
        //DoActions(_inputsManager.keyBeingPressed);
    }

    #endregion

    #region Player

    void AssignedPlayerNo()
    {
        switch (this.gameObject.tag)
        {
            case "Player1":
                thisPlayerInfo = _inputsManager.playersInfo[0];
                ability = Abilities.Ability.FLOAT;
                break;
            case "Player2":
                thisPlayerInfo = _inputsManager.playersInfo[1];
                ability = Abilities.Ability.GROW;
                break;
            case "Player3":
                thisPlayerInfo = _inputsManager.playersInfo[2];
                ability = Abilities.Ability.PULL;
                break;
            case "Player4":
                thisPlayerInfo = _inputsManager.playersInfo[3];
                ability = Abilities.Ability.SWITCH;
                break;
        }
    }

    /// <summary>
    /// Assign buttons to each actions          //ADAM feature to add later
    /// </summary>
    void AssignButtons()
    {

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

    void DoAbility()            //ADAM FINISH NOW
    {

    }

    #endregion

    #region ListenForInputs

    void DoActions(KeyCode[] _keyCodes)
    {
        if (thisPlayerInfo._controller == InputsManager.CONTROLLERTYPE.PS)
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (_keyCodes[i] == thisPlayerInfo._buttons[1])
                {
                    Jump();
                }
            }
        }
        if (thisPlayerInfo._controller == InputsManager.CONTROLLERTYPE.XB)
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                if (_keyCodes[i] == thisPlayerInfo._buttons[0])
                {
                    Jump();
                }
            }
        }
        Running();
    }

    #endregion
}
