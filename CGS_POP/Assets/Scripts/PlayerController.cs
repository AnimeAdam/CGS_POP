using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	//CONTENTS OF THIS SCRIPT:
	//Controls for players
	//Controls for pressing coloured buttons, which then searches the scene for relevant shapes and tells them to trigger their functions for as long as the button is held

	public bool P1T = false;
	public bool P2T = false;
	public bool P3T = false;
	public bool P4T = false;
	public GameObject playerInd;
	public PlayerIndicator playInd;
	public float jump = 12;
	float moveVelocity;

	// Start is called before the first frame update
	void Start()
    {
		playerInd = GameObject.Find("ColourIndicator");
		playInd = playerInd.GetComponent<PlayerIndicator>();
    }

    // Update is called once per frame
    void Update()
    {

		if (Input.GetKey(KeyCode.Q) && playInd.player1Selected == true)
		{
			P1T = true;
		}
		else {
			P1T = false;
		}

		if (Input.GetKey(KeyCode.W) && playInd.player2Selected == true)
		{
			P2T = true;
		}
		else
		{
			P2T = false;
		}

		if (Input.GetKeyDown(KeyCode.E) && playInd.player3Selected == true)
		{
			P3T = true;
		}
		else
		{
			P3T = false;
		}

		if (Input.GetKey(KeyCode.R) && playInd.player4Selected == true)
		{
			P4T = true;
		}
		else
		{
			P4T = false;
		}

		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
		{
			Right(4f);
		}

		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
		{
			Left(4f);
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump();
		}

		if (!Input.anyKey) {
			GetComponent<Rigidbody>().velocity = new Vector2(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y);
		}

	}


	public void Jump()
	{
		GetComponent<Rigidbody>().velocity = new Vector2(GetComponent<Rigidbody>().velocity.x, jump);
	}


	public void Left(float speed)
	{
		moveVelocity = -speed;
		GetComponent<Rigidbody>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody>().velocity.y);
	}

	public void Right(float speed)
	{
		moveVelocity = speed;
		GetComponent<Rigidbody>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody>().velocity.y);
	}

}
