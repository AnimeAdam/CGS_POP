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

	public float pullSpeed = 15f;
	private Transform objectTarget;
	float step;
	public bool allowGrow = true;
	public bool allowPull = true;

	// Start is called before the first frame update
	void Start()
    {
		Player = GameObject.Find("Player");
		placo = Player.GetComponent<PlayerController>();
		trafo = GetComponent<Transform>();
		objectTarget = Player.transform;
    }

	void Update()
	{
		if (placo.P1T == true && allowGrow == true) {
			Player1Action();
		}

		if (placo.P2T == true && allowPull == true)
		{
			Player2Action();
		}

		if (placo.P3T == true)
		{
			Player3Action();
		}

		if (placo.P4T == true)
		{
			Player4Action();
		}

		if (trafo.rotation.y != 0) {
			trafo.rotation = new Quaternion(0, 0, 0, 0);
		}

		if (trafo.position.z != 0) {
			trafo.position = new Vector3(trafo.position.x, trafo.position.y, 0);
		}
	}

	//contains action to be performed when player1 presses their button - GROWs shapes
	public void Player1Action() {

		trafo.localScale += newSize;
	//	StartCoroutine("SmallPause()");
		
	}

	//contains action to be performed when player2 presses their button - PULLs shapes towards the player
	public void Player2Action() {
		step = pullSpeed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, objectTarget.position, step);
	}

	//contains action to be performed when player3 presses their button - SWITCHes shape with an identically-sized different shape
	public void Player3Action() {

	}

	//contains action to be performed when player4 presses their button - FLOATs shapes upwards for as long as the button is held, then returns them slowly to the ground
	public void Player4Action() {

	}

	//IEnumerator SmallPause() {
	//	yield return new WaitForSeconds(2f);
	//}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Wall") {
			allowGrow = false;
		}

		if (other.tag == "Player") {
			allowPull = false;
		}

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Wall") {
			allowGrow = true;
		}

		if (other.tag == "Player")
		{
			allowPull = true;
		}
	}

}
