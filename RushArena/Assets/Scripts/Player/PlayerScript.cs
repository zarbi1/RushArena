using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
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
    [SerializeField] 
    internal PlayerActionScript actionScript;
    [SerializeField]
    internal PlayerAnimation animScript;
    #endregion

    #region movement variables
    [SerializeField]
    internal float maxSpeed = 0;
    [SerializeField]
    internal float fastFallSpeed = 1;
    [SerializeField]
    internal float fallSpeed = 1;
    [SerializeField] 
    internal float slideSpeed = 2;
    [SerializeField]
    internal float airControl = 1;
    [SerializeField]
    internal float dashSpeed = 1;
    [SerializeField]
    internal float wallSlideDrag = 0;
    
    [SerializeField]
    internal float wallJumpAngle = 0.5f;
    
    //interpolation linéaire de vecteur up et right/left 
    // => 0.5 = 45°, 0 = right/left, 1 = up   
    #endregion

    #region Forces 
    [SerializeField]
    internal float jumpForce = 0;
    [SerializeField]
    internal float wallJumpForce = 0;

    [SerializeField] 
    internal float throwAxeForce = 1;
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
    internal Transform RightCheck;
    [SerializeField]
    internal Transform LeftCheck;
    [SerializeField]
    internal Transform groundCheck;
    [SerializeField]
    internal LayerMask ground;
    [SerializeField]
    internal TrailRenderer trail;
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
    internal float dashLength = 0.2f;
    [SerializeField]
    internal float dashBufferLength = 0.2f;
    [SerializeField]
    internal float wallJumpCoyoteTime = 0.1f;
    [SerializeField]
    internal float dashCoolDown = 5f;
    
    #endregion
    
    #region Objects
    
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
            actionScript.UpdateActions();
        }
    }
}
