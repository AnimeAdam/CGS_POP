using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unlock : MonoBehaviour
{
    public bool locked = true;
    public ParticleSystem unlockParticle;
    public AudioManager audiMan;

    // Start is called before the first frame update
    void Start()
    {
        audiMan = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (locked == false) {
            Instantiate(unlockParticle, transform.position, Quaternion.Euler(0, 0, 0));
            audiMan.DoorUnlock.Play();
            Destroy(gameObject);
        }
    }
}
