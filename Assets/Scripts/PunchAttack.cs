using UnityEngine;
using System.Collections;

public class PunchAttack : MonoBehaviour {

	
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
		
		if (Input.GetKey(PunchButton) && !CurrentlyPunching)
			CurrentlyPunching = true;

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
