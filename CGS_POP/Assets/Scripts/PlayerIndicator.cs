using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIndicator : MonoBehaviour
{
	Material cubematerial;

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
		}

		if (Input.GetKey(KeyCode.Alpha2))
		{
			cubematerial.color = Color.green;
		}


		if (Input.GetKey(KeyCode.Alpha3))
		{
			cubematerial.color = Color.red;
		}


		if (Input.GetKey(KeyCode.Alpha4))
		{
			cubematerial.color = Color.yellow;
		}

	}
}
