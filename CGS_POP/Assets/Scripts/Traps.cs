using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    public Animator ani;
 
       
  
    //NSHAN
    // Start is called before the first frame update
    void Start()
    {
        ani.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {

    }
    //plays animation when collider is hit
   public void OnTriggerEnter()
    {
        ani.enabled = true;
        
    }
}
