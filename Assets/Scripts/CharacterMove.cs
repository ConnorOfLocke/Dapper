using UnityEngine; 
using System.Collections;

public class CharacterMove : MonoBehaviour {

	public float Speed = 5.0f;
	public float ZSpeed = 3.0f;
	

	// Update is called once per frame
	void Update () 
	{
		Vector3 newPos = transform.position;
		if (Input.GetKey (KeyCode.LeftArrow) )
		{
			newPos.x += Speed * Time.deltaTime;
		}
		
		if (Input.GetKey (KeyCode.RightArrow))
		{
			newPos.x -= Speed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.UpArrow))
		{
			newPos.z -= ZSpeed * Time.deltaTime;
		}

		if (Input.GetKey (KeyCode.DownArrow))
		{
			newPos.z += ZSpeed * Time.deltaTime;
		}

		transform.position = newPos;
	}
}
