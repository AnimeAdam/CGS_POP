using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSmasher : MonoBehaviour
{
    public ParticleSystem shardParticles;
    bool particleSpawned = false;
    AudioManager audiMan;

    // Start is called before the first frame update
    void Awake()
    {
        audiMan = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if(particleSpawned == false) { 
        string shapeName = other.name;
            if (shapeName.Contains("Triangle"))
            {
                Instantiate(shardParticles, this.transform.position, Quaternion.Euler(0, 0, 0));
                audiMan.GlassSmash.Play();
                particleSpawned = true;
                Destroy(gameObject);
            }
        }
    }
}
