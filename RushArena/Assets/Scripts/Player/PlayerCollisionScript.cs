using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionScript : MonoBehaviour
{
    
    [SerializeField]
    PlayerScript PS;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(3,7);
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
        return Physics.CheckSphere(PS.groundCheck.position, 0.1f, PS.ground);
    }

    internal bool IsTouchingWall()
    {
        Vector3 checkPos = PS.movementScript.facingRight ? PS.RightCheck.position : PS.LeftCheck.position;
        return Physics.CheckSphere(checkPos, 0.1f, PS.ground);
    }
    
    
    /**
     * <summary>checks if player is on flat terrain</summary>
     */
    internal float FloorAngle()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.5f, PS.ground))
        {
            int direction = Vector3.Angle(hit.normal, Vector3.right) < 90 ? 1 : -1;
            float angle = Vector3.Angle(hit.normal, Vector3.up);
            //Debug.Log("angle " + angle);
            return angle * direction;
        }

        return 0;
    }
    
}
