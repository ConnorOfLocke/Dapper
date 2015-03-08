using UnityEngine;
using System.Collections;

public class MovingEntity : MonoBehaviour {

	public Vector3 Velocity;

	public float near_z_limit;
	public float far_z_limit;
	
	public float right_x_limit;
	public float left_x_limit;

	public float gravity = 1.0f;

	public float y_floor = 0;
	
	private bool TimeKeeping = false;
	private GlobalTimeKeeper TimeKeeper = null;
	
	void Start ()
	{
		TimeKeeper = FindObjectOfType<GlobalTimeKeeper>();
		TimeKeeping = (TimeKeeper != null);
	}
	
	void Update () {
	
		float DeltaTime;
		if (TimeKeeping)
			DeltaTime = TimeKeeper.EntityDeltaTime;
			
		else
			DeltaTime = Time.deltaTime;
	
	
		//updates our position 
		//GetComponent<Rigidbody>().AddForce(Velocity * 100);
		if (TimeKeeping)
			transform.position += Velocity * TimeKeeper.CurDeltaRatio;
		else
			transform.position += Velocity;
		
		//moves it back from the limits
		if (transform.position.x < left_x_limit)
			transform.position = new Vector3(left_x_limit, transform.position.y, transform.position.z);
		else if (transform.position.x > right_x_limit)
			transform.position = new Vector3(right_x_limit, transform.position.y, transform.position.z);
		
		if (transform.position.z < near_z_limit)
			transform.position = new Vector3(transform.position.x, transform.position.y, near_z_limit);
		else if (transform.position.z > far_z_limit)
			transform.position = new Vector3(transform.position.x, transform.position.y, far_z_limit);

		if (transform.position.y > y_floor)
			Velocity.y -= gravity * DeltaTime;

		if (transform.position.y < y_floor)
		{
			Velocity.y = 0;
			transform.position = new Vector3(transform.position.x, y_floor, transform.position.z);
		}
		
		
	}
}
