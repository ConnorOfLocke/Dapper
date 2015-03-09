using UnityEngine;
using System.Collections;

public class TurnTable : MonoBehaviour {

	public float TurnSpeed = 2000f;
	public MovingEntity AttachedObject = null;

	private DIRECTION facing = DIRECTION.FACE_RIGHT;

	private Quaternion RightFace = Quaternion.identity;
	private Quaternion LeftFace = Quaternion.identity;

	bool TimeKeeping = false;
	GlobalTimeKeeper TimeKeeper = null;

	void Start ()
	{
		RightFace.SetLookRotation (new Vector3 (0, 0, -1));
		LeftFace.SetLookRotation (new Vector3 (0, 0, 1));
		TimeKeeper = FindObjectOfType<GlobalTimeKeeper>();
		TimeKeeping = (TimeKeeper != null);
	}

	// Update is called once per frame
	void Update () {		
	
		float DeltaTime;
		if (TimeKeeping)
			DeltaTime = TimeKeeper.EntityDeltaTime;
		else
			DeltaTime = Time.deltaTime;
	
		if (AttachedObject != null)
		{
			Vector3 curVelocity = AttachedObject.GetComponent<MovingEntity>().Velocity;

			if (curVelocity.x > 0)
				facing = DIRECTION.FACE_RIGHT;
			if (curVelocity.x < 0)
				facing = DIRECTION.FACE_LEFT;

			Quaternion curRotation = transform.rotation;

			if (transform.rotation != LeftFace && facing == DIRECTION.FACE_LEFT)
				transform.rotation = Quaternion.RotateTowards(curRotation, LeftFace, TurnSpeed * DeltaTime);
			
			if (transform.rotation != RightFace  && facing == DIRECTION.FACE_RIGHT)
				transform.rotation = Quaternion.RotateTowards(curRotation, RightFace, TurnSpeed * DeltaTime);
				

			//for mirroring the image
			/*
			if (transform.rotation.eulerAngles.y > 90) ///left
				transform.localScale = new Vector3(-1 ,transform.localScale.y , transform.localScale.z);
			else if (transform.rotation.eulerAngles.y < 90) //right
				transform.localScale = new Vector3(1 ,transform.localScale.y , transform.localScale.z); */
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
				transform.rotation = Quaternion.RotateTowards(curRotation, LeftFace, TurnSpeed * DeltaTime);

			if (transform.rotation != RightFace  && facing == DIRECTION.FACE_RIGHT)
				transform.rotation = Quaternion.RotateTowards(curRotation, RightFace, TurnSpeed * DeltaTime);
				
			//for mirroring the image
			/*
			if (transform.rotation.eulerAngles.y > 90) ///left
				transform.localScale = new Vector3(-1 ,transform.localScale.y , transform.localScale.z);
			else if (transform.rotation.eulerAngles.y < 90) //right
				transform.localScale = new Vector3(1 ,transform.localScale.y , transform.localScale.z); */
				
				
		}
			                                    

	}
}
