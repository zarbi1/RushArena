using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 



public class SlimeBehavior : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask groundLayer, playerLayer;

    //patrol mode 
    public Vector3 walkPoint;
    private bool walkPointSet;
    public float walkpointRange;
    
    //attack mode 
    public float attackDelay;
    private bool hasAttacked;
    public float throwForce;
    
    //states 
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public GameObject bullet;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);

        if (!playerInSightRange && !playerInAttackRange) Patrol();
        if (playerInSightRange && !playerInAttackRange) Chase();
        if (playerInAttackRange && playerInSightRange) Attack();


    }

    private void Patrol()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distancetoWalkPoint = transform.position - walkPoint;
        
        //walkpoint reached 
        if (distancetoWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float X = Random.Range(-walkpointRange, walkpointRange);
        walkPoint = new Vector3(transform.position.x + X, transform.position.y, transform.position.z);
        if (Physics.Raycast(walkPoint, -transform.up,2f,groundLayer))
        {
            walkPointSet = true;
        }
    }
    private void Chase()
    {
        agent.SetDestination(player.position);
        if (player.position.x < transform.position.x)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 270,transform.eulerAngles.z);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90,transform.eulerAngles.z);
        }
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!hasAttacked)
        {
            Rigidbody proj = Instantiate(bullet,transform.position,Quaternion.identity).GetComponent<Rigidbody>();
            proj.transform.LookAt(player);
            proj.AddForce(transform.forward * throwForce,ForceMode.VelocityChange);
            hasAttacked = true;
            Invoke(nameof(ResetAttack), attackDelay);
        }
    }

    private void ResetAttack()
    {
        hasAttacked = false;
    }
}
