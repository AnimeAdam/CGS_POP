using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyToUnlock : MonoBehaviour
{
    Unlock doorToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        doorToUnlock = FindObjectOfType<Unlock>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            doorToUnlock.locked = false;
            Destroy(gameObject);
        }
    }
}
