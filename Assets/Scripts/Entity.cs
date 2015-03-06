using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public float Health = 1.0f;
	public GameObject[] HitParticles = null;
	public bool isPlayer = false;
	
	private float FlashTimer = 0.0f;
	
	// Update is called once per frame
	void Update () {
		if (Health <= 0 && !isPlayer)
			Destroy (this.gameObject);

		if (FlashTimer >= 0) 
		{
			this.GetComponent<SpriteRenderer>().color = Color.red;
			FlashTimer -= Time.deltaTime;
			
		}
		else
			this.GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void GetHit(float Damage, float FlashTime, DIRECTION HitDirection )
	{
		Health -= Damage;
		FlashTimer = FlashTime;

		if (HitParticles != null)
		{
			foreach (GameObject thing in HitParticles)
			{
				if (HitDirection == DIRECTION.FACE_LEFT)
				{
					Quaternion Left = Quaternion.LookRotation(new Vector3(-1, 0, 0));
					GameObject.Instantiate(thing, transform.position + new Vector3(0, 1, 0), Left);
				}
				else if (HitDirection == DIRECTION.FACE_RIGHT)
				{
					Quaternion Right = Quaternion.LookRotation(new Vector3(1, 0, 0));
					GameObject.Instantiate(thing, transform.position + new Vector3(0, 1, 0), Right);
				}
						
			}
		}

	}
}
