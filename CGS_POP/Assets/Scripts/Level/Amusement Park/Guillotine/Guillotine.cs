using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : MonoBehaviour
{
    public Knife knife;
    public float RotateSpeed = 0.3f;

    // Update is called once per frame
    void Update()
    {
        if (!knife.TouchShapes)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        this.transform.Rotate(Vector3.up * RotateSpeed);
    }

}
