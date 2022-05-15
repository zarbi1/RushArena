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
    private float deltatTime;
    private int min = 0;
    private double sec = 10;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void ManualWakeUp()
    {
        startTime = 30;   //strat time in sec
        
        Debug.Log("Started !");
        launch = true;

        StartCoroutine(LaunchTimer());

    }

    // Update is called once per frame
    IEnumerator LaunchTimer()
    {
        if (launch)
        {
            /*
            deltatTime = startTime - Time.time;

            if (deltatTime <= 0)
            {

                gameController.TimesUp();    //call the GameController EndGame
                yield return null;
            }
            string minutes = ((int)deltatTime / 60).ToString();
            string seconds = ((int)deltatTime % 60).ToString();

            timerText.text = minutes + ":" + seconds;
            
            Debug.Log("Here");
            */
            
            timerText.text = min + ":" + sec;
            yield return new WaitForSeconds(1.5f);

            sec--;
            if (sec <= 0)
            {
                if (min == 0)
                {
                    gameController.TimesUp();
                    yield return null;
                }
                sec = 60;
                min--;
            }

            StartCoroutine(LaunchTimer());

        }
       
        
    }
}
