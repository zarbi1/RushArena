using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    
    [SerializeField]
    PlayerScript playerScript;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /**
     * <summary>checks if player is touching the floor</summary>
     */
    internal bool IsGrounded()
    {
        //si la sphère groundCheck entre en collision avec un objet ayant la layer ground alors True 
        return Physics.CheckSphere(playerScript.groundCheck.position, 0.1f, playerScript.ground);
    }
    
    
}
