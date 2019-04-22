using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Animator ani;
    public ShakeTheScreen shake;
    public float DownDistance;

    void Start()
    {
        ani.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Green" || other.tag == "Red" || other.tag == "Blue" || other.tag == "Yellow")
        {
            transform.Translate(new Vector3(0, - DownDistance, 0) * Time.deltaTime * 10f);
            ani.enabled = true;
            if (shake != null)
            {
                shake.GettingAttackedShake();
            }
        }
    }
}
