using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{

    public float chaseSpeed = 5.0f;
    public float patrollSpeed = 2.0f;
    public Transform[] wayPoints;

    public bool shouldPatrol = true;
    public bool shouldChase = true;

    public float patrollWaitTime = 1.0f;
    public Vector3 playerLastSightPos;

    private bool isPlayerSighted;

    private Vector3 playerNotSightedPos;
    private NavMeshAgent navAgent;
    private Transform playerTransform;

    private float patrollTimer;
    private int wayPointIndex;
    private float stoppingDistance = 0.05f;


    private GameObject player;
    void Awake()
    {
        isPlayerSighted = false;
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerTransform = player.transform;
        playerNotSightedPos = new Vector3(99999f, 99999f, 99999f);
        navAgent = GetComponent<NavMeshAgent>();


    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject == player && isPlayerInLineOfSight())
        {
            playerLastSightPos = playerTransform.position;
            isPlayerSighted = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            isPlayerSighted = false;
    }

    void Update()
    {
        OnPlayerSighted();
        if (isPlayerSighted && shouldChase)
            Chase();
        else if (shouldPatrol)
            Patrolling();

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

        if (playerPosDelta.sqrMagnitude > 4f)
            navAgent.destination = playerLastSightPos;

        navAgent.speed = chaseSpeed;

        //Todo: rewrite following to define what happens when monster get close to player
        if (navAgent.remainingDistance < stoppingDistance)
        {
            renderer.material.color = Color.green;
        }
    }

    void Patrolling()
    {
        navAgent.speed = patrollSpeed;

        //Patrol routine, change to next way point once reached a way point.
        if (navAgent.remainingDistance < stoppingDistance)
        {
            patrollTimer += Time.deltaTime;

            print(patrollTimer);
            if (patrollTimer >= patrollWaitTime)
            {
                if (wayPointIndex == wayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;

                patrollTimer = 0;
            }

        }

        navAgent.destination = wayPoints[wayPointIndex].position;
    }

    //checking if the player is in sight.
    bool isPlayerInLineOfSight(){
        RaycastHit hit;
        Vector3 playerRelativePos = playerTransform.position - transform.position;

        if(Physics.Raycast(transform.position + transform.up, playerRelativePos, out hit)){
            if ( hit.collider.gameObject == player)
            {
                return true;
            }
            else{
                return false;
            }
        }

        return false;
    }
}