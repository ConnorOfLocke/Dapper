using UnityEngine;
using System.Collections;

public class PunchAttack : MonoBehaviour {

	public enum ATTACK_TYPE
	{
		LIGHT_ATTACK,
		HEAVY_ATTACK
	};

	public ATTACK_TYPE AttackType = ATTACK_TYPE.LIGHT_ATTACK;
	public float DistanceToSpawnPunch = 1.0f;
	
	public KeyCode PunchButton;
	
	public float wind_up;
	public float cool_down;
	
	public DIRECTION facing = DIRECTION.FACE_LEFT;
	
	private bool CurrentlyPunching = false;
	private float PunchTimer = 0.0f;
	
	public GameObject PunchObject;
	private bool justPunched = false;
	// Update is called once per frame
	void Update () {

		//update direction facing
		Vector3 curVelocity = GetComponent<MovingEntity> ().Velocity;
		if (curVelocity.x > 0)
			facing = DIRECTION.FACE_RIGHT;
		else if (curVelocity.x < 0)
			facing = DIRECTION.FACE_LEFT;

		if (AttackType == ATTACK_TYPE.LIGHT_ATTACK)
		{
			if (Input.GetAxis("LightAttack") != 0 && !CurrentlyPunching)
				CurrentlyPunching = true;
		}
		else if (AttackType == ATTACK_TYPE.HEAVY_ATTACK)
		{
			if (Input.GetAxis("HeavyAttack") != 0 && !CurrentlyPunching)
				CurrentlyPunching = true;
		}

		if (CurrentlyPunching)
		{
			if (PunchTimer > wind_up + cool_down)			
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

			PunchTimer += Time.deltaTime;
		}
	}


}
