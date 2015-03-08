using UnityEngine;
using System.Collections;

public class EnemyPunch : MonoBehaviour {

	public float DistanceToSpawnPunch = 1.0f;
	
	public float wind_up = 2.0f;
	public float cool_down = 0.2f;
	
	public GameObject PunchWarning;

	public DIRECTION facing = DIRECTION.FACE_LEFT;
	
	private bool CurrentlyPunching = false;
	private float PunchTimer = 0.0f;
	
	public GameObject PunchObject;
	private bool justPunched = false;

	private float InteruptTimer = 0.0f;
	
	private bool TimeKeeping = false;
	private GlobalTimeKeeper TimeKeeper = null;
	
	void Start()
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
		
		//update direction facing
		if (GetComponent<EnemySeek>().Target.transform.position.x > transform.position.x)
			facing = DIRECTION.FACE_RIGHT;
		else if (GetComponent<EnemySeek>().Target.transform.position.x < transform.position.x)
			facing = DIRECTION.FACE_LEFT;
		
		//update direction facing
		//Vector3 curVelocity = GetComponent<MovingEntity> ().Velocity;
		//if (curVelocity.x > 0)
		//	facing = DIRECTION.FACE_RIGHT;
		//else if (curVelocity.x < 0)
		//	facing = DIRECTION.FACE_LEFT;

		if (InteruptTimer <= 0)
		{
			float DistanceToTarget = Vector3.Distance (GetComponent<EnemySeek>().Target.transform.position, transform.position);
			float DistanceToStartPunching = GetComponent<EnemySeek> ().EntityAvoidRadius;

			if ( DistanceToTarget < DistanceToStartPunching && !CurrentlyPunching)
			{
				CurrentlyPunching = true;
				GetComponent<EnemySeek>().PauseReact(wind_up + cool_down);
				GameObject.Instantiate(PunchWarning, transform.position + new Vector3(0,2,0), Quaternion.identity);
			}
			
			if (CurrentlyPunching)
			{
				if (DistanceToTarget > DistanceToStartPunching * 1.5f)
				{					
					CurrentlyPunching = false;
					PunchTimer = 0.0f;
					justPunched = false;

				}
				else if (PunchTimer > wind_up + cool_down)			
				{
					CurrentlyPunching = false;
					PunchTimer = 0.0f;
					justPunched = false;
				}
				else if (PunchTimer > wind_up && !justPunched)
				{
					GameObject Punchy;
					if (facing == DIRECTION.FACE_LEFT)
						Punchy = GameObject.Instantiate(PunchObject, transform.position - new Vector3(DistanceToSpawnPunch, 0, 0), Quaternion.identity) as GameObject;
					else
						Punchy = GameObject.Instantiate(PunchObject, transform.position + new Vector3(DistanceToSpawnPunch, 0, 0), Quaternion.identity) as GameObject;
					
					Attack PunchyAttack = Punchy.GetComponent<Attack>();
					if (PunchyAttack != null)
					{
						PunchyAttack.Owner = this.gameObject;
						PunchyAttack.Direction = facing;
					}
					justPunched = true;
				}
				
				PunchTimer += DeltaTime;
			}
		}
		else
			InteruptTimer -= DeltaTime;
	}

	public void Interupt(float timeToCoolOff)
	{
		InteruptTimer = timeToCoolOff;
		CurrentlyPunching = false;
	}

}
