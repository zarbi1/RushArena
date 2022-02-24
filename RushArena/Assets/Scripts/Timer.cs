using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    public Text timerText;

    public int startTime = 10;

    public int currentTime = 0;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float t = startTime - Time.time;
        currentTime = (int) t;

        string minutes = ((int)t / 60).ToString();
        string seconds = ((int)t % 60).ToString();

        timerText.text = minutes + ":" + seconds;
    }
}
