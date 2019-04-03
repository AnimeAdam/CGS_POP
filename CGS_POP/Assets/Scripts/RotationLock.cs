using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLock : MonoBehaviour
{
    Vector3 lockedRot = new Vector3(-90, 0, 0);
    Transform particleTransform;
    // Start is called before the first frame update
    void Start()
    {
        particleTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        particleTransform.position = lockedRot;
    }
}
