using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] PlayerScript playerScript;
    
    private Vector3 moveDirection;
    
    

    // Start is called before the first frame update
    void Start()
    {
    }

    
    // Update is called once per frame
    void Update()
    {

        moveDirection = new Vector3(0, 0, 0);
        
        if (playerScript.inputScript.isRightPressed)
        {
            MovePlayerRight();
        }
        else if (playerScript.inputScript.isLeftPressed)
        {
            MovePlayerLeft();
        }
        else
        {
           StopLateralMovement();
        }
        
        if (playerScript.inputScript.isSpacePressed)
        {
            Jump();
        }
        
        
        //Time.deltaTime sert rendre le mouvement indépendant du framerate 
        moveDirection.y += Physics.gravity.y  * playerScript.gravityForce; 
        playerScript.characterController.Move(moveDirection * Time.deltaTime);
    }



    private void MovePlayerLeft()
    {
        moveDirection.x = -playerScript.playerSpeed;
    }

    private void MovePlayerRight()
    {
        moveDirection.x = playerScript.playerSpeed;
    }

    private void Jump()
    {
        moveDirection.y = playerScript.jumpForce;
    }
    
    
     //Cette fonction sert à ce que le joueur ne continue pas son mouvement quand on presse
     //aucune touche
     private void StopLateralMovement()
     {
         moveDirection = new Vector3(0, moveDirection.y,0);
     }
}





