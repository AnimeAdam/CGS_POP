using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWins : MonoBehaviour
{
    private bool winner = true;
	public int flagHeight = 0;
	Transform flagCloth;
	Vector3 position0 = new Vector3(-1.3f, -1.3f, 0);
	Vector3 position1 = new Vector3(-1.3f, -1, 0);
	Vector3 position2 = new Vector3(-1.3f, -0.65f, 0);
	Vector3 position3 = new Vector3(-1.3f, -0.3f, 0);
	Vector3 position4 = new Vector3(-1.3f, 0, 0);
	AudioManager audiMan;

	bool flag1Sound = false;
	bool flag2Sound = false;
	bool flag3Sound = false;
	bool flag4Sound = false;

	// Start is called before the first frame update
	void Start()
    {
		flagCloth = this.gameObject.transform.GetChild(0);
		audiMan = FindObjectOfType<AudioManager>();

        ScenesManager.SetScenes(1);
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
		    //    if (winner)
			//	{
					if (flagHeight == 4)
					{
						//int playerNum = other.GetComponent<GamePlayer>().playerId + 1;
						//GameObject playerWinner = GameObject.Find("Canvas/PlayerWinner");
						Text playerText = GameObject.Find("Canvas/PlayerWinner").GetComponent<Text>();
						playerText.text = "Level Complete!";
					    StartCoroutine(GoToNextLevel());
					    //winner = false;
					}
			//  }   
			  

		if (other.gameObject.tag == "Player" && other is MeshCollider) {
			flagHeight += 1;
            Debug.Log("IT THIS MAY TIMES");
		}
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			flagHeight -= 1;
		}

		flag1Sound = false;
		flag2Sound = false;
		flag3Sound = false;
		flag4Sound = false;
	}

    IEnumerator GoToNextLevel()
    {
        yield return new WaitForSeconds(2f);
        ScenesManager.GoToNextScene(6);
    }

}
