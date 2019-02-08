// MyCharacter.cs - A simple example showing how to get input from Rewired.Player

using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent(typeof(CharacterController))]
public class Player1 : MonoBehaviour
{

    // The Rewired player id of this character
    public int playerId = 1;

    // The movement speed of this character
    public float moveSpeed = 3.0f;

    // Jump Acceleration
    public float jumpSpeed = 1.0f;

    private Player player; // The Rewired Player
    private CharacterController cc;

    private Vector3 moveVector;
    private bool jump;
    private bool blue;
    private bool green;
    private bool red;
    private bool yellow;


    void Awake()
    {
        // Get the Rewired Player object for this player and keep it for the duration of the character's lifetime
        player = ReInput.players.GetPlayer(playerId);

        // Get the character controller
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("MoveHorizontal1"); // get input by name or action id
        jump = player.GetButtonDown("Jump1");
        blue = player.GetButtonDown("GrowBlue1");
        green = player.GetButtonDown("GrowGreen1");
        red = player.GetButtonDown("GrowRed1");
        yellow = player.GetButtonDown("GrowYellow1");
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            cc.Move(moveVector * moveSpeed * Time.deltaTime);
        }

        // Process actions
        if (jump)
        {
            
        }
        if (blue)
        {

        }
        if (green)
        {

        }
        if (red)
        {

        }
        if (yellow)
        {

        }
    }
}