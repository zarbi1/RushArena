using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDetector : MonoBehaviour
{
    public GameControllerScript _gameControllerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "EndGameBlock")
        {
            _gameControllerScript.TimesUp();
        }
    }
}
