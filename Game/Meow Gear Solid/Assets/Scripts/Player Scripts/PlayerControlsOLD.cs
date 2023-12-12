using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsOLD : MonoBehaviour
{

//All the commented out code involves moving where the character looks. Might implement back in
	public Animator animator;
	public bool isMoving;
	public float moveSpeed = 8;
	public float rotationSpeed = 720;

	Rigidbody rigid;

	Vector3 velocity;

	public Vector3 rotationVelo;
	float Myfloat;

	public ItemData currentlyEquipped;

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
		velocity = new Vector3 (horizInput, 0, vertInput).normalized * moveSpeed;
		if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }
		if(horizInput > 0 || horizInput < 0 || vertInput > 0 || vertInput < 0)
		{
			if(footSound == false)
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
		rigid.MovePosition (rigid.position + velocity * Time.fixedDeltaTime);

		//Handles Rotation
		if(rotationVelo.magnitude >= 0.1f)
		{
			float Angle = Mathf.Atan2(rotationVelo.x, rotationVelo.z) * Mathf.Rad2Deg;
			float SmoothRotation = Mathf.SmoothDampAngle(transform.localEulerAngles.y, Angle, ref Myfloat, 0.1f);
			transform.rotation = Quaternion.Euler(0, SmoothRotation, 0);
		}
	}
	IEnumerator Timeout()
    {
			yield return new WaitForSeconds(.3f);	
			footSound = false;
        
    }
}