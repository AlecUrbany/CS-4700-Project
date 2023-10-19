using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

//All the commented out code involves moving where the character looks. Might implement back in
	public float moveSpeed = 8;
	public float rotationSpeed = 720;

	Rigidbody rigidbody;

	Vector3 velocity;


	void Start ()
    {
		rigidbody = GetComponent<Rigidbody> ();
		//viewCamera = Camera.main;
	}

	void Update ()
    {
		float horizInput = Input.GetAxisRaw ("Horizontal");
		float vertInput = Input.GetAxisRaw ("Vertical");
		velocity = new Vector3 (horizInput, 0, vertInput).normalized * moveSpeed;
	}

	void FixedUpdate()
    {
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}
}
