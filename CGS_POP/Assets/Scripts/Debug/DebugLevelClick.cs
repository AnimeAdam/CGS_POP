using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DebugLevelClick: MonoBehaviour
{

    public void GoToScene(int _scene)
    {
        SceneManager.LoadScene(_scene);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Home))
        //{
        //    Debug.Log("You are debugging :)");
        //    gameObject.SetActive(true);
        //}
        //if (Input.GetKeyDown(KeyCode.End))
        //{
        //    Debug.Log("You are debugging :)");
        //    gameObject.SetActive(false);
        //}
    }
}
