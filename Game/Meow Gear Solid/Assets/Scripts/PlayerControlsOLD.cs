using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsOLD : MonoBehaviour
{

//All the commented out code involves moving where the character looks. Might implement back in
	public float moveSpeed = 8;
	public float rotationSpeed = 720;

	Rigidbody rigid;

	Vector3 velocity;

	public Vector3 rotationVelo;
	float Myfloat;


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
		if (Input.GetButton("Aim"))
		{

		} 

		velocity = new Vector3 (horizInput, 0, vertInput).normalized * moveSpeed;
	}

	void FixedUpdate()
    {
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
}