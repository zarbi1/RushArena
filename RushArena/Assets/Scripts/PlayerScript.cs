using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    [SerializeField]
    internal float airControl = 1;
    [SerializeField]
    internal float fastFallSpeed = 1;
    [SerializeField] 
    internal float slideSpeed = 1;
    


    //variables de collision
    [SerializeField]
    internal Transform groundCheck;
    [SerializeField]
    internal LayerMask ground;


    internal Rigidbody rigidbody;
    internal CapsuleCollider hitbox;


    PhotonView view;



    // Start is called before the first frame update
    private void Start()
    {
        print("Main PlayerScript Starting");
        rigidbody = GetComponent<Rigidbody>();
        hitbox = GetComponent<CapsuleCollider>();

        view = GetComponent<PhotonView>();
        
    }

    private void Update()
    {
        if (view.IsMine)
        {
            movementScript.UpdateMovement();
        }
    }
}
