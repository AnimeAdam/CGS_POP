using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
	//this will later have to be updated to find an array of playercontroller scripts, as there should be four in scene which all need to be reacted to
    
	public GameObject Player;
	public PlayerController placo;
	public Transform trafo;
	Vector3 newSize = new Vector3(0.01f, 0.01f, 0.01f);
	public CollisionChecker upCheck;
	public CollisionChecker downCheck;
	public CollisionChecker leftCheck;
	public CollisionChecker rightCheck;


	public float pullSpeed = 4f;
	private Transform objectTarget;
	float step;
	public bool allowGrow = true;
	public bool allowPull = true;
	public bool allowSwitch = true;
	public bool allowFloat = true;

	Vector3 preswitchPosition;
	Quaternion preswitchRotation;
	Vector3 preswitchSize;
	GameObject prefabManager;
	PrefabArray shapeToBecome;
	public int currentColour; //blue = 1, green = 2, red = 3, yellow = 4
	public int currentShape; //circle = 0, square = 1, triangle = 2
	int i = 0;
	Transform thisGameobject;
	GameObject newShape;
	Transform childTrafo; //IF THIS IS CHANGED TO GAMEOBJECT ALONG WITH THE CORRESPONDING CODE, THE GETPOSITION FOR PRESWITCHPOSITION THROWS A NULLREF I.E. NO CHILD OBJECTS ARE BEING FOUND

	Vector3 floatUp = new Vector3(0, 0.1f, 0);
	Rigidbody thisRigid;

	// Start is called before the first frame update
	void Start()
    {
		Player = GameObject.Find("Player");
		placo = Player.GetComponent<PlayerController>();
		trafo = GetComponent<Transform>();
		//un-comment when fixing the object-trapped-so-can't-grow code
		//upCheck = GetComponentInChildren<CollisionChecker>();
		//downCheck = GetComponentInChildren<CollisionChecker>();
		//leftCheck = GetComponentInChildren<CollisionChecker>();
		//rightCheck = GetComponentInChildren<CollisionChecker>();
		objectTarget = Player.transform;
		prefabManager = GameObject.Find("PrefabManager");
		shapeToBecome = prefabManager.GetComponent<PrefabArray>();
		thisGameobject = GetComponent<Transform>();
		childTrafo = GetComponentInChildren<Transform>();
		thisRigid = GetComponent<Rigidbody>();
    }

	void Update()
	{
		//if opposite colliders are returning true, deactivate growth as object is now 'stuck'
		//if ((upCheck.objectInRange == true && downCheck.objectInRange == true) || (leftCheck.objectInRange == true && rightCheck.objectInRange == true))
		//{
		//	allowGrow = false;
		//}
		//else allowGrow = true;

		if (placo.P1T == true && allowGrow == true) {
			Player1Action();
		}

		if (placo.P2T == true && allowPull == true)
		{
			Player2Action();
		}

		if (placo.P3T == true && allowSwitch == true)
		{
			Player3Action();
		}

		if (placo.P4T == true)
		{
			Player4Action();
		}
		else thisRigid.useGravity = true;

		if (trafo.rotation.y != 0)
		{
			trafo.rotation = new Quaternion(0, 0, 0, 0);
		}

		if (trafo.position.z != 0)
		{
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

		preswitchPosition = childTrafo.position;
		preswitchRotation = childTrafo.rotation;
		preswitchSize = childTrafo.localScale;

		//determines which colour the shape is based on arrayToSwitchIn, 
		//then destroys the current game object before cycling through the array and spawning the next prefab in the cycle at the saved positions

		foreach (Transform shape in childTrafo.transform)
			Destroy(shape.gameObject);
		currentShape +=1;
		if (currentShape >= 3)
		{
			currentShape = 0;
		}
		i = currentShape;

		switch (currentColour) {
			case 1:
				if (i < 2)
				{
					newShape = (GameObject)Instantiate(shapeToBecome.bluePrefabs[i], preswitchPosition, preswitchRotation);
				}
				else newShape = (GameObject)Instantiate(shapeToBecome.bluePrefabs[i], preswitchPosition, Quaternion.Euler(preswitchRotation.x, preswitchRotation.y, preswitchRotation.z - 90f));
				break;
			case 2:
				if (i < 2)
				{
					newShape = (GameObject)Instantiate(shapeToBecome.greenPrefabs[i], preswitchPosition, preswitchRotation);
				}
				else newShape = (GameObject)Instantiate(shapeToBecome.greenPrefabs[i], preswitchPosition, Quaternion.Euler(preswitchRotation.x, preswitchRotation.y, preswitchRotation.z - 90f));
				break;
			case 3:
				if (i < 2)
				{
					newShape = (GameObject)Instantiate(shapeToBecome.redPrefabs[i], preswitchPosition, preswitchRotation);
				}
				else newShape = (GameObject)Instantiate(shapeToBecome.redPrefabs[i], preswitchPosition, Quaternion.Euler(preswitchRotation.x, preswitchRotation.y, preswitchRotation.z - 90f));
				break;
			case 4:
				if (i < 2)
				{
					newShape = (GameObject)Instantiate(shapeToBecome.yellowPrefabs[i], preswitchPosition, preswitchRotation);
				}
				else newShape = (GameObject)Instantiate(shapeToBecome.yellowPrefabs[i], preswitchPosition, Quaternion.Euler(preswitchRotation.x, preswitchRotation.y, preswitchRotation.z - 90f));
				break;
		}
		newShape.transform.localScale = preswitchSize*2;
		newShape.transform.parent = thisGameobject;
	}

	//contains action to be performed when player4 presses their button - FLOATs shapes upwards for as long as the button is held, then returns them slowly to the ground
	public void Player4Action() {
		thisRigid.useGravity = false;
		trafo.Translate(Vector3.up * 10 * Time.deltaTime);
	}


	//IEnumerator SmallPause() {
	//	yield return new WaitForSeconds(2f);
	//}

	private void OnTriggerEnter(Collider other)
	{

		if (other.tag == "Player") {
			allowPull = false;
		}

	}

	private void OnTriggerExit(Collider other)
	{

		if (other.tag == "Player")
		{
			allowPull = true;
		}
	}

}
