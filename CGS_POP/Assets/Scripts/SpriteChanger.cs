using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    ParticleSystemRenderer _psm;
    Material _parMat1, _parMat2, _parMat3;

    // Start is called before the first frame update
    void Start()
    {
        _psm = GetComponent<ParticleSystemRenderer>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
