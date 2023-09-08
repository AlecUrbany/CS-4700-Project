using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    //Fields for movement
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float maxSpeed = 10f;

    private Vector3 velocity;

    //Input
    private float xInput;
    private float zInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveInput();
    }

    void FixedUpdate()
    {
        Move(xInput, zInput);
    }

    //Movement
    private void Move(float xMove, float zMove)
    {
        Vector3 direction = transform.right * xMove + transform.forward * zMove;
        playerRB.AddForce(direction.normalized * speed * 10, ForceMode.Force);

        ControlSpeed();

    }
    private void ControlSpeed()
    {
        Vector3 flatVelocity = new Vector3(playerRB.velocity.x, 0f, playerRB.velocity.z);
        
        if(flatVelocity.magnitude > maxSpeed) 
        {
            //recalculate speed to be within limits of maxSpeed;
            Vector3 maxVelocity = flatVelocity.normalized * maxSpeed;

            //set player x and z velocity to new limited velocity
            playerRB.velocity = new Vector3(maxVelocity.x, playerRB.velocity.y, maxVelocity.z);
        }
    }

    //Create Input
    private void MoveInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        zInput = Input.GetAxisRaw("Vertical");
    }

}
