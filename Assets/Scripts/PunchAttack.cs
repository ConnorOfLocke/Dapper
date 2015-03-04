using UnityEngine;
using System.Collections;

public class PunchAttack : MonoBehaviour {

	public enum DIRECTION
	{
		FACE_LEFT,
		FACE_RIGHT
	};
	
	public KeyCode PunchButton;
	
	public float wind_up;
	public float cool_down;
	
	public DIRECTION facing = DIRECTION.FACE_LEFT;
	
	private bool CurrentlyPunching = false;
	private float PunchTimer = 0.0f;
	
	public GameObject PunchObject;
	
	// Update is called once per frame
	void Update () {
		
		if (Input.GetKey(PunchButton) &&
				 !CurrentlyPunching)
		{
			CurrentlyPunching = true;
		}
		
		if (CurrentlyPunching)
		{
			
			
			PunchTimer += Time.deltaTime;
		}
	
	}
}
