using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{

//All the commented out code involves moving where the character looks. Might implement back in
	public float moveSpeed = 8;

	Rigidbody rigidbody;
	//Camera viewCamera;
	Vector3 velocity;

	void Start ()
    {
		rigidbody = GetComponent<Rigidbody> ();
		//viewCamera = Camera.main;
	}

	void Update ()
    {
		//Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, viewCamera.transform.position.y));
		//transform.LookAt (mousePos + Vector3.up * transform.position.y);
		velocity = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical")).normalized * moveSpeed;
	}

	void FixedUpdate()
    {
		rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}
}
