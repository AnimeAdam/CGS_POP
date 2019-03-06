using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public static class ScenesManager
{
    public static int defaultLevel = 4;

    private static int previousScene = 0;
    private static int currentScene = 0;
    private static int nextScene = 0;
    private static Dictionary<int, string> allScenes = new Dictionary<int, string>();

    static public void SetScenes(int _next)
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        nextScene = currentScene + _next;
    }

    static public void SetPreviousScene()
    {
        previousScene = currentScene;
    }
    
    static public void GoToNextScene(int _scene)
    {
        if (currentScene == 0 || nextScene == 0)
        {
            Debug.Log("SOMETHING HAS GONE WRONG WITH GOTO NEXT SCENE");
        }
        else
        {
            if (_scene != 0)
            {
                SetPreviousScene();
                SceneManager.LoadScene(nextScene);
            }
            else
            {
                SetPreviousScene();
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
