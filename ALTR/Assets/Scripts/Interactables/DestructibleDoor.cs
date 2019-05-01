using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleDoor : MonoBehaviour
{
    public bool unlockDoor = false;
    public ParticleSystem unlockParticle;
    AudioManager audiMan;

    void Start() {
        audiMan = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (unlockDoor == true) {
            Instantiate(unlockParticle, transform.position, Quaternion.Euler(0, 0, 0));
            audiMan.DoorUnlock.Play();
            Destroy(gameObject);
        }
    }

}

