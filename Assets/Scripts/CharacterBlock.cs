using UnityEngine;
using System.Collections;

public class CharacterBlock : MonoBehaviour {

	public float TimeBlocking = 1.0f;
	public float BlockCoolDown = 5.0f;

	private float BlockTimer;
	public bool IsBlocking = false;

	private bool TimeKeeping = false;
	private GlobalTimeKeeper TimeKeeper = null;

	private bool BlockButtonDown = false;

	public GameObject BlockEffect;
	private GameObject CurrentBlockEffect = null;

	private Color OriginalColor;
	private Renderer AttachedMesh;

	void Start () 
	{
		TimeKeeper = FindObjectOfType<GlobalTimeKeeper>();
		TimeKeeping = (TimeKeeper != null);
		AttachedMesh = GetComponentInChildren<Renderer> ();
		OriginalColor = AttachedMesh.material.color;
	}

	void Update () {
	
		float DeltaTime;
		if (TimeKeeping)
			DeltaTime = TimeKeeper.EntityDeltaTime;
		else
			DeltaTime = Time.deltaTime;

		if (BlockTimer - BlockCoolDown > 0)
		{
			AttachedMesh.material.color = Color.blue;
			BlockTimer -= DeltaTime;
		}
		else if (BlockTimer > 0)
		{
			if (AttachedMesh.material.color == Color.blue)
				AttachedMesh.material.color = OriginalColor;
			BlockTimer -= DeltaTime;
		}

		if (BlockTimer < BlockCoolDown)
		{
			IsBlocking = false;
			FindObjectOfType<CharacterMove>().LockMovement  = false;
			
			PunchAttack[] Punches = FindObjectsOfType<PunchAttack>();
			foreach (PunchAttack Attack in Punches)
				Attack.LockAttack = false;

			Destroy(CurrentBlockEffect);
			CurrentBlockEffect = null;
		}

		if (Input.GetAxis("Block") != 0 && !BlockButtonDown && !IsBlocking)
		{
			BlockButtonDown = true;
			IsBlocking = true;
			BlockTimer = TimeBlocking + TimeBlocking;

			//locks player movement and attacking
			FindObjectOfType<CharacterMove>().LockMovement  = true;
			
			PunchAttack[] Punches = FindObjectsOfType<PunchAttack>();
			foreach (PunchAttack Attack in Punches)
				Attack.LockAttack = true;

			CurrentBlockEffect = GameObject.Instantiate(BlockEffect, transform.position, Quaternion.identity) as GameObject;
			CurrentBlockEffect.transform.SetParent(this.gameObject.transform);

		}
		else if (Input.GetAxis("Block") == 0 && BlockButtonDown)
		{
			BlockButtonDown = false;
		}



	}
}
