using System.Collections;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] PlayerScript PS;  //PS = PlayerScript 
    
    private int extraJumps;
    private bool grounded;
    private bool facingRight;
    
    
    private float dashBufferCounter;
    private bool isDashing;
    private bool hasDashed;
    private bool canDash => dashBufferCounter > 0f && !hasDashed;
    
    void Start()
    {
        extraJumps = 1;
        
    }

    public void UpdateMovement()
    {
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

        if (grounded)
        {
            extraJumps = 1;
            hasDashed = false;
        }
        
        if (PS.inputScript.isSpaceDown) 
        {  
            if (grounded)
            {
                Jump();
            }
            else if (extraJumps >= 1)
            {
                Jump();
                extraJumps--;
            }
        }
        #endregion

        #region Fall Physics
        if (PS.inputScript.isDownPressed)
        {
            PS.RB.AddForce(Vector3.down * PS.fastFallSpeed,ForceMode.VelocityChange);
        }
        
        
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
    }


    public void FixedUpdateMovement()
    {
        if (canDash)
        {
            StartCoroutine(Dash(PS.inputScript.xInput, PS.inputScript.yInput));
        }
        if (!isDashing)
        {
            PS.RB.useGravity = true;
            HorizontalMovement();
        }
        
    }



    #region Fonctions 
    
    private void HorizontalMovement()
    {
        Debug.Log(PS.inputScript.xInput);
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
        else if (Mathf.Abs(PS.RB.velocity.x) > 0 && (Mathf.Sign(targetSpeed) != Mathf.Sign(PS.RB.velocity.x)))
            //ici on gère le cas où on veut changer de direction 
        {
            velPower = PS.turnPower;
        }
        else // sinon cas où on continue dans la mm direction 
        {
            velPower = PS.accelPower;
        }
        
        #endregion

        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accel, velPower) * Mathf.Sign(speedDif);
        //movement = Mathf.Lerp(PS.RB.velocity.x, movement, lerpAmount); 
        
        PS.RB.AddForce(movement * Vector3.right); //vector3.right = vecteur unitaire horizontal 
    }
    
    
    private void Jump()
    {
        
        float force = PS.jumpForce;
        if (PS.RB.velocity.y < 0) 
            force -= PS.RB.velocity.y;
            
        PS.RB.AddForce(Vector3.up * force, ForceMode.Impulse);
    }


    private IEnumerator Dash(float x, float y)
    {
        float dashStartTime = Time.time;
        hasDashed = true;
        isDashing = true;
        
        PS.RB.velocity = Vector3.zero;
        PS.RB.useGravity = false;

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

        isDashing = false;
    }

}

   #endregion


