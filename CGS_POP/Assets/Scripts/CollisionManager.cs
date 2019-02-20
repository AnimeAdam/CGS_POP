using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
	public bool side1Check;
	public bool side2Check;
	public bool side3Check;
	public bool side4Check;

	ObjectController objeCon;

	// Start is called before the first frame update
	void Start()
    {
		objeCon = GetComponentInParent<ObjectController>();
    }

    // Update is called once per frame
    void Update()
    {
		if (side1Check)
		{
			objeCon.collider1Check = true;
		}
		else objeCon.collider1Check = false;

		if (side2Check)
		{
			objeCon.collider2Check = true;
		}
		else objeCon.collider2Check = false;

		if (side3Check)
		{
			objeCon.collider3Check = true;
		}
		else objeCon.collider3Check = false;

		if (side4Check)
		{
			objeCon.collider4Check = true;
		}
		else objeCon.collider4Check = false;
	}
}
