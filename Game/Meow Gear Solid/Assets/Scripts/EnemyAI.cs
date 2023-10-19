using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float playerRange;
    private Vector3 distanceFromPlayer;
    public GameObject childObject;
    public Rigidbody rb;
    public float moveSpeed;
    public bool horizontalPatrol = true;
    public float patrolDistanceX = 10.0f;
    public float patrolDistanceZ = 10.0f;
    private float currentPatrolDistanceX;
    private float currentPatrolDistanceZ;
    private bool movingRight = true;
    private bool movingUp = true; 


    // Update is called once per frame
    void Awake(){
        GameObject childObject = transform.Find("Enemy Sightline").gameObject;
    }
    void Update()
    {
        Vector3 distanceFromPlayer = player.position - transform.position;
        bool canSeePlayer = childObject.GetComponentInChildren<visionCone>().canSeePlayer;
        if(distanceFromPlayer.magnitude <playerRange && canSeePlayer){
            FollowPlayer(distanceFromPlayer);
        }
        else{
            if(horizontalPatrol){ PatrolHorizontal(); }
            else{ PatrolVertical(); }
        }
         
    }

    void FollowPlayer(Vector3 distanceFromPlayer){
        distanceFromPlayer.Normalize();
        rb.velocity = distanceFromPlayer * moveSpeed;
    }
    void PatrolHorizontal()
    {
        Vector3 horizontalMovementDirection = movingRight ? Vector3.right : Vector3.left;
        rb.velocity = horizontalMovementDirection * moveSpeed;

        currentPatrolDistanceX += moveSpeed * Time.deltaTime;

        if (currentPatrolDistanceX >= patrolDistanceX)
        {
            movingRight = !movingRight;
            currentPatrolDistanceX = 0.0f;
        }
    }

    void PatrolVertical(){
        Vector3 verticalMovementDirection = movingUp ? Vector3.forward : Vector3.back;
        rb.velocity = verticalMovementDirection * moveSpeed;

        currentPatrolDistanceZ += moveSpeed * Time.deltaTime;

        if (currentPatrolDistanceZ >= patrolDistanceZ){
            movingUp = !movingUp;
            currentPatrolDistanceZ = 0.0f;
        }
    }
}
