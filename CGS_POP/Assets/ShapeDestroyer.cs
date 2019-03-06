using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeDestroyer : MonoBehaviour
{
	ParticleSystem[] deathParticles = new ParticleSystem[12];
	int i;

    // Start is called before the first frame update
    void Start()
    {
		foreach (ParticleSystem deathParticle in deathParticles)
		{
			deathParticles[i] = GetComponent<ParticleSystem>();
			i++;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Blue")
		{

		}
		else if (other.gameObject.tag == "Green")
		{

		}
		else if (other.gameObject.tag == "Red")
		{

		}
		else if (other.gameObject.tag == "Yellow")
		{

		}
	}
}
