using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;

    private int startTime;

    public GameControllerScript gameController;
    private bool launch = false;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void ManualWakeUp()
    {
        startTime = 10;   //strat time in sec
        launch = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (launch)
        {
            float t = startTime - Time.time;

            if (t <= 0)
            {

                gameController.TimesUp();    //call the GameController EndGame
                return; //end the script here the game is over
            }
            string minutes = ((int)t / 60).ToString();
            string seconds = ((int)t % 60).ToString();

            timerText.text = minutes + ":" + seconds;
        }
       
        
    }
}
