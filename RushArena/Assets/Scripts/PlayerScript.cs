using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//=================================================
// The main player script
//=================================================
public class PlayerScript : MonoBehaviour
{
    //référnces aux autres scripts du joueur 
    [SerializeField]
    internal PlayerInputScript inputScript;

    [SerializeField]
    internal PlayerMovementScript movementScript;

    [SerializeField]
    internal PlayerCollisionScript collisionScript;

    //variables de mouvement du joueur
    [SerializeField] 
    internal float playerSpeed = 0;
    [SerializeField]
    internal float jumpForce = 0;
    [SerializeField] 
    internal float gravityForce = 1;
    
    
    //variables de collision
    [SerializeField] 
    internal Transform groundCheck;
    [SerializeField] 
    internal LayerMask ground;
    
    
    internal CharacterController characterController;
    internal CapsuleCollider hitbox;

    // Start is called before the first frame update
    void Start()
    {
        print("Main PlayerScript Starting");
        characterController = GetComponent<CharacterController>();
        hitbox = GetComponent<CapsuleCollider>();
    }

}
