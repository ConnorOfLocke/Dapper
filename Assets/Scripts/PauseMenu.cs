using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class PauseMenu : MonoBehaviour {

	public float TimeToRotate = 1.0f;
	
	public bool Active = false;

	private Quaternion ActiveRotation = Quaternion.identity;
	private Quaternion InActiveRotation = Quaternion.identity;

	private bool ButtonPress = false;
	
	public Text MainText;
	private bool MainMenuConfirmation = false;
	
	public Button ContinueButton;
	public Button MainMenuButton;

	public string[] PauseMessages;
		
	private float curTimeRotating = 0.0f;
	// Use this for initialization
	void Start () {
		ActiveRotation = Quaternion.Euler(new Vector3(0, 0, 0));
		InActiveRotation = Quaternion.Euler(new Vector3(0, 180, 179));
	}
	
	// Update is called once per frame
	void Update () {
	
		//Activates the Pause Menu
		if (Input.GetAxis("Pause") > 0 && !ButtonPress)
		{
			if (Active)
			{
				Active = false;
				curTimeRotating = 0;
				GlobalTimeKeeper TimeKeeper =  FindObjectOfType<GlobalTimeKeeper>();
				if (TimeKeeper != null)
					TimeKeeper.UnPause();
					
				ContinueButton.enabled = false;
				MainMenuButton.enabled = false;
				
				ContinueButton.interactable = false;
				MainMenuButton.interactable = false;
				
				FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
			}
			else
			{
				Active = true;
				curTimeRotating = 0;
				GlobalTimeKeeper TimeKeeper =  FindObjectOfType<GlobalTimeKeeper>();
				if (TimeKeeper != null)
					TimeKeeper.Pause();
					
				MainText.text = PauseMessages[Random.Range(0, PauseMessages.Length)];
				ContinueButton.enabled = true;
				MainMenuButton.enabled = true;
				
				ContinueButton.interactable = true;
				MainMenuButton.interactable = true;
				
				FindObjectOfType<EventSystem>().SetSelectedGameObject(ContinueButton.gameObject);
			}
			ButtonPress = true;
		}
		else if (Input.GetAxis("Pause") == 0)
			ButtonPress = false;
	
		//actually rotates it
		float SlerpRatio = (curTimeRotating / TimeToRotate);
		if (Active)
		{
			if (transform.localRotation != ActiveRotation)
				transform.localRotation = Quaternion.Slerp(transform.localRotation, 
																  ActiveRotation,
				                                           			SlerpRatio);
		}
		else
		{
			if (transform.localRotation != InActiveRotation)
				transform.localRotation = Quaternion.Slerp(transform.localRotation, 
					                                              InActiveRotation,
				                                                   SlerpRatio);
		}
		if (curTimeRotating < TimeToRotate)
			curTimeRotating += Time.deltaTime;
		else
			curTimeRotating = 0;
	
	}
	
	public void ChangeState()
	{
		if (Active)
		{
			Active = false;
			curTimeRotating = 0;
			GlobalTimeKeeper TimeKeeper =  FindObjectOfType<GlobalTimeKeeper>();
			if (TimeKeeper != null)
				TimeKeeper.UnPause();
		}
		else
		{
			Active = true;
			curTimeRotating = 0;
			GlobalTimeKeeper TimeKeeper =  FindObjectOfType<GlobalTimeKeeper>();
			if (TimeKeeper != null)
				TimeKeeper.Pause();
			
		}
	}
	
	public void ConfirmMainMenu()
	{
		if (!MainMenuConfirmation)
		{
			MainText.text = "..You sure?..";
			MainMenuConfirmation = true;
		}
		else
			Application.LoadLevel("MenuScene");
	}
	
	
}
