using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EndMenu : MonoBehaviour {

	public Button RetryButton;
	public Button MainMenuButton;

	public Entity Player;

	private bool EndGame = false;
	
	private Quaternion ActiveRotation = Quaternion.identity;
	public float TimeToRotate = 1.0f;
	private float CurTimeRotating = 0.0f;

	void Start () 
	{
		ActiveRotation = Quaternion.Euler (new Vector3 (0, 0, 0));

		RetryButton.enabled = false;
		MainMenuButton.enabled = false;
		
		RetryButton.interactable = false;
		MainMenuButton.interactable = false;
	}
	
	void Update () 
	{
		if (EndGame)
		{
			if (CurTimeRotating < TimeToRotate)
			{
				float SlerpRatio = (CurTimeRotating / TimeToRotate);
				
				transform.localRotation = Quaternion.Slerp(transform.localRotation, 
				                                           ActiveRotation,
				                                           SlerpRatio);
				CurTimeRotating += Time.deltaTime;
			}
		}
		else
		{
			if (Player.Health <= 0)
			{
				EndGame = true;

				RetryButton.enabled = true;
				MainMenuButton.enabled = true;
				
				RetryButton.interactable = true;
				MainMenuButton.interactable = true;
				
				FindObjectOfType<EventSystem>().SetSelectedGameObject(RetryButton.gameObject);

				FindObjectOfType<CharacterMove>().LockMovement  = true;
				
				PunchAttack[] Punches = FindObjectsOfType<PunchAttack>();
				foreach (PunchAttack Attack in Punches)
					Attack.LockAttack = true;
				
			}
		}


	}
}
