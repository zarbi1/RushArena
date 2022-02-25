using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControllerScript : MonoBehaviour
{

    public GameOverScreen gameOverScreen; //when lose = called
    public Text GameStatus;
    public GameObject player;


    private bool isGameEnded = false;
    private bool Ended = false;

    // Start is called before the first frame update
    public void Start()
    {
        //send a message
        StartCoroutine(DisplayMessage("Rush !"));
    }

    public void GameOver(int playerID)
    {
        gameOverScreen.Setup(playerID);
    }




    public void TimesUp()
    {
        isGameEnded = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameEnded && !Ended) //check if the game has ended
        {
            Ended = true; //so that We have only one iteration
            StartCoroutine(DisplayMessage("Time's UP !"));
            //now check players position
            var players = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players);
            foreach(var p in players)
            {
                Debug.Log(p.transform.position); //all players coordinatesn here the game is stoped so we can compare the current player positionto the other one.
            }
        }
    }


    //called when time is up
   

    //display message
    IEnumerator DisplayMessage(string message)
    {
        GameStatus.text = message;
        yield return new WaitForSeconds(5);
        GameStatus.text = "";
    }
}
