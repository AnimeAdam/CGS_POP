using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUI : MonoBehaviour
{
    GameObject _debugMenu;

    // Start is called before the first frame update
    void Start()
    {
        _debugMenu = GameObject.Find("DebugMenu");
        _debugMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            Debug.Log("You are debugging :)");
            _debugMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            Debug.Log("You are debugging :)");
            _debugMenu.SetActive(false);
        }
    }
}
