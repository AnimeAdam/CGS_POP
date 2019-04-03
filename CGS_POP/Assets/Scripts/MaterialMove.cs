using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialMove : MonoBehaviour
{
    public float moveX = 0.5f;
    public float moveY = 0f;

    // Update is called once per frame
    void Update()
    {
        float movementX = moveX * Time.time;
        float movementY = moveY * Time.time;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(movementX, movementY);
    }
}
