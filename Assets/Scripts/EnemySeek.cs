using UnityEngine;
using System.Collections;

public class EnemySeek : MonoBehaviour
{
	public GameObject Target = null;
	public float Speed = 0.8f;
	public float MaxSpeed = 0.1f;
	public float DistanceToFollow = 2.0f;
	public bool CanStopFollowing = true;
	
	public float EntityAvoidRadius = 2.0f;
	public float AvoidanceFactor = 0.01f;
	
	private MovingEntity AttachedScript;
	
	private bool isFollowing = false;
	// Use this for initialization
	void Start ()
	{
		AttachedScript = GetComponent<MovingEntity>();
	}

	// Update is called once per frame
	void Update ()
	{
		if (Target != null)
		{
			if (Vector3.Distance(transform.position, Target.transform.position) < DistanceToFollow)
				isFollowing = true;
			else if (CanStopFollowing)
				isFollowing = false;
				
			if (isFollowing)
			{
				//gets the direction vector from the enemy position to the player * speed
				Vector2 Direction;
				
				Direction = new Vector2(Target.transform.position.x, Target.transform.position.z) -
				 						 new Vector2(transform.position.x, transform.position.z);
				
				
				Direction = Direction.normalized * Speed * Time.deltaTime;
			
				//adds velocity
				Vector3 newVelocity = 	AttachedScript.Velocity + new Vector3(	Direction.x,
																				AttachedScript.Velocity.y,
																				Direction.y );
				//caps it at MaxSpeed
				if (newVelocity.magnitude > MaxSpeed)
				{
					newVelocity = newVelocity.normalized * MaxSpeed;
				}
				if (Vector3.Distance(transform.position, Target.transform.position) < EntityAvoidRadius)
					newVelocity = Vector3.zero;
				
				AttachedScript.Velocity = newVelocity;

			}
			else
			{
				AttachedScript.Velocity = Vector3.zero;
			}
		}
		//Vector3 shyness = Avoidance ();
		//AttachedScript.Velocity += Avoidance() * AvoidanceFactor * Time.deltaTime;
	}

//SLOW AS BUTS
//	Vector3 Avoidance()
//	{
//		MovingEntity[] Entities = FindObjectsOfType<MovingEntity>();
//		Vector3 returnVector = Vector3.zero;
//		
//		foreach (MovingEntity Entity in Entities)
//		{
//			float Distance = Vector3.Distance( Entity.gameObject.transform.position, transform.position); 
//			if (Distance < EntityAvoidRadius)
//			{
//				AttachedScript.Velocity += (transform.position - Entity.gameObject.transform.position) * (EntityAvoidRadius - Distance);
//			}
//		}
//		if (returnVector.magnitude > 1)
//			return returnVector.normalized;
//		else
//			return returnVector;
//	}
}

