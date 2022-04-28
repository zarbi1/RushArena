using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBonus : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            other.gameObject.SendMessage("ResetDash");
            other.gameObject.SendMessage("FireballPickup");
            Destroy(gameObject);
        }
    }
}
