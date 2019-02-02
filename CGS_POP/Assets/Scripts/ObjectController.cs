using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	//this will later have to be updated to find an array of playercontroller scripts, as there should be four in scene which all need to be reacted to

    // Start is called before the first frame update
    void Start()
    {
		GameObject Player = GameObject.Find("Player");
		PlayerController pc = Player.GetComponent<PlayerController>();
		Transform tf = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//contains action to be performed when player1 presses their button - GROW
	void Player1Action() {

	}

	//contains action to be performed when player2 presses their button - PULL
	void Player2Action() {

	}

	//contains action to be performed when player3 presses their button - SWITCH
	void Player3Action() {

	}

	//contains action to be performed when player4 presses their button - FLOAT
	void Player4Action() {

	}

}
