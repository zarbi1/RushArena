using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameController : MonoBehaviour
{

    public GameOverScreen gameOverScreen; //when lose = called
    public Text GameStatus;

    Timer timer = new Timer();
    public GameObject player;
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

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timer.currentTime);
        if (timer.currentTime == 0)
        {
            DisplayMessage("Time's UP !");
        }
    }

    //display message
    IEnumerator DisplayMessage(string message)
    {
        GameStatus.text = message;
        yield return new WaitForSeconds(5);
        GameStatus.text = "";
    }
}
