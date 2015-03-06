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
	public float ReactionPauseTime = 1.0f;


    public float TimeToExist = 0.5f;

    private float TimeExisting = 0.0f;
    public GameObject Owner;

	public float FlashTime = 0.1f;

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
				if (collision.gameObject.GetComponent<MovingEntity>() != null)
				{

					Vector3 NewVelocity;
					if (Direction == DIRECTION.FACE_LEFT)
						NewVelocity = new Vector3(-1, 0, 0) * Force * Time.deltaTime;
					else 
						NewVelocity = new Vector3(1, 0, 0) * Force * Time.deltaTime;

					NewVelocity -= collision.GetComponent<MovingEntity>().Velocity;
					NewVelocity.y += 0.2f;
		
					collision.gameObject.GetComponent<MovingEntity>().Velocity = NewVelocity;
				}

				collision.gameObject.GetComponent<Entity>().GetHit(Damage, FlashTime, Direction);

				if (collision.gameObject.GetComponent<EnemySeek>() != null)
					collision.gameObject.GetComponent<EnemySeek>().PauseReact(ReactionPauseTime);

				if (collision.gameObject.GetComponent<EnemyPunch>() != null)
					collision.gameObject.GetComponent<EnemyPunch>().Interupt(ReactionPauseTime);

			}
		}
	}


}
