using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RiptideNetworking;

[RequireComponent(typeof(InputHandler))]
public class TopDownCharacterMover : MonoBehaviour
{
	private InputHandler _input;


	[SerializeField]
	private bool RotateTowardMouse;

	[SerializeField]
	private float MovementSpeed;
	[SerializeField]
	private float RotationSpeed;

	[SerializeField]
	private Camera Camera;

	private Rigidbody rb;

	private Vector3 targetVector;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		_input = GetComponent<InputHandler>();
	}

	private void OnEnable()
	{
		InvokeRepeating("Move", 0f, 0.04f);
		Camera = Camera.main;
	}

	// Update is called once per frame
	void Update()
	{
		targetVector = new Vector3(_input.InputVector.x, 0, _input.InputVector.y);

        if(_input.GetJumpButtowDown)
        {
			rb.AddForce(new Vector3(0, 150, 0), ForceMode.Impulse);
		}
	}

	void FixedUpdate()
	{

		var movementVector = MoveTowardTarget(targetVector);

		if (!RotateTowardMouse)
		{
			RotateTowardMovementVector(movementVector);
		}

		if (RotateTowardMouse)
		{
			RotateFromMouseVector();
		}

	}

	private void RotateFromMouseVector()
	{
		Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

		if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
		{
			var target = hitInfo.point;
			target.y = transform.position.y;
			transform.LookAt(target);
		}
	}

	private Vector3 MoveTowardTarget(Vector3 targetVector)
	{
		var speed = MovementSpeed * Time.deltaTime;
		// transform.Translate(targetVector * (MovementSpeed * Time.deltaTime)); Demonstrate why this doesn't work
		//transform.Translate(targetVector * (MovementSpeed * Time.deltaTime), Camera.gameObject.transform);

		targetVector = Quaternion.Euler(0, Camera.gameObject.transform.rotation.eulerAngles.y, 0) * targetVector;
		var targetPosition = transform.position + targetVector * speed;
		rb.MovePosition(targetPosition);

		return targetVector;
	}

	private void RotateTowardMovementVector(Vector3 movementDirection)
	{
		if (movementDirection.magnitude == 0) { return; }
		var rotation = Quaternion.LookRotation(movementDirection);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, RotationSpeed);
	}

	public void Move()
	{
		Message message = Message.Create(MessageSendMode.reliable, Packets.ClientToHostId.playerMovement);
		message.AddVector3(transform.position).AddQuaternion(transform.rotation);
		CheckMessage.CheckMsg(message);
	}
}
