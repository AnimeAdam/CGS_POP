using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWins : MonoBehaviour
{
    private bool winner = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (winner)
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
    }
}
