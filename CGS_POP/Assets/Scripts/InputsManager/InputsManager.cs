using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class InputsManager : MonoBehaviour
{
    //Constants
    private const int NUMBEROFKEYCODE = 509;                            //The total number of key code inputs
    [FlagsAttribute] public enum PLAYERS : short {NOPLAYER = 0, P1 = 1, P2 = 2, P3 = 4, P4 = 8};     //Players connected
    public enum CONTROLLERTYPE {NOCONTROLLER,PS,XB,KB};

    //Classes

    #region PlayerInfo
    public class PlayerInfo
    {
        //Variables
        private PLAYERS player = PLAYERS.NOPLAYER;
        private CONTROLLERTYPE controller = CONTROLLERTYPE.NOCONTROLLER;
        private string playerTag = "";
        private KeyCode[] buttons = new KeyCode[]{};

        //Getters & Setters
        public PLAYERS _players {get{return player;}set { player = value; }}
        public CONTROLLERTYPE _controller { get { return controller; } set { controller = value; } }
        public string _playerTag { get { return playerTag; } set { playerTag = value; } }
        public KeyCode[] _buttons { get { return buttons; } set { buttons = value; } }

        //Constructor
        /*public PlayerInfo()
        {
            _players = PLAYERS.NOPLAYER;
            _controller = CONTROLLERTYPE.NOCONTROLLER;
        }*/

        //Deconstructor
        /// <summary>
        /// Reset the class to default
        /// </summary>
        public void ResetPlayerInfo()
        {
            player = PLAYERS.NOPLAYER;
            controller = CONTROLLERTYPE.NOCONTROLLER;
            playerTag = "";
            buttons = new KeyCode[] {};
        }

        //Functions
        /// <summary>
        /// Sets the player tag to the PlayerInfo
        /// </summary>
        /// <param name="_player">PLAYERS enum assigned to the PlayerInfo</param>
        public void SetPlayerTag()
        {
            playerTag = player.ToString() + controller.ToString();
        }

        /// <summary>
        /// Sets the buttons for the controller
        /// </summary>
        public void SetControllerButtons()
        {
            switch (playerTag)
            {
                case "P1PS":
                    buttons = Playstation.P1PS;
                    break;
                case "P2PS":
                    buttons = Playstation.P2PS;
                    break;
                case "P3PS":
                    buttons = Playstation.P3PS;
                    break;
                case "P4PS":
                    buttons = Playstation.P4PS;
                    break;
                case "P1XB":
                    buttons = Xbox.P1XB;
                    break;
                case "P2XB":
                    buttons = Xbox.P2XB;
                    break;
                case "P3XB":
                    buttons = Xbox.P3XB;
                    break;
                case "P4XB":
                    buttons = Xbox.P4XB;
                    break;
            }
        }
    }

    #endregion

    //Varibles
    public PlayerInfo[] playersInfo = {new PlayerInfo(), new PlayerInfo(), new PlayerInfo(), new PlayerInfo(), new PlayerInfo()}; //Array of players
    public PLAYERS NUMOFPLAYERS = PLAYERS.NOPLAYER;
    public KeyCode[] keyBeingPressed = new KeyCode[40];

    public float DEADZONE = 0.2f;

    #region UnityEvents

    void Awake()
    {
        FindPlayerControllers();
    }

    // Start is called before the first frame update
    void Start()
    {   //DEBUG
        //DebugFindControllers();
        //FindPlayerControllers();
    }

    // Update is called once per frame
    void Update()
    {
        //DEBUGS
        //DebugAnalogueInputs();
        //DebugInputs();
        /*if (ReturnKeypressed() == Playstation.P1PS[1])
        {
            Debug.Log("X has been pressed");
        }
        transform.position = ReturnMovement(playersInfo[0])[0];
        if (ReturnMovement(playersInfo[0])[0] == Vector3.zero)
        {
            transform.position = ReturnMovement(playersInfo[0])[1];
        }*/
        /*
        KeyCode[] _keyCodes = ReturnKeypressed();
        if (_keyCodes != new KeyCode[] { })
        {
            for (int i = 0; i < _keyCodes.Length; i++)
            {
                Debug.Log(_keyCodes[i]);
            }
        }
        */
        KeysPressed();
    }

    #endregion

    #region Get Inputs

    /// <summary>
    /// Returns movement from controllers, [0] is Analogue [1] is Digital D-pad
    /// </summary>
    public Vector3[] ReturnMovement(PlayerInfo _playerInfo)
    {
        string _string = _playerInfo._playerTag;
        Vector3[] XYMOVEMENT = { new Vector3(0f, 0f, 0f), new Vector3(0f, 0f, 0f) };

        XYMOVEMENT[0].x = Input.GetAxis(_string + "H");
        XYMOVEMENT[0].y = -Input.GetAxis(_string + "V");
        if (XYMOVEMENT[0].x < DEADZONE && XYMOVEMENT[0].x > -DEADZONE) { XYMOVEMENT[0].x = 0; }
        if (XYMOVEMENT[0].y < DEADZONE && XYMOVEMENT[0].y > -DEADZONE) { XYMOVEMENT[0].y = 0; }

        if(_playerInfo._controller != CONTROLLERTYPE.KB) { 
        if (XYMOVEMENT[1].x == 0) { XYMOVEMENT[1].x = Input.GetAxis(_string + "DH"); }
        if (XYMOVEMENT[1].y == 0) { XYMOVEMENT[1].y = Input.GetAxis(_string + "DV"); }
        }

        return XYMOVEMENT;
    }

    /// <summary>
    /// Returns the key pressed
    /// </summary>
    void KeysPressed()
    {
        keyBeingPressed = new KeyCode[40];
        int _keyCodeNum = 0;
        for (int i = 0; i < NUMBEROFKEYCODE; i++)
        {
            if (Input.GetKey((KeyCode)i))// && !(i > 330 && i < 349))
            {
                keyBeingPressed[_keyCodeNum] = (KeyCode)i;
                _keyCodeNum++;
            }
        }
    }

    #endregion

    #region Find Controllers and Players

    /// <summary>
    /// Finds how many controllers are connected
    /// </summary>
    void FindPlayerControllers()
    {
        PLAYERS _players = PLAYERS.P1;
        int _player = 0;
        int _playerNum = 1;     //Counts the number of players

        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            string _controller = Input.GetJoystickNames()[i];
            if (_controller == "")
            {
                _controller = "Keyboard";
            }

            if (_player < 4)
            {
                switch (_controller)
                {
                    case "Wireless Controller":
                        playersInfo[_player]._controller = CONTROLLERTYPE.PS;
                        AssignPlayer(playersInfo[_player], _players);
                        playersInfo[_player].SetPlayerTag();
                        playersInfo[_player].SetControllerButtons();
                        AddToNumberOfPlayers(playersInfo[_player]);
                        _playerNum *= 2;
                        _players = (PLAYERS)_playerNum;
                        _player++;
                        Debug.Log("Player " + _player + " is a PlayStation Controller");
                        break;
                    case "Controller (XBOX 360 For Windows)": //ADAM CHANGE WHEN TESTING 360 CONTROLLER
                        playersInfo[_player]._controller = CONTROLLERTYPE.XB;
                        AssignPlayer(playersInfo[_player], _players);
                        playersInfo[_player].SetPlayerTag();
                        playersInfo[_player].SetControllerButtons();
                        AddToNumberOfPlayers(playersInfo[_player]);
                        _playerNum *= 2;
                        _players = (PLAYERS)_playerNum;
                        _player++;
                        Debug.Log("Player " + _player + " is a Xbox Controller");
                        break;
                    case null:
                        Debug.Log("No Controller at " + i);
                        break;
                    default:
                        Debug.Log("InputManager.FindPlayer() Unknown Controller: " + _controller);
                        break;
                }
            }
        }

        while (_player < 4)
        {
            playersInfo[_player]._controller = CONTROLLERTYPE.KB;
            AssignPlayer(playersInfo[_player], _players);
            playersInfo[_player].SetPlayerTag();
            playersInfo[_player].SetControllerButtons();
            AddToNumberOfPlayers(playersInfo[_player]);
            _playerNum *= 2;
            _players = (PLAYERS)_playerNum;
            _player++;
            Debug.Log("Player " + _player + " is a Keyboard");
        }

        //_playerNum
        //SetNumberOfPlayer(_playerNum);
    }

    /// <summary>
    /// Assign the player to the controller
    /// </summary>
    /// <param name="_playerInfo">Assign the controller you want to the player you want</param>
    /// <param name="_player">PLAYERS enum assigned to the PlayerInfo</param>
    void AssignPlayer(PlayerInfo _playerInfo, PLAYERS _player)
    {
        _playerInfo._players = _player;
    }

    /// <summary>
    /// Adds up the total number of players after they have been assigned a controller and a player number
    /// </summary>
    /// <param name="_playerInfo"></param>
    void AddToNumberOfPlayers(PlayerInfo _playerInfo)
    {
        NUMOFPLAYERS = NUMOFPLAYERS | _playerInfo._players;
    }

    /// <summary>
    /// Intialises the controller to the player
    /// </summary>
    void AssignIntialPlayers()
    {
        PLAYERS _players = PLAYERS.P1;
        int playerNum = 1;

        for (int i = 0; i < 4; i++)
        {
            if (playersInfo[i] != null)
            {
                playersInfo[i]._players = _players;
                playerNum *= 2;
                _players = (PLAYERS)playerNum;
            }
        }
    }

    /// <summary>
    /// Initalises all controllers and players to there defaults
    /// </summary>
    void InitaliseAllPlayers()
    {
        AssignIntialPlayers();
        for (int i = 0; i < 4; i++)
        {
            if (playersInfo[i] != null)
            {
                playersInfo[i].SetPlayerTag();
            }
        }
    }

    #endregion

    #region Debugs

    /// <summary>
    /// For finding the input number in KeyCode
    /// </summary>
    void DebugInputs()
    {
        for (int i = 0; i < NUMBEROFKEYCODE; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                KeyCode _keyCode = (KeyCode)i;
                //string _string = (string) _keyCode;
                Debug.Log("You just pressed: " + _keyCode + " At number: " + i);
            }
        }
    }

    /// <summary>
    /// For debugging and test analogue and directional inputs
    /// </summary>
    void DebugAnalogueInputs()
    {
        //ADAM CONVERT THIS CHECKING THE ANALOGUE INPUTS OF THE CONTROLLERS AND MOVEMENT
        Vector3 XYMOVEMENT = new Vector3(0f,0f,0f);

        XYMOVEMENT.x = Input.GetAxis("P1PSH");
        XYMOVEMENT.y = -Input.GetAxis("P1PSV");

        if (XYMOVEMENT.x < DEADZONE && XYMOVEMENT.x > -DEADZONE) { XYMOVEMENT.x = 0; }
        if (XYMOVEMENT.y < DEADZONE && XYMOVEMENT.y > -DEADZONE) { XYMOVEMENT.y = 0; }

        if (XYMOVEMENT.x == 0) {XYMOVEMENT.x = Input.GetAxis("P1PSDH");}
        if (XYMOVEMENT.y == 0) {XYMOVEMENT.y = Input.GetAxis("P1PSDV");}

        transform.position = XYMOVEMENT;
    }

    /// <summary>
    /// Check all the controllers connected
    /// </summary>
    void DebugFindControllers()
    {
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
        {
            Debug.Log(Input.GetJoystickNames()[i]);
        }
    }
    #endregion
}