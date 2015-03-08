using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartMenu : MonoBehaviour {

	public Text MainText;
	public string[] StartMessages;
	
	public float TimeToStart = 3.0f;

	private Quaternion InActiveRotation = Quaternion.identity;
	public float TimeToRotate = 1.0f;
	float CurTimeRotating = 0.0f;
	
	// Use this for initialization
	void Start () {
		MainText.text = StartMessages[Random.Range(0, StartMessages.Length)];
		
		InActiveRotation = Quaternion.Euler(new Vector3(0, 180, 179));
		CurTimeRotating = 0.0f;
		GlobalTimeKeeper TimeKeeper =  FindObjectOfType<GlobalTimeKeeper>();
		if (TimeKeeper != null)
			TimeKeeper.Pause();
	}
	
	// Update is called once per frame
	void Update () {
		
		if (TimeToStart <= 0)
		{
			
			GlobalTimeKeeper TimeKeeper =  FindObjectOfType<GlobalTimeKeeper>();
			if (TimeKeeper != null)
				TimeKeeper.UnPause();
		
			float SlerpRatio = (CurTimeRotating / TimeToRotate);
		
			transform.localRotation = Quaternion.Slerp(transform.localRotation, 
			                                           InActiveRotation,
			                                           SlerpRatio);
			if (CurTimeRotating < TimeToRotate)
				CurTimeRotating += Time.deltaTime;
			else
				Destroy(this.gameObject);
		}
		else
		{
			TimeToStart -= Time.deltaTime;
			GlobalTimeKeeper TimeKeeper =  FindObjectOfType<GlobalTimeKeeper>();
			if (TimeKeeper != null)
				TimeKeeper.Pause();
		}
	
	}
}
