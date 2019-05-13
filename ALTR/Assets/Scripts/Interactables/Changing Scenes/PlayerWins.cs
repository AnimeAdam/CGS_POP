using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWins : MonoBehaviour
{
    //Audio Manager
    AudioManager audiMan;
    
    //Flag positions
    private int flagHeight = 0;
    private Vector3 position0 = new Vector3(-1.3f, -1.3f, 0);
    private Vector3 position1 = new Vector3(-1.3f, -1, 0);
    private Vector3 position2 = new Vector3(-1.3f, -0.65f, 0);
    private Vector3 position3 = new Vector3(-1.3f, -0.3f, 0);
    private Vector3 position4 = new Vector3(-1.3f, 0, 0);
    private bool flag1Sound = false;
    private bool flag2Sound = false;
    private bool flag3Sound = false;
    private bool flag4Sound = false;
    Transform flagCloth;

    //Scene Manager
    public bool defaultLevel = false;
    public bool exitGame = false;
    public int levelToGoTo;
    public bool menuLevel = false;
    private Menus menu;


	// Start is called before the first frame update
	void Start()
    {
		flagCloth = this.gameObject.transform.GetChild(0);
		audiMan = FindObjectOfType<AudioManager>();
        menu = FindObjectOfType<Menus>();
        ScenesManager.SetScenes(0);
    }

    // Update is called once per frame
    void Update()
    {
		switch (flagHeight) {

			case 0:
				flagCloth.localPosition = position0;
				break;
			case 1:
				if (flag1Sound == false)
				{
					audiMan.Flag_1.PlayOneShot(audiMan.Flag_1.clip);
					flag1Sound = true;
				}

				flagCloth.localPosition = position1;
				break;
			case 2:
				if (flag2Sound == false)
				{
					audiMan.Flag_2.PlayOneShot(audiMan.Flag_2.clip);
					flag2Sound = true;
				}
				flagCloth.localPosition = position2;
				break;
			case 3:
				if (flag3Sound == false)
				{
					audiMan.Flag_3.PlayOneShot(audiMan.Flag_3.clip);
					flag3Sound = true;
				}
				flagCloth.localPosition = position3;
				break;
			case 4:
				if (flag4Sound == false)
				{
					audiMan.Flag_4.PlayOneShot(audiMan.Flag_4.clip);
					flag4Sound = true;
				}
				flagCloth.localPosition = position4;
				break;
		}
    }

    void OnTriggerEnter(Collider other)
    {
		if (flagHeight == 4)
		{
            if (menuLevel == false)
            {
                var go = GameObject.Find("Canvas/PlayerWinner");
                if (go)
                {
                    Text playerText = go.GetComponent<Text>();
                    playerText.color = Color.cyan;
                    playerText.text = "Level Complete!";
                }
            }
		    if (exitGame)
		    {
		        Application.Quit();
		    }
		    else
		    {
		        if (!defaultLevel)
		        {
		            GoToNextLevel();
		        }
		        else
		        {
		            ScenesManager.GoToNextScene(levelToGoTo);
		        }
		    }
        }

		if (other.gameObject.tag == "Player" && other is MeshCollider)
		{
			flagHeight++;
		}
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player" && other is MeshCollider)
		{
			flagHeight--;
		}

		flag1Sound = false;
		flag2Sound = false;
		flag3Sound = false;
		flag4Sound = false;
	}

    void GoToNextLevel()
    {
        menu.OpenMenu(true, menu.levelSelectMenu);
        GamePlayer.menuOpenClose = false;
    }
}