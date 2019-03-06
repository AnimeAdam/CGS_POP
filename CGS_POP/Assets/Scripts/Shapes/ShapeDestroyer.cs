using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShapeDestroyer : MonoBehaviour
{
	public Material[] deathMaterials = new Material[12];
	ParticleSystemRenderer ps;
	int i;
	Transform _ts;
	ParticleSystem _partiSys;
	ShapeManager shaMan;
	Transform[] shaTra;

	// Start is called before the first frame update
	void Start()
    {
		foreach (Material deathMaterial in deathMaterials)
		{
			deathMaterials[i] = GetComponent<Material>();
			i++;
		}
		ps = GetComponentInChildren<ParticleSystemRenderer>();
		shaMan = FindObjectOfType<ShapeManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		string _name = other.gameObject.name;
		Vector3 _pos = other.gameObject.transform.position;

		shaTra = shaMan.GetShapesList();

		if (_name.Contains("Circle"))
		{
			ParticleSystemRenderer _gb;
//			_gb = PrefabUtility.InstantiatePrefab(ParticleSystem ps) as ParticleSystem;
			ps.material = deathMaterials[1];
		}
		if (_name.Contains("Square"))
		{
			ParticleSystemRenderer _gb;
			_gb = Instantiate(ps, _pos, transform.rotation, transform);
			ps.material = deathMaterials[4];
		}
		if (_name.Contains("Triangle"))
		{
			ParticleSystemRenderer _gb;
			_gb = Instantiate(ps, _pos, transform.rotation, transform);
			ps.material = deathMaterials[7];
		}
		Destroy(other.gameObject);
	}

}
