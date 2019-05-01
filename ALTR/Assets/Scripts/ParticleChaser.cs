using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleChaser : MonoBehaviour
{
    Transform childTransform;
    Transform parentTransform;
    Vector3 position = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        parentTransform = GetComponent<Transform>();
        childTransform = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        position = childTransform.position;
        parentTransform.position = position;
    }
}
