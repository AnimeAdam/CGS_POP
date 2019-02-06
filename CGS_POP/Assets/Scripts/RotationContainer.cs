using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationContainer : MonoBehaviour
{
	private Transform shapeTransform;

    // Start is called before the first frame update
    void Start()
    {
		shapeTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if (shapeTransform.rotation.y != 0)
		{
			shapeTransform.rotation = new Quaternion(0, 0, 0, 0);
		}

		if (shapeTransform.position.z != 0)
		{
			shapeTransform.position = new Vector3(shapeTransform.position.x, shapeTransform.position.y, 0);
		}
	}
}
