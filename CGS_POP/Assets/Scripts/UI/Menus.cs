﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    //Menus
    private GameObject debugLevelMenu;
    private GameObject mainMenu;

    //Set up Gamepad Interface
    private EventSystem eventSys;

    //Menu Buttons
    private UnityEngine.UI.Button[] debugLevelButtons;
    private UnityEngine.UI.Button[] mainMenuButtons;
    private UnityEngine.UI.Button[] highlightedMenu;
    private UnityEngine.UI.Button highlightedButton;
    private int currentButton;
    private int previousButton;


    void Awake()
    {
        SetObjects();
        HideMenus();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelDebugMenu();
    }

    #region MenuOpenActions

    void LevelDebugMenu()
    {
        if (Input.GetKeyDown(KeyCode.Home))
        {
            Debug.Log("Open Level Debug Menu");
            debugLevelMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.End))
        {
            Debug.Log("Close Level Debug Menu");
            debugLevelMenu.SetActive(false);
        }
    }

    public void OpenMainMenu(bool state)   //true = open; false = close
    {
        if (state)
        {
            mainMenu.SetActive(true);
            Time.timeScale = 0;
            eventSys.SetSelectedGameObject(mainMenuButtons[0].gameObject);
            SetCurrentButtonMenu(0, mainMenuButtons[0], mainMenuButtons);
        }
        else
        {
            ClearMenuButtons();
            mainMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    #endregion

    #region MenuButtonsActions

    //Debug Level Menu
    public void GoToScene(int _scene)
    {
        SceneManager.LoadScene(_scene);
    }

    //Main Menu
    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Options()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    #endregion
    
    #region SetObjects

    void SetObjects()
    {
        eventSys = EventSystem.current;

        debugLevelMenu = GameObject.Find("DebugMenu");
        mainMenu = GameObject.Find("MainMenu");

        mainMenuButtons = mainMenu.GetComponentsInChildren<UnityEngine.UI.Button>();
        debugLevelButtons = debugLevelMenu.GetComponentsInChildren<UnityEngine.UI.Button>();
    }

    void HideMenus()
    {
        debugLevelMenu.SetActive(false);
        mainMenu.SetActive(false);
    }

    void SetCurrentButtonMenu(int butti, UnityEngine.UI.Button butt, UnityEngine.UI.Button[] menu)
    {
        currentButton = butti;
        highlightedButton = butt;
        highlightedMenu = menu;
    }

    void ClearMenuButtons()
    {
        currentButton = 0;
        highlightedButton = null;
        highlightedMenu = null;
    }

    #endregion

    #region ButtonNavigation

    //Keep around incase something else doesn't work
    public void NavigateLeftRightButton(bool direction) //true = right false = left
    {
        Debug.Log("WHAT");
        if (direction)
        {
            previousButton = currentButton;
            if (currentButton != highlightedMenu.Length)
            {
                currentButton++;
                eventSys.SetSelectedGameObject(mainMenuButtons[currentButton].gameObject);
                highlightedButton = mainMenuButtons[currentButton];
            }
            else
            {
                currentButton = 0;
                eventSys.SetSelectedGameObject(mainMenuButtons[currentButton].gameObject);
                highlightedButton = mainMenuButtons[currentButton];
            }
        }
        else
        {
            previousButton = currentButton;
            if (currentButton != highlightedMenu.Length)
            {
                currentButton--;
                eventSys.SetSelectedGameObject(mainMenuButtons[currentButton].gameObject);
                highlightedButton = mainMenuButtons[currentButton];
            }
            else
            {
                currentButton = highlightedMenu.Length;
                eventSys.SetSelectedGameObject(mainMenuButtons[currentButton].gameObject);
                highlightedButton = mainMenuButtons[currentButton];
            }
        }
    }

    public void PressButton()
    {
        //highlightedButton.onClick.Invoke();
        eventSys.currentSelectedGameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
    }

    #endregion

}
