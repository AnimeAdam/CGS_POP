using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriangle : MonoBehaviour
{
	//Goes on the triangle element of the ShapeManager prefab. Upon contact with a player, the player is killed because of how darned spiky triangles are

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") {
			other.gameObject.GetComponent<GamePlayer>().playerHealth -= 1;
		}
	}
}
