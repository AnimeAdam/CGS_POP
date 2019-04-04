using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCollisionChecker : MonoBehaviour
{
    //public Collider box1;
    //public Collider box2;
    //public Collider box3;
    //public Collider box4;

    private GameObject _player3;

    public float growthLimit = 0f;
    public bool stopGrowth = false;
    public bool growthX = false;
    public bool growthY = false;

    private List<Collider> CollisionList = new List<Collider>();

    void Start()
    {
        _player3 = GameObject.Find("Player3");
    }

    #region Collision Triggers

    void Update()
    {
        if (growthLimit > 0)
        {
            if (transform.localScale.x >= growthLimit)
            {
                stopGrowth = true;
            }
        }
    }

    #region Triggers
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Red" && other.tag != "Green" && other.tag != "Blue" && other.tag != "Yellow")
        {
            CollisionList.Add(other);
        }

        IsItBeingSqueezed();
    }

    void OnTriggerExit(Collider other)
    {
        if (ClearCollider(other))
        {
            IsItFree();
            return;
        }
        if (other.tag != "Red" && other.tag != "Green" && other.tag != "Blue" && other.tag != "Yellow")
        {
            foreach (Collider _other in CollisionList)
            {
                if (ClearCollider(_other))
                {
                    IsItFree();
                    continue;
                }
                if (_other.name == other.name)
                {
                    CollisionList.Remove(_other);
                    break;
                }
            }
        }
        IsItFree();
    }

    #endregion

    #region Collision
    
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
                _rigidbody.velocity = new Vector3(0f, Physics.gravity.y*Time.deltaTime, 0f);
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
        for (int i = 0; i < CollisionList.Count; i++)
        {
            if (ClearCollider(CollisionList[i]))
            {
                continue;
            }
            for (int j = i + 1; j < CollisionList.Count; j++)
            {
                if (ClearCollider(CollisionList[j]))
                {
                    continue;
                }
                Vector3 closestPoint1Vec3 = CollisionList[i].ClosestPoint(transform.position);
                Vector3 closestPoint2Vec3 = CollisionList[j].ClosestPoint(transform.position);

                Vector3 closestPointBound1Vec3 = CollisionList[i].ClosestPointOnBounds(transform.position);
                Vector3 closestPointBound2Vec3 = CollisionList[j].ClosestPointOnBounds(transform.position);

                //Debug Code
                /*if (name == "Circle")
                {
                    Debug.Log(CollisionList[i].name + closestPoint1Vec3);
                    Debug.DrawLine(transform.position, closestPoint1Vec3, Color.blue);
                    Debug.Log(CollisionList[j].name + closestPoint2Vec3);
                    Debug.DrawLine(transform.position, closestPoint2Vec3, Color.cyan);
                    Debug.Break();
                    Debug.Log(CollisionList[i].name + closestPointBound1Vec3);
                    Debug.Log(CollisionList[j].name + closestPointBound1Vec3);
                }*/

                if((closestPoint1Vec3.y < closestPoint2Vec3.y) || (closestPoint1Vec3.y > closestPoint2Vec3.y))
                {
                    growthX = true;
                    break;
                }

                if ((closestPoint1Vec3.x < closestPoint2Vec3.x) || (closestPoint1Vec3.x > closestPoint2Vec3.x))
                {
                    growthY = true;
                    break;
                }

                if (((closestPoint1Vec3.y < closestPoint2Vec3.y) || (closestPoint1Vec3.y > closestPoint2Vec3.y)) &&
                    ((closestPoint1Vec3.x < closestPoint2Vec3.x) || (closestPoint1Vec3.x > closestPoint2Vec3.x)))
                {
                    stopGrowth = true;
                }
            }
        }
    }

    void IsItFree()
    {
        if (CollisionList.Count <= 1)
        {
            stopGrowth = false;
        }
    }

    bool ClearCollider(Collider _col)
    {
        if (_col == null)
        {
            CollisionList.Remove(_col);
            return true;
        }

        return false;
    }

    #endregion
}
