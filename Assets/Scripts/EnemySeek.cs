using UnityEngine;
using System.Collections;

public class EnemySeek : MonoBehaviour
{
	public GameObject Target = null;
	public float Speed = 0.8f;
	public float MaxSpeed = 0.1f;
	public float Friction = 0.5f;
	
	public float DistanceToFollow = 2.0f;
	public bool CanStopFollowing = true;
	
	public float EntityAvoidRadius = 10.0f;
	
	private MovingEntity AttachedScript;
	
	private bool isFollowing = false;

	public float SeekCoolDownTimer = 1.0f;
	private float SeekCoolDown = 0.0f;
	
	private bool TimeKeeping = false;
	private GlobalTimeKeeper TimeKeeper = null;
	
	void Start ()
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
	
		if (DeltaTime != 0)
		{
			if (Target != null)
			{
				if (SeekCoolDown <= 0)
				{
					if (Vector3.Distance(transform.position, Target.transform.position) < DistanceToFollow)
						isFollowing = true;
					else if (CanStopFollowing)
						isFollowing = false;
						
					if (isFollowing)
					{
						//gets the direction vector from the enemy position to the player * speed
						Vector2 curDirection = new Vector2(AttachedScript.Velocity.x, AttachedScript.Velocity.z);
	
						Vector2 newDirection = curDirection + 
												(new Vector2(Target.transform.position.x, Target.transform.position.z) -
						 						 new Vector2(transform.position.x, transform.position.z)).normalized * Speed * DeltaTime;
						 						 
	
						//caps it at MaxSpeed
						if (newDirection.magnitude > MaxSpeed)
						{
							newDirection = newDirection.normalized * MaxSpeed;
						}
	
						if (Vector3.Distance(transform.position, Target.transform.position) < EntityAvoidRadius)
							newDirection = Vector2.zero;
			
					
						//adds velocity
						Vector3 newVelocity = new Vector3(	newDirection.x,
															AttachedScript.Velocity.y,
	                                             			newDirection.y );
						AttachedScript.Velocity = newVelocity;
	
					}
					else
					{
						AttachedScript.Velocity = Vector3.zero;
					}
				}
				else
					SeekCoolDown -= Time.deltaTime;
			}
		}
		//Vector3 shyness = Avoidance ();
		//AttachedScript.Velocity += Avoidance() * AvoidanceFactor * Time.deltaTime;
	}

	public void PauseReact(float stopTime)
	{
		SeekCoolDownTimer = stopTime;
		SeekCoolDown = stopTime;
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

