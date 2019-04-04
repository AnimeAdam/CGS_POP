using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger : MonoBehaviour
{
    ParticleSystemRenderer _psm;
    public Material[] particleMaterials = new Material[3];
    int i = 0;

    // Start is called before the first frame update
    void Start()
    {
        _psm = GetComponent<ParticleSystemRenderer>();
        InvokeRepeating("ParticleMaterialChanger", 0.2f, 0.2f);    
    }

    void ParticleMaterialChanger() {
        if (i >= particleMaterials.Length) {
            i = 0;
        }
        _psm.material = particleMaterials[i];
        i++;
    }
}
