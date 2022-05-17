using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHitbox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.SendMessage("GoToCheckPoint");
        }
    }
}
