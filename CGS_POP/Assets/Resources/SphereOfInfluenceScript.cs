using System.Collections;
using System.Collections.Generic;
using Rewired.UI.ControlMapper;
using UnityEngine;

public class SphereOfInfluenceScript : MonoBehaviour
{
    private int timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if (timer == 2)
        {
            Destroy(this.gameObject);
        }
    }
}
