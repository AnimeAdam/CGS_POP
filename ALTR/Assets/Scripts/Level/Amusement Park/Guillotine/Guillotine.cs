using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guillotine : MonoBehaviour
{
    public Knife knife;
    public float RotateSpeed = 0.3f;
    public Menus menuInfo;

    void Start() {
        menuInfo = FindObjectOfType<Menus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!knife.TouchShapes && menuInfo.timeIsPaused == false)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        this.transform.Rotate(Vector3.up * RotateSpeed);
    }

}
