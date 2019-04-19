using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCollisionChecker : MonoBehaviour
{
    //MeshFilter
    private MeshFilter meshFilter;

    //Growing Checks
    private GameObject _player3;
    public bool unlimitedGrowth = false;
    public bool squeezeX = false;
    public bool squeezeY = false;

    //Collision Lists
    private List<Collider> CollisionList = new List<Collider>();


    void Start()
    {
        _player3 = GameObject.Find("Player3");
        meshFilter = GetComponent<MeshFilter>();
    }

    void Update()
    {
        IsItBeingSqueezed();
    }

    #region Collisions

    void OnCollisionEnter(Collision col)
    {
        if (_player3.GetComponent<GamePlayer>().P3T)
        {
            if (col != null)
            {
                if (col.gameObject.tag == "Red" || col.gameObject.tag == "Blue" ||
                    col.gameObject.tag == "Green" || col.gameObject.tag == "Yellow")
                {
                    StartCoroutine(IgnoreShapeChangingCollision(col));
                }
            }
        }
    }

    #endregion

    #region Triggers

    void OnTriggerEnter(Collider other)
    {
         AddToList(other);
    }

    void OnTriggerExit(Collider other)
    {
         RemoveFromList(other);
    }

    #endregion

    #region ColliderFunctions
    
    void AddToList(Collider col_)
    {
        if (col_ != null)
        {
            CollisionList.Add(col_);
        }
    }

    void RemoveFromList(Collider col_)
    {
         CollisionList.Remove(col_);
    }

    #endregion

    #region Checks

    IEnumerator IgnoreShapeChangingCollision(Collision col)
    {
        Collider _collider = GetComponent<Collider>();
        Rigidbody _rigidbody = GetComponent<Rigidbody>();

        int i = 15;
        while (i > 0)
        {
            _rigidbody.Sleep();
            if (_rigidbody.velocity.y > 0)
            {
                _rigidbody.velocity = new Vector3(0f,
                Physics.gravity.y * Time.deltaTime, 0f);
            }
            else
            {
                _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
            }
            i--;
            yield return new WaitForEndOfFrame();
        }
    }

    void IsItBeingSqueezed()
    {
        bool Xleft = false;
        bool Xright = false;
        bool Yup = false;
        bool Ydown = false;

        for (int i = 0; i < CollisionList.Count; i++)
        {
            if (CollisionList[i] == null)
            {
                RemoveFromList(CollisionList[i]);
            }
            else
            {
                Vector3 closestPointVec3 = CollisionList[i].ClosestPoint(transform.position);
                
                if (closestPointVec3.x < (transform.position.x + meshFilter.mesh.bounds.min.x * transform.localScale.y))
                {
                    Xleft = true;
                }

                if (closestPointVec3.x > (transform.position.x + meshFilter.mesh.bounds.max.x * transform.localScale.y))
                {
                    Xright = true;
                }

                if (closestPointVec3.y < (transform.position.y + meshFilter.mesh.bounds.min.y * transform.localScale.x))
                {
                    Ydown = true;
                }

                if (closestPointVec3.y > (transform.position.y + meshFilter.mesh.bounds.max.y * transform.localScale.x))
                {
                    Yup = true;
                }
            }
        }

        if (!Xleft || !Xright)
        {
            squeezeX = false;
        }
        if (!Ydown || !Yup)
        {
            squeezeY = false;
        }

        if (Xleft && Xright)
        {
            squeezeX = true;
        }
        if (Ydown && Yup)
        {
            squeezeY = true;
        }
    }

    #endregion
}
