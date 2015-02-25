using UnityEngine; 
using System.Collections;

public class CharacterMove : MonoBehaviour {

	public float speed = 2.5f;
	public float max_speed = 5.0f;
	public float jump_velocity;
	public float gravity;
	public float friction = 1.0f;
	
	public float y_floor = 0;
	
	public float near_z_limit;
	public float far_z_limit;
	
	public float right_x_limit;
	public float left_x_limit;
	
	private Vector3 m_Velocity;
	private bool m_is_jumping = false;

	void Start()
	{
		m_Velocity = Vector3.zero;
	}

	// Update is called once per frame
	void Update () 
	{
		Vector2 curHorizontalVelocity = new Vector2(m_Velocity.x, m_Velocity.z);
		
		//PC INPUT
		if (Input.GetKey (KeyCode.LeftArrow) )
			curHorizontalVelocity.x -= speed * Time.deltaTime;
		
		if (Input.GetKey (KeyCode.RightArrow))
			curHorizontalVelocity.x += speed * Time.deltaTime;

		if (Input.GetKey (KeyCode.UpArrow))
			curHorizontalVelocity.y += speed * Time.deltaTime * 0.5f;

		if (Input.GetKey (KeyCode.DownArrow))
			curHorizontalVelocity.y -= speed * Time.deltaTime * 0.5f;


		//limits the horizontal velocity by maxspeed as a max Magnitude
		if (curHorizontalVelocity.magnitude != 0)
		{	
			//clamps at maxSpeed
			curHorizontalVelocity = Vector2.ClampMagnitude( curHorizontalVelocity, max_speed * Time.deltaTime);
		
			Vector2 friction_vec = (curHorizontalVelocity.normalized) * friction * Time.deltaTime;
			if (m_is_jumping)
				friction_vec *= 2;
				
			//substracts friction
			if (curHorizontalVelocity.magnitude - friction * Time.deltaTime > 0)
				curHorizontalVelocity -= friction_vec;
			else
				curHorizontalVelocity = Vector2.zero;
		}
		
		
		//Jump
		if (Input.GetKey (KeyCode.Space) && !m_is_jumping)
		{
			m_Velocity.y += jump_velocity; 
			m_is_jumping = true;
		}
		
		if (m_Velocity.y != 0)
		{
			//decreases by gravity
			if ( transform.position.y + (m_Velocity.y - gravity * Time.deltaTime) > y_floor)
				m_Velocity.y -= gravity * Time.deltaTime;
			else
			{
				//rounds to y_floor and resets jump
				m_Velocity.y = 0;
				transform.position = new Vector3(transform.position.x, y_floor, transform.position.z);
				m_is_jumping = false;  
			}
		}
		//finally updates velocity
		m_Velocity = new Vector3(curHorizontalVelocity.x, m_Velocity.y, curHorizontalVelocity.y );
	 	
	 	//updates our position 
		transform.position += m_Velocity;
		//moves it back from the limits
		if (transform.position.x < left_x_limit)
			transform.position = new Vector3(left_x_limit, transform.position.y, transform.position.z);
		else if (transform.position.x > right_x_limit)
			transform.position = new Vector3(right_x_limit, transform.position.y, transform.position.z);
		
		if (transform.position.z < near_z_limit)
			transform.position = new Vector3(transform.position.x, transform.position.y, near_z_limit);
		else if (transform.position.z > far_z_limit)
			transform.position = new Vector3(transform.position.x, transform.position.y, far_z_limit);
			
		
	}
}
