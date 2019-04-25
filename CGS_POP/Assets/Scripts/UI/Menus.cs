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
    [HideInInspector] public GameObject debugLevelMenu;
    [HideInInspector] public GameObject mainMenu;
    [HideInInspector] public GameObject levelSelectMenu;

    //Set up Gamepad Interface
    private EventSystem eventSys;

    //Menu Buttons
    [HideInInspector] public UnityEngine.UI.Button[] highlightedMenu;
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

    public void OpenMenu(bool state, GameObject menu)   //true = open; false = close
    {
        if (state)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
            eventSys.SetSelectedGameObject(menu);
            SetCurrentButtonMenu(0, menu.GetComponentsInChildren<UnityEngine.UI.Button>()[0],
                menu.GetComponentsInChildren<UnityEngine.UI.Button>());
            audiMan.TestMusic.Stop();
            startTimer = false;
        }
        else
        {
            audiMan.TestMusic.Play();
            ClearMenuButtons();
            menu.SetActive(false);
            startTimer = true;
        }
    }

    #endregion

    #region MenuButtonsActions

    //Level Select Menu
    public void GoToScene(int _scene)
    {
        ClearMenuButtons();
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

    public void ExitSoftware()
    {
        ClearMenuButtons();
        Application.Quit();
    }

    #endregion

    #region SetObjects

    void SetObjects()
    {
        eventSys = EventSystem.current;

        debugLevelMenu = GameObject.Find("DebugMenu");
        mainMenu = GameObject.Find("MainMenu");
        levelSelectMenu = GameObject.Find("LevelSelect");
    }

    void HideMenus()
    {
        debugLevelMenu.SetActive(false);
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(false);
    }

    void SetCurrentButtonMenu(int butti, UnityEngine.UI.Button butt, UnityEngine.UI.Button[] menu)
    {
        currentButton = butti;
        highlightedButton = butt;
        highlightedMenu = menu;
        eventSys.SetSelectedGameObject(menu[currentButton].gameObject);
    }

    void ClearMenuButtons()
    {
        currentButton = 0;
        highlightedButton = null;
        highlightedMenu = null;
        Time.timeScale = 1;
        GamePlayer.menuOpenClose = true;
        audiMan.TestMusic.Play();
    }

    #endregion

    #region ButtonNavigation

    public void NavigateLeftRightButton(bool direction, UnityEngine.UI.Button[] menu) //true = right false = left
    {
        audiMan.MenuSound.Play();

        if (direction)
        {
            previousButton = currentButton;
            if (currentButton != highlightedMenu.Length-1)
            {
                currentButton++;
                eventSys.SetSelectedGameObject(menu[currentButton].gameObject);
                highlightedButton = menu[currentButton];
            }
            else
            {
                currentButton = 0;
                eventSys.SetSelectedGameObject(menu[currentButton].gameObject);
                highlightedButton = menu[currentButton];
            }
        }
        else
        {
            previousButton = currentButton;
            if (currentButton != 0)
            {
                currentButton--;
                eventSys.SetSelectedGameObject(menu[currentButton].gameObject);
                highlightedButton = menu[currentButton];
            }
            else
            {
                currentButton = highlightedMenu.Length-1;
                eventSys.SetSelectedGameObject(menu[currentButton].gameObject);
                highlightedButton = menu[currentButton];
            }
        }

        if (mainMenu.activeSelf)
        {
            mainMenu.GetComponent<Image>().sprite = menuSprites[currentButton];
        }
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
