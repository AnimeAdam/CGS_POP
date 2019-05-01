using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skywheel : MonoBehaviour
{
    public float RotateSpeed = 0.3f;
    public float ShapesNeedToStayTime = 2f;

    public GameObject Glass;
    public Basket[] Baskets;
    [HideInInspector] public Menus menuInfo;

    void Start() {
        menuInfo = FindObjectOfType<Menus>();
    }

    private void FixedUpdate()
    {
        if (Count() == 4)
        {
            Invoke("ReCheck", ShapesNeedToStayTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (menuInfo.timeIsPaused == false)
        {
            Rotate();
        }
    }

    private void Rotate()
    {
        this.transform.Rotate(Vector3.down * RotateSpeed);
    }

    //Conut how many shapes in basket
    private int Count()
    {
        int Num = 0;
        for (int i = 0; i < Baskets.Length; i++)
        {
            if (Baskets[i].ShapeOnME)
            {
                Num++;
            }
        }
        return Num;
    }

    //Make Sure all shapes stay in basket at list 2 sec(ShapesNeedToStayTime)
    private void ReCheck()
    {
        //if (Count() == 4)
        //{
        //    Glass.SetActive(false);
        //    //maybe some audio here
        //}
    }
}
