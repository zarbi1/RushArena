using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float jumpDelay = 2;

    [SerializeField] private float jumpForce = 1;

    [SerializeField] private float moveSpeed;
    
    [SerializeField] private Transform leftLimit;
    [SerializeField] private Transform rightLimit;

    private Vector3 moveDirection;
    
    
    private Rigidbody RB;
    private System.Random rd;
    
    
    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 3f;
    private float characterVelocity = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;
    
    private Vector3 right = new Vector3(1, 1, -1);
    private Vector3 left = new Vector3(1, 1, 1);
    
    void Start(){
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }
 
    void calcuateNewMovementVector(){
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 0).normalized;
        movementPerSecond = movementDirection * characterVelocity;

        transform.localScale.z = movementDirection.x > 0 ? -1 : 1;

    }
 
    void Update(){
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime){
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }
     
        //move enemy: 
        transform.position = new Vector3(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y, transform.position.z);
 
    }
    }

    // void Jump()
    // {
    //     RB.AddForce(Vector3.up * jumpForce);
    // }

    /*void Wander()
    {
        switch (rd.Next(0,2))
        {
            case 0:
                moveDirection = Vector3.left;
                break;
            case 1:
                moveDirection = Vector3.right;
                break;
        }
        Debug.Log(1);
        //Jump();
    }*/
    

