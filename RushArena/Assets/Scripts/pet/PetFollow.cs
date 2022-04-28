using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFollow : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1;

    [SerializeField] private Transform player;
    [SerializeField] private float MinDist = 10;
    [SerializeField] private float rotateSpeed = 1;
    
    private void FixedUpdate()
    {
        transform.LookAt(player);

        if (Vector3.Distance(transform.position,player.position) >= MinDist)
        {
            transform.position += transform.forward * moveSpeed;
        }
        else
        {
            transform.RotateAround(player.position,Vector3.up, rotateSpeed);
            transform.Rotate(Vector3.down, 90);
        }
        
    }
}
