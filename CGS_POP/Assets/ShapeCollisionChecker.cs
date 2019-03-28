using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeCollisionChecker : MonoBehaviour
{
    //public Collider box1;
    //public Collider box2;
    //public Collider box3;
    //public Collider box4;

    /*public bool stopGrowth = false;

    private List<Collider> CollisionList = new List<Collider>();
    private List<Vector3> CollisionPoints = new List<Vector3>();

    #region Collision Triggers


    void OnTriggerEnter(Collider other)
    {
        CollisionList.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        foreach (Collider _other in CollisionList)
        {
            if (_other.name == other.name)
            {
                CollisionList.Remove(_other);
                break;
            }
        }
    }*/

    /*void bool IsItBeingSqueezed()
    {
        foreach (Collider _other in CollisionList)
        {
            CollisionPoints.Add(_other.ClosestPoint(transform.position));
        }

        int i = 0;
        foreach (Vector3 _vec3 in CollisionPoints)
        {
            i++;
            int j = 0;
            foreach (Vector3 __vec3 in CollisionPoints)
            {
                j++;
                if ((_vec3.y < __vec3.y && __vec3.y < _vec3.y) && ()        //FIND THE COLLIDERS NAME 
                {
                    return true;
                }
            }
        }
        return false;
    }*/


    //#endregion


}
