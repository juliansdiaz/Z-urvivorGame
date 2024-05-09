using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Variables
    NavMeshAgent enemyNavMeshAgent;
    Animator enemyAnimator;
    AudioSource enemyAudio;

    [SerializeField] AudioClip talk_sfx;

    [Header("-----Waypoints Path-----")]
    [SerializeReference] Transform[] destinations;
    [SerializeField] float distanceToWayPoint = 2.5f;
    int destinationIndex = 0;

    [Header("-----FollowPlayer-----")]
    [SerializeField] bool isFollowingPlayer  = true;
    [SerializeField] float followDistance = 10.0f;
    GameObject playerGameObject;
    float distanceToPlayer;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        enemyNavMeshAgent.destination = destinations[destinationIndex].transform.position;
        playerGameObject = FindObjectOfType<PlayerMovement>().gameObject;
        enemyAnimator = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();

        PlaySound(talk_sfx);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, playerGameObject.transform.position); //Calculate distance between enemy and player
        if((distanceToPlayer < followDistance) && isFollowingPlayer) //Check if player is in following distance
        {
            FollowPlayer();
            if(distanceToPlayer <= 3.5f) //Check if player is close enough to attack
            {
                Attack();
                if(distanceToPlayer > 3.5f) //Check if player is far enough to follow again
                {
                    FollowPlayer();
                }
            }
        }
        else
        {
            EnemyWaypointsPath(); //Follow default route
        }
    }

    public void PlaySound(AudioClip sfx)
    {
        enemyAudio.PlayOneShot(sfx);
    }

    void EnemyWaypointsPath()
    {
        //Play walking animation
        enemyAnimator.SetBool("WalkState", true);
        enemyAnimator.SetBool("RunState", false);

        enemyNavMeshAgent.destination = destinations[destinationIndex].position; //Set route waypoints
        if (Vector3.Distance(transform.position, destinations[destinationIndex].position) <= distanceToWayPoint) //Check if enemy is close to waypoint position
        {
            enemyNavMeshAgent.speed = 1.5f; //Reduce walking speed
            enemyNavMeshAgent.angularSpeed = 20.0f; //increase turning speed
            if (destinations[destinationIndex] != (destinations[destinations.Length - 1])) //Check if there are more waypoints in the route to follow
            {
                destinationIndex++; //Move to next waypoint in route
            }
            else
            {
                destinationIndex = 0; //Reset following route
            }
        }
    }

    void FollowPlayer()
    {
        enemyNavMeshAgent.destination = playerGameObject.transform.position; //Start following player
        enemyNavMeshAgent.angularSpeed = 60.0f; //Increase turning distance
        enemyNavMeshAgent.speed = 3.0f; //Increase movement distance
        enemyNavMeshAgent.acceleration = 4.0f; //Increase movement acceleration

        //Play running animation
        enemyAnimator.SetBool("WalkState", false);
        enemyAnimator.SetBool("RunState", true);
        enemyAnimator.SetBool("AttackState", false);
    }

    void Attack()
    {
        enemyNavMeshAgent.speed = 0.0f; //Stop Moving

        //Play attack animation
        enemyAnimator.SetBool("AttackState", true);
        enemyAnimator.SetBool("RunState", false);
    }
}
