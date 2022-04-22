using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] PlayerScript PS;  //PS = PlayerScript 
    
    private int extraJumps;
    private bool grounded;
    public bool facingRight;
    
    private bool TouchingFront;
    private bool wallSliding;
    
    private float dashBufferCounter;
    private bool isDashing;
    private bool hasDashed;
    private float DashTimer;
    
    #region private bool isSliding
    private bool isSliding()
    {
        bool slopeFacingRight = floorAngle > 0;
        return slopeFacingRight == facingRight && floorAngle != 0 && PS.inputScript.isDownPressed;
    }
    #endregion

    public Vector3 LastCheckpointPos = new Vector3(0,0,0.667f);
    
    public float floorAngle;
    private bool canDash => dashBufferCounter > 0f && !hasDashed && DashTimer <= 0;

    private float wallJumpCoyote;
    
    void Start()
    {
        extraJumps = 1;
        PS.trail.emitting = false;
        DashTimer = 0;
    }

    public void UpdateMovement()
    {
        
        floorAngle = grounded ? PS.collisionScript.FloorAngle() : 0;
        
        
        if (PS.inputScript.xInput == 1)
        {
            facingRight = true;
        }
        else if (PS.inputScript.xInput == -1)
        {
            facingRight = false;
        }
        
        #region Jump
        grounded = PS.collisionScript.IsGrounded();
        TouchingFront = PS.collisionScript.IsTouchingWall();
        
        if (grounded)
        {
            extraJumps = 1;
            hasDashed = false;
        }

        wallSliding = TouchingFront && !grounded && PS.inputScript.xInput != 0;

        if (wallSliding)
        {
            wallJumpCoyote = PS.wallJumpCoyoteTime;
        }
        else
        {
            wallJumpCoyote -= Time.deltaTime;
        }
        if (PS.inputScript.isSpaceDown) 
        {
            if (grounded)
            {
                Jump(PlayerJumpState.Default);
            }
            else if (wallSliding)
            {
                Jump(PlayerJumpState.WallSliding);
            }
            else if (wallJumpCoyote > 0)
            {
                Jump(PlayerJumpState.WallCoyote);
            }
            else if (extraJumps >= 1)
            {
                Jump(PlayerJumpState.Default);
                extraJumps--;
            }
        }
        #endregion

        #region Fall Physics
        //fastfall 
        if (PS.inputScript.isDownPressed && !grounded)
        {
            PS.RB.AddForce(Vector3.down * PS.fastFallSpeed,ForceMode.VelocityChange);
        }

        if (wallSliding && PS.RB.velocity.y < 0)
        {
            PS.RB.drag = PS.wallSlideDrag;
        }
        else
        {
            PS.RB.drag = 0;
        }
        
        //just making player fall faster 
        if (PS.RB.velocity.y < 0)
        {
            PS.RB.AddForce(Vector3.down * PS.fallSpeed,ForceMode.Acceleration);
        }
        #endregion
        
        #region dash timer
        if (PS.inputScript.isDashPressed)
        {
            dashBufferCounter = PS.dashBufferLength;
        }
        else
        {
            dashBufferCounter -= Time.deltaTime;
        }
        #endregion
        
        #region checkpoint


        if (PS.inputScript.isTPressed && LastCheckpointPos.z != 0)
        {
            PS.RB.position = LastCheckpointPos;
            PS.RB.velocity = Vector3.zero;
        }
        #endregion
    }


    public void FixedUpdateMovement()
    {
        if (canDash)
        {
            StartCoroutine(Dash(PS.inputScript.xInput, PS.inputScript.yInput));
        }
        if (!isDashing)
        {
            DashTimer -= Time.fixedDeltaTime;
            HorizontalMovement();
        }
        
    }



    #region Fonctions 
    
    private void HorizontalMovement()
    {

        float targetSpeed = PS.inputScript.xInput * PS.maxSpeed;
        float speedDif = targetSpeed - PS.RB.velocity.x;
        
        
        #region gestion de l'acceleration 
        
        float accel;
        
        if ((PS.RB.velocity.x > targetSpeed && targetSpeed > 0.01f) || (PS.RB.velocity.x < targetSpeed && targetSpeed < -0.01f))
        {
            accel = 0; // ==> pas d'accélération si on va déjà à la vitesse max 
        }
        else {

            accel = (Mathf.Abs(targetSpeed) > 0.01f) ? PS.acceleration : PS.deceleration;
        
            if (!grounded)
            {
                accel *= PS.airControl;
            }
        }
        
        #endregion
        
        #region gestion vélocité

        float velPower;
        
        //on applique des vélocités différentes en fonction des situations  
        
        if (Mathf.Abs(targetSpeed) < 0.01f)//targetspeed ≈ 0 ==> on applique la vélocité de stoppage  
        {
            velPower = PS.stopPower; 
        }
        else if (Mathf.Abs(PS.RB.velocity.x) > 0 && Mathf.Sign(targetSpeed) != Mathf.Sign(PS.RB.velocity.x))
            //ici on gère le cas où on veut changer de direction 
        {
            velPower = PS.turnPower;
        }
        else // sinon cas où on continue dans la mm direction 
        {
            velPower = isSliding() ? PS.accelPower * PS.slideSpeed : PS.accelPower ;
        }
        
        #endregion

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accel, velPower) * Mathf.Sign(speedDif);
        //movement = Mathf.Lerp(PS.RB.velocity.x, movement, lerpAmount); 
        
        PS.RB.AddForce(movement * Vector3.right); //vector3.right = vecteur unitaire horizontal 
    }
    
    
    private void Jump(PlayerJumpState state)
    {
        float force;
        Vector2 direction;

        switch (state) {
            case PlayerJumpState.WallSliding:
                PS.RB.drag = 0;
                force = PS.wallJumpForce;
                direction = Vector2.Lerp(Vector2.right * -PS.inputScript.xInput,Vector2.up,PS.wallJumpAngle);
                break;
            
            case PlayerJumpState.WallCoyote:
                force = PS.wallJumpForce;
                direction = Vector2.Lerp(Vector2.right * PS.inputScript.xInput,Vector2.up,PS.wallJumpAngle);
                break;
            
            default:
                force = PS.jumpForce;
                direction = Vector2.up;
                break;
        }
        
        if (PS.RB.velocity.y < 0) 
            force -= PS.RB.velocity.y;
        
        PS.RB.AddForce(direction * force, ForceMode.Impulse);
        
    }


    private IEnumerator Dash(float x, float y)
    {
        DashTimer = PS.dashCoolDown;
        float dashStartTime = Time.time;
        hasDashed = true;
        isDashing = true;
        
        PS.RB.velocity = Vector3.zero;
        PS.RB.useGravity = false;

        PS.trail.emitting = true;
        
        Vector2 dir;

        if (x != 0 || y != 0)
        {
            dir = new Vector2(x,y);
        }
        else
        {
            if (facingRight)
            {
                dir = new Vector2(1, 0);
            }
            else
            {
                dir = new Vector2(-1, 0);
            }
        }

        while (Time.time < dashStartTime + PS.dashLength)
        {
            PS.RB.velocity = dir.normalized * PS.dashSpeed;
            yield return null;
        }

        PS.RB.velocity = Vector3.zero;
        PS.RB.useGravity = true;
        isDashing = false;

        PS.trail.emitting = false;
    }

    void SetNewCheckPoint(Vector3 pos)
    {
        Debug.Log(LastCheckpointPos);
        LastCheckpointPos = pos;
    }
    
}

   #endregion


