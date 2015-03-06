using UnityEngine;
using System.Collections;

public class TurnTable : MonoBehaviour {

	public float TurnSpeed = 2000f;
	public MovingEntity AttachedObject = null;

	private DIRECTION facing = DIRECTION.FACE_RIGHT;

	private Quaternion RightFace = Quaternion.identity;
	private Quaternion LeftFace = Quaternion.identity;

	void Start ()
	{
		RightFace.SetLookRotation (new Vector3 (0, 0, -1));
		LeftFace.SetLookRotation (new Vector3 (0, 0, 1));
	}

	// Update is called once per frame
	void Update () {
	
		if (AttachedObject != null)
		{
			Vector3 curVelocity = AttachedObject.GetComponent<MovingEntity>().Velocity;

			if (curVelocity.x > 0)
				facing = DIRECTION.FACE_RIGHT;
			if (curVelocity.x < 0)
				facing = DIRECTION.FACE_LEFT;

			Quaternion curRotation = transform.rotation;

			if (transform.rotation != LeftFace && facing == DIRECTION.FACE_LEFT)
				transform.rotation = Quaternion.RotateTowards(curRotation, LeftFace, TurnSpeed * Time.deltaTime);
			
			if (transform.rotation != RightFace  && facing == DIRECTION.FACE_RIGHT)
				transform.rotation = Quaternion.RotateTowards(curRotation, RightFace, TurnSpeed * Time.deltaTime);
      	}
		else
		{
			Vector3 curVelocity = GetComponent<MovingEntity>().Velocity;
			
			if (curVelocity.x > 0)
				facing = DIRECTION.FACE_RIGHT;
			else if (curVelocity.x < 0)
				facing = DIRECTION.FACE_LEFT;
			
			Quaternion curRotation = transform.rotation;
			
			if (transform.rotation != LeftFace && facing == DIRECTION.FACE_LEFT)
				transform.rotation = Quaternion.RotateTowards(curRotation, LeftFace, TurnSpeed * Time.deltaTime);

			if (transform.rotation != RightFace  && facing == DIRECTION.FACE_RIGHT)
				transform.rotation = Quaternion.RotateTowards(curRotation, RightFace, TurnSpeed * Time.deltaTime);
		}
			                                    

	}
}
