using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

	public float Health = 1.0f;
	public GameObject[] HitParticles = null;
	public bool isPlayer = false;
	
	private float FlashTimer = 0.0f;

	private bool BlockPossible;

	private Renderer EntityShader;

	void Start()
	{
		EntityShader = GetComponentInChildren<Renderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (Health <= 0 && !isPlayer)
			Destroy (this.gameObject);

		if (FlashTimer >= 0) 
		{
			//this.GetComponent<SpriteRenderer>().color = Color.red;
			EntityShader.material.color = Color.red;
			FlashTimer -= Time.deltaTime;
		}
		else
		{
			if (EntityShader.material.color == Color.red)
				EntityShader.material.color = Color.white;
		}

		BlockPossible = (GetComponent<CharacterBlock> () != null);
	}

	public void GetHit(float Damage, float FlashTime, DIRECTION HitDirection )
	{
		if (BlockPossible)
		{
			if (GetComponent<CharacterBlock>().IsBlocking)
				return;
		}

		Health -= Damage;
		if (Health < 0) Health = 0;
		
		ChainCounter Counter = FindObjectOfType<ChainCounter>();
		if (Counter != null)
		{
			if (isPlayer)
				Counter.Resetimmediately();
			else
				Counter.AddHit();
		}
		
		FlashTimer = FlashTime;

		if (HitParticles != null)
		{
			foreach (GameObject thing in HitParticles)
			{
				if (HitDirection == DIRECTION.FACE_LEFT)
				{
					Quaternion Left = Quaternion.LookRotation(new Vector3(-1, 0, -0.001f));
					GameObject.Instantiate(thing, transform.position + new Vector3(0, 1, 0), Left);
				}
				else if (HitDirection == DIRECTION.FACE_RIGHT)
				{
					Quaternion Right = Quaternion.LookRotation(new Vector3(1, 0, -0.001f));
					GameObject.Instantiate(thing, transform.position + new Vector3(0, 1, 0), Right);
				}
						
			}
		}

	}
}
