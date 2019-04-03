using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShapeDestroyer : MonoBehaviour
{
	public ParticleSystem[] deathParticles = new ParticleSystem[12];
	ParticleSystemRenderer ps;
	int i;
	Transform _ts;
	ParticleSystem _partiSys;
	ShapeManager shaMan;
	Transform[] shaTra;
	AudioManager audiMan;

	// Start is called before the first frame update
	void Start()
    {
		ps = GetComponentInChildren<ParticleSystemRenderer>();
		shaMan = FindObjectOfType<ShapeManager>();
		_partiSys = GetComponent<ParticleSystem>();
		audiMan = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		string _name = other.gameObject.name;
		Vector3 _pos = other.gameObject.transform.position;
		Vector3 _sca = other.gameObject.transform.localScale;
		float scaleCheck = (other.gameObject.transform.localScale.x + other.gameObject.transform.localScale.y + other.gameObject.transform.localScale.z) / 3;

		if (other.gameObject.tag == "Blue")
		{
			if (_name.Contains("Circle"))
			{
				Instantiate(deathParticles[0], _pos, Quaternion.Euler(0,0,0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[0], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[0], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[0], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10) {
					Instantiate(deathParticles[0], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[0], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[0], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[0], _pos, Quaternion.Euler(0, 0, 0));
				}
				
			}
			if (_name.Contains("Square"))
			{
				Instantiate(deathParticles[1], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[1], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[1], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[1], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[1], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[1], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[1], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[1], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
			if (_name.Contains("Triangle"))
			{
				Instantiate(deathParticles[2], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[2], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[2], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[2], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[2], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[2], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[2], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[2], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
            audiMan.Circular_Saw.Play();
            Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Green")
		{
			if (_name.Contains("Circle"))
			{
				Instantiate(deathParticles[3], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[3], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[3], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[3], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[3], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[3], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[3], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[3], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
			if (_name.Contains("Square"))
			{
				Instantiate(deathParticles[4], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[4], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[4], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[4], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[4], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[4], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[4], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[4], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
			if (_name.Contains("Triangle"))
			{
				Instantiate(deathParticles[5], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[5], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[5], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[5], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[5], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[5], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[5], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[5], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
            audiMan.Circular_Saw.Play();
            Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Red")
		{
			if (_name.Contains("Circle"))
			{
				Instantiate(deathParticles[6], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[6], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[6], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[6], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[6], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[6], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[6], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[6], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
			if (_name.Contains("Square"))
			{
				Instantiate(deathParticles[7], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[7], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[7], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[7], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[7], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[7], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[7], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[7], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
			if (_name.Contains("Triangle"))
			{
				Instantiate(deathParticles[8], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[8], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[8], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[8], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[8], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[8], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[8], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[8], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
            audiMan.Circular_Saw.Play();
            Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Yellow")
		{
			if (_name.Contains("Circle"))
			{
				Instantiate(deathParticles[9], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[9], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[9], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[9], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[9], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[9], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[9], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[9], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
			if (_name.Contains("Square"))
			{
				Instantiate(deathParticles[10], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[10], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[10], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[10], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[10], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[10], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[10], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[10], _pos, Quaternion.Euler(0, 0, 0));
				}
			}
			if (_name.Contains("Triangle"))
			{
				Instantiate(deathParticles[11], _pos, Quaternion.Euler(0, 0, 0));
				if (scaleCheck >= 3 && scaleCheck < 6)
				{
					Instantiate(deathParticles[11], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck >= 6 && scaleCheck < 10)
				{
					Instantiate(deathParticles[11], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[11], _pos, Quaternion.Euler(0, 0, 0));
				}
				else if (scaleCheck > 10)
				{
					Instantiate(deathParticles[11], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[11], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[11], _pos, Quaternion.Euler(0, 0, 0));
					Instantiate(deathParticles[11], _pos, Quaternion.Euler(0, 0, 0));
				}

				//ParticleSystemRenderer _gb;
				//_gb = Instantiate(ps, _pos, transform.rotation, transform);
				//ps.material = deathMaterials[12];
			}
            audiMan.Circular_Saw.Play();
            Destroy(other.gameObject);
		}
		shaTra = shaMan.GetShapesList();
	}

}
