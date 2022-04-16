using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAxe : MonoBehaviour
{
    private GameObject player;
    private Rigidbody AxeRB;

    private void Start()
    {
        AxeRB = GetComponent<Rigidbody>();
    }

    public void Init(GameObject player)
    {
        this.player = player;
    }

    private void OnCollisionEnter()
    {
        player.SendMessage("SwitchPositions",AxeRB);
    }


    // Update is called once per frame
    
}
