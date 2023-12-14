using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementControls : MonoBehaviour
{

//All the commented out code involves moving where the character looks. Might implement back in
	public Animator animator;
	public bool isMoving;
    public bool isCrouching;
	public float moveSpeed = 450;
    public float tankMoveSpeed = 4;
	public float rotationSpeed = 720;
    public float tankRotationSpeed = 360;

	Rigidbody rigid;

	public Vector3 playerVelocity;

	public Vector3 rotationVelo;
	float Myfloat;

	public ItemData currentlyEquipped;
    public bool tankControls;

	//Sound nonsense is below
	public AudioSource source;
    public AudioClip footStep1;
    public AudioClip footStep2;
	public bool footSound;

	void Start ()
    {
		rigid = GetComponent<Rigidbody> ();
		//viewCamera = Camera.main;
	}

	void Update ()
    {	
		rotationVelo = new Vector3(Input.GetAxisRaw ("Horizontal"), 0 , Input.GetAxisRaw ("Vertical"));
		float horizInput = Input.GetAxisRaw ("Horizontal");
		float vertInput = Input.GetAxisRaw ("Vertical");
		playerVelocity = new Vector3 (horizInput, 0, vertInput).normalized * moveSpeed * Time.fixedDeltaTime;
		if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }

        if(Input.GetButton("Aim"))
        {
            tankControls = true;
        }
        else
        {
            tankControls = false;
        }

        if(Input.GetButtonDown("Crouch"))
        {
            ToggleCrouch();
        }

		if(horizInput > 0 || horizInput < 0 || vertInput > 0 || vertInput < 0)
		{
			if(footSound == false && isCrouching == false)
			{
				if(Random.Range(0, 2) == 0 )
				{
					footSound = true;
					source.PlayOneShot(footStep2, .75f);
					StartCoroutine("Timeout");	
				}
				else
				{
					footSound = true;
					source.PlayOneShot(footStep1, .75f);
					StartCoroutine("Timeout");	
				}
				
			}
            if(footSound == false && isCrouching == true)
			{
				if(Random.Range(0, 2) == 0 )
				{
					footSound = true;
					source.PlayOneShot(footStep2, .75f);
					StartCoroutine("TimeoutCrouch");	
				}
				else
				{
					footSound = true;
					source.PlayOneShot(footStep1, .75f);
					StartCoroutine("TimeoutCrouch");	
				}
				
			}
			
			isMoving = true;
			animator.SetBool("IsMoving", isMoving);
		}
		else
		{
			isMoving = false;
			animator.SetBool("IsMoving", isMoving);
		}
	}

	void FixedUpdate()
    {
		if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }
		//Handles rigid body movement
        if(tankControls == true)
        {
            rigid.velocity = transform.forward*Input.GetAxis("Vertical")*tankMoveSpeed;
            
            transform.Rotate(0,Input.GetAxis("Horizontal")*tankRotationSpeed*.25f*Time.deltaTime,0);
            
        }

        else
        {
            if(isCrouching == true)
            {
                rigid.velocity = playerVelocity *.5f;
                //Handles Rotation
                if(rotationVelo.magnitude >= 0.1f)
                {
                    float Angle = Mathf.Atan2(rotationVelo.x, rotationVelo.z) * Mathf.Rad2Deg;
                    float SmoothRotation = Mathf.SmoothDampAngle(transform.localEulerAngles.y, Angle, ref Myfloat, 0.1f);
                    transform.rotation = Quaternion.Euler(0, SmoothRotation, 0);
                }
            }

            else
            {
                rigid.velocity = playerVelocity;
                if(rotationVelo.magnitude >= 0.1f)
                {
                    float Angle = Mathf.Atan2(rotationVelo.x, rotationVelo.z) * Mathf.Rad2Deg;
                    float SmoothRotation = Mathf.SmoothDampAngle(transform.localEulerAngles.y, Angle, ref Myfloat, 0.1f);
                    transform.rotation = Quaternion.Euler(0, SmoothRotation, 0);
                }
            }            
        }

	}
    public void ToggleCrouch()
    {
        if (isCrouching == false)
        {
            isCrouching = true;
            animator.SetBool("IsCrouching", isCrouching);
        }

        else
        {
            isCrouching = false;
            animator.SetBool("IsCrouching", isCrouching);
        }
    }
	IEnumerator Timeout()
    {
			yield return new WaitForSeconds(.3f);	
			footSound = false;
        
    }
    IEnumerator TimeoutCrouch()
    {
			yield return new WaitForSeconds(.6f);	
			footSound = false;
        
    }
}