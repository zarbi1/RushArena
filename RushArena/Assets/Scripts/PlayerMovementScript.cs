using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    [SerializeField] PlayerScript PS;  //PS = PlayerScript 
    
    private int extraJumps;
    private bool grounded;
    private float fastFall;
    
    void Start()
    {
        extraJumps = 1;
    }

    public void UpdateMovement()
    {   
        #region Jump
        grounded = PS.collisionScript.IsGrounded();

        if (grounded)
        {
            extraJumps = 1;
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
        
    }


    public void FixedUpdateMovement()
    {
        Debug.Log(extraJumps);
        HorizontalMovement();
    }



    #region Fonctions 
    
    private void HorizontalMovement()//,float lerpAmount)
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
        // lerp sert à fluidifier les changements de vitesse surtout par ex en fin de dash
        
        PS.RB.AddForce(movement * Vector3.right); //vector3.right = vecteur unitaire horizontal 
    }


    #region Jump
    
    private void Jump()
    {
        
        float force = PS.jumpForce;
        if (PS.RB.velocity.y < 0) 
            force -= PS.RB.velocity.y;
            
        PS.RB.AddForce(Vector3.up * force, ForceMode.Impulse);
    }
    
    
    #endregion
}

   #endregion


