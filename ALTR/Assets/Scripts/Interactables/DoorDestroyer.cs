using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDestroyer : MonoBehaviour
{
    public DestructibleDoor desDor;
    public ParticleSystem unlockParticle;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Red" || other.tag == "Blue" || other.tag == "Green" || other.tag == "Yellow")
        {
            desDor.unlockDoor = true;
            Instantiate(unlockParticle, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

}
