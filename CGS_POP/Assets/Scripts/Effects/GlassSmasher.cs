using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassSmasher : MonoBehaviour
{
    public ParticleSystem shardParticles;
    bool particleSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
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
                particleSpawned = true;
                Destroy(gameObject);
            }
        }
    }
}
