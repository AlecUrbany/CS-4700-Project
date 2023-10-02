using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

//All the commented out code involves moving where the character looks. Might implement back in
	//public float moveSpeed = 8;
	//public float rotationSpeed = 720;

	//Rigidbody rigidbody;

	//Vector3 velocity;

	[SerializeField] private float _moveSpeed = 3f;

	[SerializeField] private float _turnSpeed = 30f;

	private CharacterController _cc;


	void Start ()
    {
		//rigidbody = GetComponent<Rigidbody> ();
		//viewCamera = Camera.main;

		_cc = GetComponent<CharacterController>();
	}

	void Update ()
    {
		//float horizInput = Input.GetAxisRaw ("Horizontal");
		//float vertInput = Input.GetAxisRaw ("Vertical");
		//velocity = new Vector3 (horizInput, 0, vertInput).normalized * moveSpeed;
		if (Input.GetKey(KeyCode.W))
        {
            _cc.Move(transform.forward * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _cc.Move(-transform.forward * _moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _cc.transform.Rotate(0, -_turnSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
           _cc. transform.Rotate(0, _turnSpeed * Time.deltaTime, 0);
        }
	}

	void FixedUpdate()
    {
		//rigidbody.MovePosition (rigidbody.position + velocity * Time.fixedDeltaTime);
	}
}
