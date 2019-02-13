using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;

public class ShakeTheScreen : MonoBehaviour
{
    private CameraShaker cameraShaker;
    
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraShaker = CameraShaker.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GettingAttackedShake()
    {
        //One Shake
        float magnitude = 2f;
        float roughness = 60f;
        float fadeInTime = 0f;
        float fadeOutTime = 0.5f;

        cameraShaker.ShakeOnce(magnitude, roughness, fadeInTime, fadeOutTime);
    }
}
