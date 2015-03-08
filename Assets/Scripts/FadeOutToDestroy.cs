using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutToDestroy : MonoBehaviour {

	public float TimeToExist = 1.0f;
	private float Timer;
	
	private bool TimeKeeping = false;
	private GlobalTimeKeeper TimeKeeper = null;
	
	void Start(){
		Timer = TimeToExist;
		TimeKeeper = FindObjectOfType<GlobalTimeKeeper>();
		TimeKeeping = (TimeKeeper != null);
	}
	// Update is called once per frame
	void Update () {
	
		float DeltaTime;
		if (TimeKeeping)
			DeltaTime = TimeKeeper.EntityDeltaTime;
		else
			DeltaTime = Time.deltaTime;
	
		if (GetComponent<SpriteRenderer>() != null)
		{
			Color prevColor = GetComponent<SpriteRenderer>().color;
			GetComponent<SpriteRenderer>().color = new Color(prevColor.r,
													prevColor.g,
													prevColor.b,
													prevColor.a * (Timer / TimeToExist));
		}
		
		if (GetComponent<Image>() != null)
		{
			Color prevColor = GetComponent<Image>().color;
			GetComponent<Image>().color = new Color(prevColor.r,
			                                        prevColor.g,
			                                        prevColor.b,
			                                        prevColor.a * (Timer / TimeToExist));
		}
		
		Timer -= DeltaTime;
		if (Timer <= 0)
			Destroy(this.gameObject);
	
	}
}
