using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate2: MonoBehaviour
{
    public int speed = 5;
    Vector3 movement = new Vector3(0, 0, 1);


    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(movement * speed);
    }
}
