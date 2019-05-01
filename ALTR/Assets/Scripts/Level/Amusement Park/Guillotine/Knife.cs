using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public bool TouchShapes = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<GamePlayer>().playerHealth-- ;
        }
        if (other.tag == "Green" || other.tag == "Red" || other.tag == "Blue" || other.tag == "Yellow")
        {
            TouchShapes = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Green" || other.tag == "Red" || other.tag == "Blue" || other.tag == "Yellow")
        {
            TouchShapes = false;
        }
    }
}
