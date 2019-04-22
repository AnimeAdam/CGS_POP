using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [HideInInspector] public bool ShapeOnME = false;

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Green"|| other.tag == "Red"|| other.tag == "Blue"|| other.tag == "Yellow")
        {
            ShapeOnME = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Green" || other.tag == "Red" || other.tag == "Blue" || other.tag == "Yellow")
        {
            ShapeOnME = false;
        }
    }
}
