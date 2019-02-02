using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	//this will later have to be updated to find an array of playercontroller scripts, as there should be four in scene which all need to be reacted to

	public GameObject Player;
	public PlayerController placo;
	public Transform trafo;
	Vector3 newSize = new Vector3(0.005f, 0.005f, 0.005f);

	// Start is called before the first frame update
	void Start()
    {
		Player = GameObject.Find("Player");
		placo = Player.GetComponent<PlayerController>();
		trafo = GetComponent<Transform>();
    }

	void Update()
	{
		if (placo.P1T == true) {
			Player1Action();
		}
	}

	//contains action to be performed when player1 presses their button - GROW
	public void Player1Action() {

		trafo.localScale += newSize;
	//	StartCoroutine("SmallPause()");
		
	}

	//contains action to be performed when player2 presses their button - PULL
	public void Player2Action() {

	}

	//contains action to be performed when player3 presses their button - SWITCH
	public void Player3Action() {

	}

	//contains action to be performed when player4 presses their button - FLOAT
	public void Player4Action() {

	}

	IEnumerator SmallPause() {
		yield return new WaitForSeconds(2f);
	}

}
