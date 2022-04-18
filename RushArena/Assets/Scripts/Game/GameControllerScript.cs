using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
public class GameControllerScript : MonoBehaviour
{
    
    public Text GameStatus;
    public GameObject player;


    private Vector3 LocalPLayerPos;
    private Vector3 OtherPlayerPos;

    private bool isGameEnded = false;
    private bool Ended = false;

    public Timer timer;
    

    // Start is called before the first frame update
    public void Awake()
    {
        //wkaing up
        Scene scene = SceneManager.GetActiveScene();
        
        //start the timer
        
        //send a message
        StartCoroutine(DisplayMessage("Rush !"));
        
        timer.ManualWakeUp();
    }
    





    public void TimesUp()
    {
        isGameEnded = true;
    }

    // Update is called once per frame
    PhotonView view;
    void Update()
    {
        if (isGameEnded && !Ended) //check if the game has ended
        {
            Ended = true; //so that We have only one iteration
            StartCoroutine(DisplayMessage("Time's UP !"));
            //now check players position
            var players = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players);
            foreach(GameObject p in players)
            {
                //check if it's us
                PhotonView viewPLayer = p.GetComponent<PhotonView>();
                if (viewPLayer.IsMine)
                {
                    LocalPLayerPos = p.transform.position;
                }
                else
                {
                    OtherPlayerPos = p.transform.position;
                }
            }

            //now compare the two position and setup the win or lose
            if(LocalPLayerPos.x > OtherPlayerPos.x) //current player has won the game
            {
                DisplayMessage("You WON");
                SceneManager.LoadScene("Win Scene");
            }
            else //current player has lost the game
            {
                DisplayMessage("You Lost"); 
                SceneManager.LoadScene("GameOver Scene 1");
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
