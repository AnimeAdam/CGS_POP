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

	// Start is called before the first frame update
	void Start()
    {
		flagCloth = this.gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
		switch (flagHeight) {

			case 0:
				flagCloth.localPosition = position0;
				break;
			case 1:
				flagCloth.localPosition = position1;
				break;
			case 2:
				flagCloth.localPosition = position1;
				break;
			case 3:
				flagCloth.localPosition = position2;
				break;
			case 4:
				flagCloth.localPosition = position2;
				break;
			case 5:
				flagCloth.localPosition = position3;
				break;
			case 6:
				flagCloth.localPosition = position3;
				break;
			case 7:
				flagCloth.localPosition = position4;
				break;
			case 8:
				flagCloth.localPosition = position4;
				break;
		}
    }

    void OnTriggerEnter(Collider other)
    {
		/*        if (winner)
				{
					if (other.gameObject.tag == "Player")
					{
						int playerNum = other.GetComponent<GamePlayer>().playerId + 1;
						//GameObject playerWinner = GameObject.Find("Canvas/PlayerWinner");
						Text playerText = GameObject.Find("Canvas/PlayerWinner").GetComponent<Text>();
						playerText.text = "Player " + playerNum + " WINS";
						winner = false;
					}
			  }   
			  */

		if (other.gameObject.tag == "Player") {
			flagHeight += 1;

		}
    }

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player") {
			flagHeight -= 1;
		}
	}



}
