using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public GameObject FollowObject;
	public float MoveDelay = 1.0f;
	float yDistance = 0.0f;
	float zDistance = 0.0f;

	private Vector3 Velocity = Vector3.zero;

	// Use this for initialization
	void Start () 
	{
		Velocity = Vector3.zero;
		yDistance = Mathf.Abs(FollowObject.transform.position.y - transform.position.y);
		zDistance = Mathf.Abs(FollowObject.transform.position.z - transform.position.z);
	}

	// Update is called once per frame
	void Update () {

		Vector3 followPos = FollowObject.transform.position;
		followPos.y += yDistance;
		followPos.z -= zDistance;
		

		Vector3 newPostion = Vector3.SmoothDamp(transform.position,
		                                        followPos,
		                                        ref Velocity,
		                                        MoveDelay);
		transform.position = newPostion;
	}
}
