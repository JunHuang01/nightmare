using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public float chaseSpeed = 5.0f; // enemy chasing speed
    public float patrollSpeed = 2.0f; //enemy patroll speed
    public float currSpeed = 0.2f;
    public Transform[] wayPoints; //array that holds all the way points

    public bool shouldPatrol = true; //should the enemy patroll
    public bool shouldChase = true; //should the enemy chase player

    public float patrollWaitTime = 1.0f; //how much time does enemy wait at wait point
    public Vector3 playerLastSightPos; //last place the player was spotted by enemy
    
    public float stoppingDistance = 1.0f; //enemy stopping distance
    private bool isPlayerSighted;

    public float remDist;
   
    private Vector3 playerNotSightedPos; //a abitary number when player is not in sight.
    private NavMeshAgent navAgent; 
    private Transform playerTransform; //player's transform info

    private float patrollTimer; //time how much time enemy has been waiting at the way point
    private int wayPointIndex; //index of which way point enemy is on
    

    //reference to player and enemy stats;
    private EnemyStats enemyStats;


    //reference to player object 
    private GameObject player;

    //reference to enemy animator
    private Animator anim;

    //timer to track when was the last enemy attack
    private float enemyLastAttackTime;

    //bool for enemy attacking
    private bool isAttacking;

    //player animation states
    enum EnemyActionState { Idle, Moving, Attaccking, doneAttacking };

    //player animation state holder
    EnemyActionState actState;

    //global hash id
    //private HashIDs hash;

    void Awake()
    {
        isPlayerSighted = false; 
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerTransform = player.transform;
        playerNotSightedPos = new Vector3(99999f, 99999f, 99999f);
        navAgent = GetComponent<NavMeshAgent>();
        enemyStats = GetComponent<EnemyStats>();
        
        anim = GetComponent<Animator>();
        currSpeed = navAgent.speed;

        actState = EnemyActionState.Idle;

        //hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashIDs>();

    }

    void OnTriggerStay(Collider other)
    {
        // if player is in range of detection and is in sight, grab player's position, and indicate that player has been seen
        if (other.gameObject == player && isPlayerInLineOfSight())
        {
            playerLastSightPos = playerTransform.position;
            isPlayerSighted = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //if the player has left the range of detection, indicate that player is no longer spotted
        if (other.gameObject == player)
        {
            playerLastSightPos = playerNotSightedPos;
            isPlayerSighted = false;
        }
    }

    void Update()
    {
        remDist = (playerLastSightPos - transform.position).sqrMagnitude;
            
        currSpeed = navAgent.speed;
        //OnPlayerSighted();

        //When player is sighted and the enemy should chase, then cahse player otherwise if the enemy should patroll then patroll
        if (isPlayerSighted && shouldChase)
            Chase();
        else if (shouldPatrol)
            Patrolling();
        else
        {
            isAttacking = false;
            currSpeed = 0.2f;
        }

        updateActState();
        updateEnemyAnim();
        

    }

    void updateActState() {
        if (isAttacking)
            actState = EnemyActionState.Attaccking;
        else if (currSpeed > 1.0f)
        {
            actState = EnemyActionState.Moving;
        }
        else if (currSpeed < 1.0f)
            actState = EnemyActionState.Idle;
    }

    //update enemy animation according to action state
    void updateEnemyAnim() {
        if (actState == EnemyActionState.Attaccking) {
            anim.SetBool("isAttacking", true);
        }
        else if (actState == EnemyActionState.Moving)
        {
            anim.SetFloat("Speed", 3.5f);
            anim.SetBool("isAttacking", false);
        }
        else if (actState == EnemyActionState.Idle)
        {
            anim.SetFloat("Speed", 0f);
            anim.SetBool("isAttacking", false);
        }
    }
    //ToDo: rewrite this function for proper Enemy Response
    void OnPlayerSighted()
    {

        //Testing to see if player enters in sight testing cube change color
        if (isPlayerSighted)
        {
            renderer.material.color = Color.red;
        }
        else
        {
            renderer.material.color = Color.yellow;
        }
    }

    void Chase()
    {
        //Get the player's direction and distance from enemy
        Vector3 playerPosDelta = playerLastSightPos - transform.position;
        
        //set agent speed;
        navAgent.speed = chaseSpeed;

        //if player is not too close to the enemy, then set the new destination for enemy to be last position player is spotted
        if (playerPosDelta.sqrMagnitude > 4.0f)
            navAgent.destination = playerLastSightPos;

        //set chase speed of enemy
        
        AttackPlayer();
    }

    void AttackPlayer() {
        //when enemy get close to player attack player
        if (navAgent.remainingDistance < stoppingDistance && Time.time > enemyLastAttackTime && !isAttacking)
        {
            
            enemyLastAttackTime = Time.time + GlobalConstant.nextEnemyAttackRate;

            //if the enemy is Candle enemy set attack animation
            StopCoroutine("WaitTillAttackDone");
            isAttacking = true;
            StartCoroutine("WaitTillAttackDone");

        }

    }

    IEnumerator WaitTillAttackDone() {
        yield return new WaitForSeconds(0.5f);
        //player will be attacked by the enemy
        enemyStats.StartEnemyAttack();

        isAttacking = false;
        
        yield break;
    }

    void Patrolling()
    {
        //set enemy patroll speed
        navAgent.speed = patrollSpeed;

        //Patrol routine, change to next way point once reached a way point.
        if (navAgent.remainingDistance < stoppingDistance)
        {
            //increase timer of how long enemy has been at way point
            patrollTimer += Time.deltaTime;

            //print(patrollTimer);
            //if enemy has been waiting at the way point enough time, then move to next way point in line
            if (patrollTimer >= patrollWaitTime)
            {
                if (wayPointIndex == wayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                //reset timer for how long enemy has been waiting at way point
                patrollTimer = 0;
            }

        }

        //transform enemy to new way point.
        navAgent.destination = wayPoints[wayPointIndex].position;
    }

    //checking if the player is in sight.
    bool isPlayerInLineOfSight(){
        RaycastHit hit;
        //find the player's position relative to enemy
        Vector3 playerRelativePos = playerTransform.position - transform.position;

        float scaleFactor = 0.2f;

        //cast a ray toward player and check if the player is in line of sight or not
        if(Physics.Raycast(transform.position + transform.up*scaleFactor , playerRelativePos.normalized*10, out hit)){
            //if player is the first object in sight then player is in line of sight otherwise false
            
            if ( hit.collider.gameObject == player)
            {
                Debug.DrawRay(transform.position + transform.up * scaleFactor, playerRelativePos.normalized * 10, Color.green);   
                return true;
            }
            else{
                //print(hit.collider.gameObject.name);
                Debug.DrawRay(transform.position + transform.up * scaleFactor, playerRelativePos.normalized * 10, Color.red);
                return false;
            }
        }

        //everything else is indicating player is not in sight.
        return false;
    }
}