using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyToUnlock : MonoBehaviour
{
    Unlock[] doorToUnlock;

    // Start is called before the first frame update
    void Start()
    {
        doorToUnlock = FindObjectsOfType<Unlock>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            foreach (Unlock lockedDoor in doorToUnlock)
            {
                lockedDoor.locked = false;
                Destroy(gameObject);
            }
        }
    }
}
