using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] PlayerScript playerScript;
    
    private Vector3 moveDirection;
    private int extraJumps;



    // Start is called before the first frame update
    void Start()
    {
        extraJumps = 1;
    }

    
    // Update is called once per frame
    void Update()
    {

        moveDirection = new Vector3(moveDirection.x, moveDirection.y, 0);
        bool grounded = playerScript.collisionScript.IsGrounded();


        if (playerScript.inputScript.isRightPressed)
        {
            MovePlayerRight(grounded);
        }
        else if (playerScript.inputScript.isLeftPressed)
        {
            MovePlayerLeft(grounded);
        }
        else
        {
            StopLateralMovement();
        }

        if (playerScript.inputScript.isSpacePressed)
        {
            /*
                * Si le joueur est au sol on le laisse sauter et on réinitialise le nb de saut en +
                * sinon on le fait sauter si il lui reste un saut 
                */
            if (grounded)
            {
                Jump();
                extraJumps = 1;
            }
            else if (extraJumps >= 1)
            {
                Jump();
                extraJumps--;
            }
        }



        //Time.deltaTime sert rendre le mouvement indépendant du framerate 

        if (!grounded)
        {
            if (playerScript.inputScript.isDownPressed)
            {
                moveDirection.y += Physics.gravity.y * playerScript.gravityForce * playerScript.fastFallSpeed;
            }
            else
            {
                moveDirection.y += Physics.gravity.y * playerScript.gravityForce;
            }
        }

        playerScript.characterController.Move(moveDirection * Time.deltaTime);
        
        
    }



    private void MovePlayerLeft(bool grounded)
    {
        if (grounded)
        {
            moveDirection.x = -playerScript.playerSpeed;
        }
        else
        {
            moveDirection.x = -playerScript.playerSpeed*playerScript.airControl;
        }
    }

    private void MovePlayerRight(bool grounded)
    {
        if (grounded)
        {
            moveDirection.x = playerScript.playerSpeed;
        }
        else
        {
            moveDirection.x = playerScript.playerSpeed*playerScript.airControl;
        }    
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





