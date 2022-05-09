using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAxe : MonoBehaviour
{
    [SerializeField] internal float rotateSpeed = 1;
    [SerializeField] private GameObject tpEffect; 
    
    private GameObject player;
    private Rigidbody AxeRB;
    private int rotateDir;
    

    private void Start()
    {
        AxeRB = GetComponent<Rigidbody>();
    }

    public void Init(GameObject player)
    {
        this.player = player;
    }

    private void InitRotation(int rotateDir)
    {
        this.rotateDir = rotateDir;
    }

    private void FixedUpdate()
    {
        AxeRB.transform.Rotate(0,0,-rotateSpeed);
    }
    
    

    private void OnCollisionEnter()
    {   
        Instantiate(tpEffect,transform.position, Quaternion.identity);
        player.SendMessage("SwitchPositions", AxeRB);
    }
    

    // Update is called once per frame
    
}
