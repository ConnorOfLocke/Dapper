using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public GameObject FollowObject;
	Vector3 CurPosition;
	float yDistance = 0.0f;

	private Vector3 Velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		CurPosition = FollowObject.transform.position;
		Velocity = Vector3.zero;
		yDistance = FollowObject.transform.position.y - transform.position.y;

	}

	// Update is called once per frame
	void Update () {

		Vector3 followPos = FollowObject.transform.position;
		followPos.z = transform.position.z;
		followPos.y -= yDistance;

		Vector3 newPostion = Vector3.SmoothDamp(transform.position,
		                                        followPos,
		                                        ref Velocity,
		                                        Time.deltaTime);
		transform.position = newPostion;
	}
}
