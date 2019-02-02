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

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



		if (Input.GetKey(KeyCode.Q))
		{
			P1T = true;
		}
		else {
			P1T = false;
		}

		if (Input.GetKey(KeyCode.W))
		{
			P2T = true;
		}
		else
		{
			P2T = false;
		}

		if (Input.GetKey(KeyCode.E))
		{
			P3T = true;
		}
		else
		{
			P3T = false;
		}

		if (Input.GetKey(KeyCode.R))
		{
			P4T = true;
		}
		else
		{
			P4T = false;
		}
	}
}
