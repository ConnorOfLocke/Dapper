using UnityEngine;
using System.Collections;

public enum DIRECTION
{
	FACE_LEFT,
	FACE_RIGHT
};

public class Attack : MonoBehaviour {

    public float Force = 1.0f;
    public float Damage = 1.0f;

    public float TimeToExist = 0.5f;

    private float TimeExisting = 0.0f;
    public GameObject Owner;

	public DIRECTION Direction = DIRECTION.FACE_LEFT;

	// Update is called once per frame
	void Update () {

        if (TimeExisting >= TimeToExist)
            DestroyImmediate(this.gameObject);
        TimeExisting += Time.deltaTime;
	}

	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.GetComponent<Entity>() != null)
		{
			if (Owner != collision.gameObject)
			{	
				Vector3 playerDirection = (collision.gameObject.transform.position - transform.position).normalized;

				if (Direction == DIRECTION.FACE_LEFT)
					playerDirection = (playerDirection + new Vector3(1, 0, 0)).normalized;
				else 
					playerDirection = (playerDirection - new Vector3(1, 0, 0)).normalized;

				playerDirection *= Force;
				
				if (collision.gameObject.GetComponent<MovingEntity>() != null)
					collision.gameObject.GetComponent<MovingEntity>().Velocity += playerDirection * Time.deltaTime;

				collision.gameObject.GetComponent<Entity>().Health -= Damage;
			}
		}
	}


}
