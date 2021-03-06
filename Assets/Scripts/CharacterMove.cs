﻿using UnityEngine; 
using System.Collections;

public class CharacterMove : MonoBehaviour {

	public float speed = 2.5f;
	public float dodgeSpeed = 1000.0f;
	public float max_speed = 5.0f;
	public float jump_velocity;
	public float friction = 1.0f;

	private MovingEntity AttachedScript;
	private bool m_is_jumping = false;

	public float DodgeCoolDown = 3.0f;
	private bool IsDodging = false;
	private float TimeDodging = 0.1f;
	private float DodgeTimer = 0.0f;
	private bool DodgeButtonDown;


	private bool TimeKeeping = false;
	private GlobalTimeKeeper TimeKeeper = null;

	public bool LockMovement = false;

	void Start()
	{
		AttachedScript = GetComponent<MovingEntity>();
		TimeKeeper = FindObjectOfType<GlobalTimeKeeper>();
		TimeKeeping = (TimeKeeper != null);
	}

	// Update is called once per frame
	void Update () 
	{
		float DeltaTime;
		if (TimeKeeping)
			DeltaTime = TimeKeeper.EntityDeltaTime;
		else
			DeltaTime = Time.deltaTime;

		Vector3 Velocity = AttachedScript.Velocity;
		Vector2 curHorizontalVelocity = new Vector2 (Velocity.x, Velocity.z);

		if (!LockMovement) 
		{
			if (DodgeTimer > 0)
				DodgeTimer -= DeltaTime;

			if (DodgeTimer < DodgeCoolDown)
				IsDodging = false;

			//PC INPUT
			curHorizontalVelocity.x = Input.GetAxis ("Horizontal");
			curHorizontalVelocity.y = Input.GetAxis ("Vertical");


			if (Input.GetAxis("Dodge") != 0 && DodgeTimer <= 0 && !DodgeButtonDown) {
				if (curHorizontalVelocity.x > 0)
					curHorizontalVelocity.x += dodgeSpeed * DeltaTime;

				if (curHorizontalVelocity.x < 0)
					curHorizontalVelocity.x -= dodgeSpeed * DeltaTime;

				DodgeTimer = TimeDodging + DodgeCoolDown;
				IsDodging = true;
				DodgeButtonDown = true;
			}
			else if ((Input.GetAxis("Dodge") == 0) && DodgeButtonDown)
				DodgeButtonDown = false;
			

		}

		//limits the horizontal velocity by maxspeed as a max Magnitude
		if (curHorizontalVelocity.magnitude != 0) {	
			//clamps at maxSpeed
			if (!IsDodging)
				curHorizontalVelocity = Vector2.ClampMagnitude (curHorizontalVelocity, max_speed * DeltaTime);
			else
				IsDodging = true;
	
			Vector2 friction_vec = (curHorizontalVelocity.normalized) * friction * DeltaTime;
			if (m_is_jumping)
				friction_vec *= 2;
			
			//substracts friction
			if (curHorizontalVelocity.magnitude - friction * DeltaTime > 0)
				curHorizontalVelocity -= friction_vec;
			else
				curHorizontalVelocity = Vector2.zero;
		}
	
		if (!LockMovement)
		{
			//Jump
			if (Input.GetAxis ("Jump") != 0 && !m_is_jumping) 
			{
				Velocity.y += jump_velocity; 
				m_is_jumping = true;
			}
		}
	
		if (Velocity.y == 0) 
		{
			//rounds to y_floor and resets jump
			transform.position = new Vector3 (transform.position.x, AttachedScript.y_floor, transform.position.z);
			m_is_jumping = false;  
		}
		//finally updates velocity
		AttachedScript.Velocity = new Vector3 (curHorizontalVelocity.x, Velocity.y, curHorizontalVelocity.y);	

	}
}
