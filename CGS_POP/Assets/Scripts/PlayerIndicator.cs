using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
	Material cubematerial;

	public bool player1Selected = true;
	public bool player2Selected = false;
	public bool player3Selected = false;
	public bool player4Selected = false;

	// Start is called before the first frame update
	void Start()
    {
		cubematerial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.Alpha1))
		{
			cubematerial.color = Color.blue;
		player1Selected = true;
		player2Selected = false;
		player3Selected = false;
		player4Selected = false;
		}


		if (Input.GetKey(KeyCode.Alpha2))
		{
			cubematerial.color = Color.green;
			player1Selected = false;
			player2Selected = true;
			player3Selected = false;
			player4Selected = false;
		}


		if (Input.GetKey(KeyCode.Alpha3))
		{
			cubematerial.color = Color.red;
			player1Selected = false;
			player2Selected = false;
			player3Selected = true;
			player4Selected = false;
		}


		if (Input.GetKey(KeyCode.Alpha4))
		{
			cubematerial.color = Color.yellow;
			player1Selected = false;
			player2Selected = false;
			player3Selected = false;
			player4Selected = true;
		}

	}
}
