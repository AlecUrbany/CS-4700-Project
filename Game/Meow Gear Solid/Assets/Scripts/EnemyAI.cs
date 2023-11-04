using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EnemyAIType{
    leftRight,
    UpDown,
    Square,
    Stationary
}
public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float playerRange;
    private Vector3 distanceFromPlayer;
    public GameObject childObject;
    public Rigidbody rb;
    public float moveSpeed;
    public EnemyAIType aiType;
    public float patrolDistance = 10.0f;
    public Vector3 startPosition;
    public int rotationSpeed;
    private float currentPatrolDistance;
    private bool movingStage1 = false;
    private bool movingStage2 = false;
    private int phase = 1;




    // Update is called once per frame
    void Awake(){
        GameObject childObject = transform.Find("Enemy Sightline").gameObject;
        startPosition = transform.position;
    }
    void Update()
    {
        Vector3 distanceFromPlayer = player.position - transform.position;
        bool canSeePlayer = childObject.GetComponentInChildren<visionCone>().canSeePlayer;
        if(distanceFromPlayer.magnitude <playerRange && canSeePlayer){
            FollowPlayer(distanceFromPlayer);
        }
        else{
            switch(aiType){
                case EnemyAIType.leftRight:
                    PatrolHorizontal();
                    break;
                case EnemyAIType.UpDown:
                    PatrolVertical();
                    break;
                case EnemyAIType.Square:
                    PatrolSquare();
                    break;
                case EnemyAIType.Stationary:
                    break;
            }
        }
         
    }

    void FollowPlayer(Vector3 distanceFromPlayer){
        distanceFromPlayer.Normalize();
        rb.velocity = distanceFromPlayer * moveSpeed;
        if (rb.velocity != Vector3.zero){
            Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);        }

    }
    void PatrolHorizontal()
    {
        Vector3 horizontalMovementDirection = movingStage1 ? Vector3.right : Vector3.left;
        rb.velocity = horizontalMovementDirection * moveSpeed;

        Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 8f);

        currentPatrolDistance += moveSpeed * Time.deltaTime;

        if (currentPatrolDistance >= patrolDistance){
            movingStage1 = !movingStage1;
            currentPatrolDistance = 0.0f;
        }
    }

    void PatrolVertical(){
        Vector3 verticalMovementDirection = movingStage1 ? Vector3.forward : Vector3.back;
        rb.velocity = verticalMovementDirection * moveSpeed;
        Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 2f);

        currentPatrolDistance += moveSpeed * Time.deltaTime;

        if (currentPatrolDistance >= patrolDistance){
            movingStage1 = !movingStage1;
            currentPatrolDistance = 0.0f;
        }
    }

    void PatrolSquare(){
        Vector3 verticalMovementDirection = (movingStage1 && movingStage2) ? Vector3.back : movingStage1 ? Vector3.left : movingStage2 ? Vector3.right : Vector3.forward;
        rb.velocity = verticalMovementDirection * moveSpeed;
        Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);

        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 5f);

        currentPatrolDistance += moveSpeed * Time.deltaTime;

        if (currentPatrolDistance >= patrolDistance){
            switch(phase){
                case 1:
                    movingStage1 = !movingStage1;
                    currentPatrolDistance = 0.0f;
                    phase++;
                    break;
                case 2:
                    movingStage2 = !movingStage2;
                    currentPatrolDistance = 0.0f;
                    phase++;
                    break;
                case 3:
                    movingStage1 = !movingStage1;
                    currentPatrolDistance = 0.0f;
                    phase++;
                    break;
                case 4:
                    movingStage2 = !movingStage2;
                    currentPatrolDistance = 0.0f;
                    phase = 1;
                    break;
            }
        }
    }
}
