using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
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
    public NavMeshAgent agent;
    private float currentPatrolDistance;
    private bool movingStage1 = false;
    private bool movingStage2 = false;
    private int phase = 1;
    private bool patrol = true;
    private bool alert = false;
    private bool chasing = false;
    private bool coroutineRunning = false;
    private Coroutine chaseCoroutine;
    public Quaternion startRotation;
    // private GameObject gameStateManagerObject;
    private AlertPhase alertPhaseScript; 
    private Vector3 lastKnownPosition;
    private double timeRemaining;

//These six lines are for the exclamation point upon noticing the player
    public Transform enemyMouth;
    [SerializeField] private GameObject floatingTextBox;
    public float nameLifeSpan = .5f;
    public AudioSource source;
    public AudioClip alertSound;
    public bool hasBeenAlerted = false;






    // Update is called once per frame
    void Awake(){
        GameObject childObject = transform.Find("Enemy Sightline").gameObject;
        // gameStateManagerObject = GameObject.Find("Game State Manager");
        alertPhaseScript = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<AlertPhase>();
        alert = alertPhaseScript.inAlertPhase;
    }
    void Start(){
        startPosition = transform.position;
        startRotation = transform.rotation;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update(){
        alert = alertPhaseScript.inAlertPhase;
        lastKnownPosition = alertPhaseScript.lastKnownPosition;
        timeRemaining = alertPhaseScript.timeRemaining;
        if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }
        Vector3 distanceFromPlayer = player.position - transform.position;
        bool canSeePlayer = childObject.GetComponentInChildren<visionCone>().canSeePlayer;
        if(distanceFromPlayer.magnitude < playerRange && canSeePlayer)
        {
            if(!hasBeenAlerted)
            {
                ShowAlertSound();
                hasBeenAlerted = true;
            }
            chasing = true;
            alertPhaseScript.inAlertPhase = true;
            alertPhaseScript.lastKnownPosition = player.position;
            alertPhaseScript.timeRemaining = 5;
            patrol = false;
            FollowPlayer(player.position);
        }

        else if(chasing && canSeePlayer){
            chasing = true;
            alertPhaseScript.timeRemaining = 5;
            FollowPlayer(player.position);
        }

        else if(chasing && !canSeePlayer){
            if(timeRemaining > 0){
                alert = true;
                alertPhaseScript.inAlertPhase = true;
                chasing = false;
            }
            else{
                alert = false;
                alertPhaseScript.inAlertPhase = false;
                chasing = false;
                patrol = true;
            }
        }

        else if(alert && timeRemaining > 0){
            float threshold = 0.1f;
            if(Vector3.Distance(lastKnownPosition, transform.position) < threshold){
                rb.velocity = Vector3.zero;
                Debug.Log("reached location");
            }
            else{
                FollowPlayer(lastKnownPosition);
            }
            
        }

        else if (patrol){
            patrol = true;
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
        else{
            if(!agent.pathPending){
                agent.SetDestination(startPosition);
                Debug.Log("returning to start");
            }
            if(startPosition.x == transform.position.x && startPosition.z == transform.position.z){
                rb.velocity = Vector3.zero;
                transform.rotation = startRotation;
                patrol = true;
                agent.ResetPath();
            }
        }
    }


    void FollowPlayer(Vector3 playerPosition){
        Vector3 distanceFromPlayer = playerPosition - transform.position;
        distanceFromPlayer.Normalize();
        rb.velocity = distanceFromPlayer * moveSpeed;

        if (rb.velocity != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }
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
    IEnumerator ContinueChase(){
        yield return new WaitForSeconds(5f);
        if(!chasing){
            alert = false;
            alertPhaseScript.inAlertPhase = false;
            hasBeenAlerted = false;
            patrol = true;
        }
        chaseCoroutine = null;
    }
    void ShowAlertSound()
    {
        if(floatingTextBox)
        {
            GameObject prefab = Instantiate(floatingTextBox, enemyMouth, false);
            source.PlayOneShot(alertSound);
            Destroy(prefab, nameLifeSpan);
        }
    }
}





