using System;
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
    public Sprite[] menuSprites = new Sprite[3];

    // Audio Management
    AudioManager audiMan;

    //Timer
    private int minutesPassed = 0;
    private float realTimeSeconds = 0f;
    private Text timer;
    private bool secondsMin = true;
    private bool startTimer = true; //Start and stop timer

    void Awake()
    {
        SetObjects();
        HideMenus();
        audiMan = FindObjectOfType<AudioManager>();
        timer = GameObject.Find("Timer").GetComponent<Text>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelDebugMenu();
        if (startTimer)
        {
            PassingOfTime();
        }
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
            audiMan.TestMusic.Stop();
            startTimer = false;
        }
        else
        {
            audiMan.TestMusic.Play();
            ClearMenuButtons();
            mainMenu.SetActive(false);
            startTimer = true;
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
        ClearMenuButtons();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Options()
    {
        ClearMenuButtons();
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        ClearMenuButtons();
        SceneManager.LoadScene(0);
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
        Time.timeScale = 1;
        //FindObjectOfType<GamePlayer>().
        GamePlayer.menuOpenClose = true;
        audiMan.TestMusic.Play();
    }

    #endregion

    #region ButtonNavigation

    //Keep around incase something else doesn't work
    public void NavigateLeftRightButton(bool direction) //true = right false = left
    {
        audiMan.MenuSound.Play();

        if (direction)
        {
            previousButton = currentButton;
            if (currentButton != highlightedMenu.Length-1)
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
            if (currentButton != 0)
            {
                currentButton--;
                eventSys.SetSelectedGameObject(mainMenuButtons[currentButton].gameObject);
                highlightedButton = mainMenuButtons[currentButton];
            }
            else
            {
                currentButton = highlightedMenu.Length-1;
                eventSys.SetSelectedGameObject(mainMenuButtons[currentButton].gameObject);
                highlightedButton = mainMenuButtons[currentButton];
            }
        }

        mainMenu.GetComponent<Image>().sprite = menuSprites[currentButton];
    }

    public void PressButton()
    {
        //highlightedButton.onClick.Invoke();
        if (eventSys.currentSelectedGameObject != null)
        {
            eventSys.currentSelectedGameObject.GetComponent<UnityEngine.UI.Button>().onClick.Invoke();
        }
    }

    #endregion

    #region TimerFunctions

    void PassingOfTime()
    {
        realTimeSeconds += Time.unscaledDeltaTime;

        if (Mathf.Round(realTimeSeconds) % 60 == 0 && secondsMin)
        {
           secondsMin = false;
           minutesPassed++;
		   realTimeSeconds = 0f;
           timer.text = ("Time: " + minutesPassed + ":00");
        }
        else if(!(Mathf.Round(realTimeSeconds) % 60 == 0))
        {
            if (!secondsMin)
            {
                secondsMin = true;
            }
            if (Mathf.Round(realTimeSeconds) < 10)
            {
                timer.text = ("Time: " + minutesPassed + ":0" + Mathf.Round(realTimeSeconds));
            }
            else
            {
                timer.text = ("Time: " + minutesPassed + ":" + Mathf.Round(realTimeSeconds));
            }
        }
    }

    #endregion

}
