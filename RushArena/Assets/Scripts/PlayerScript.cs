using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//=================================================
// The main player script
//=================================================
public class PlayerScript : MonoBehaviour
{
    #region Scripts
    [SerializeField]
    internal PlayerInputScript inputScript;
    [SerializeField]
    internal PlayerMovementScript movementScript;
    [SerializeField]
    internal PlayerCollisionScript collisionScript;
    #endregion

    #region movement variables
    [SerializeField]
    internal float maxSpeed = 0;
    [SerializeField]
    internal float fastFallSpeed = 1;
    [SerializeField]
    internal float fallSpeed = 1;
    [SerializeField] 
    internal float slideSpeed = 1;
    [SerializeField]
    internal float airControl = 1;
    [SerializeField]
    internal float dashSpeed = 1;
    #endregion

    #region Forces 
    [SerializeField]
    internal float jumpForce = 0;
    #endregion

    #region transition variables 
    [SerializeField]
    internal float accelPower = 1;
    [SerializeField]
    internal float turnPower = 1;
    [SerializeField]
    internal float stopPower = 0;
    [SerializeField]
    internal float acceleration = 1;
    [SerializeField]
    internal float deceleration = 1;
    #endregion

    #region collision checks
    [SerializeField]
    internal Transform groundCheck;
    [SerializeField]
    internal LayerMask ground;
    #endregion

    #region player components
    internal Rigidbody RB;
    internal CapsuleCollider hitbox;
    #endregion

    #region network variables
    PhotonView view;
    #endregion

    #region Timers 
    [SerializeField]
    internal float jumpDelay = 1;
    [SerializeField]
    internal float dashLength = 0.2f;
    [SerializeField]
    internal float dashBufferLength = 0.2f;
    #endregion

    private void Start() 
    {
        print("Main PlayerScript Starting");
        RB = GetComponent<Rigidbody>();
        hitbox = GetComponent<CapsuleCollider>();

        view = GetComponent<PhotonView>();
        
    }

    private void FixedUpdate()
    {
        if (view.IsMine)
        {
            movementScript.FixedUpdateMovement();
        }
    }
    
    private void Update()
    {
        if (view.IsMine)
        {
        movementScript.UpdateMovement();
        }
    }
}
