﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker3 : MonoBehaviour
{
	CollisionManager colMan;

	// Start is called before the first frame update
	void Start()
	{
		colMan = GetComponentInParent<CollisionManager>();
	}


	// Update is called once per frame
	void Update()
    {
        
    }


	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Wall") {
			colMan.side3Check = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Wall")
		{
			colMan.side3Check = false;
		}
	}

}
