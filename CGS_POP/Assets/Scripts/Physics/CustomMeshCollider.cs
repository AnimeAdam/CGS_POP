using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMeshCollider : MonoBehaviour
{
    public PhysicMaterial physicMaterial;
    public float MeshSizeIncrease = 1f;
    public int NumberOfMeshes = 1;
    public bool isTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        do
        {
            CreateNewMesh();
            --NumberOfMeshes;
        }
        while (NumberOfMeshes > 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateNewMesh()
    {
        Mesh _mesh = new Mesh();
        _mesh.vertices = GetComponent<MeshFilter>().mesh.vertices;
        _mesh.triangles = GetComponent<MeshFilter>().mesh.triangles;
        _mesh.uv = GetComponent<MeshFilter>().mesh.uv;
        _mesh.normals = GetComponent<MeshFilter>().mesh.normals;
        _mesh.colors = GetComponent<MeshFilter>().mesh.colors;
        _mesh.tangents = GetComponent<MeshFilter>().mesh.tangents;

        Vector3[] _vertices = _mesh.vertices;
        for (int i = 0; i < _vertices.Length; i++)
        {
            _vertices[i] *= MeshSizeIncrease;          //Might need scalar
        }
        _mesh.vertices = _vertices;

        MeshCollider _meshCollider = gameObject.AddComponent<MeshCollider>();
        _meshCollider.sharedMesh = _mesh;
        _meshCollider.sharedMaterial = physicMaterial;
        _meshCollider.convex = true;
        if (NumberOfMeshes != 1)
        {
            if (isTriggered)
            {
                _meshCollider.isTrigger = true;
            }
        }
    }
}
