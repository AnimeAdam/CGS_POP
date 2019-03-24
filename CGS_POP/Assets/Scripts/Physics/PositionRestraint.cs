using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionRestraint : MonoBehaviour
{
	Transform player;
	Vector3 resetPos;

    // Start is called before the first frame update
    void Start()
    {
		player = GetComponent<Transform>();
		resetPos = new Vector3(player.position.x, player.position.y, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.z != 0){
			player.transform.position = resetPos;
		}
    }
}
