using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeOutToDestroy : MonoBehaviour {

	public float TimeToExist = 1.0f;
	private float Timer;
	
	void Start(){
		Timer = TimeToExist;
	}
	// Update is called once per frame
	void Update () {
		if (GetComponent<Image>() != null)
		{
			Color prevColor = GetComponent<Image>().color;
			GetComponent<Image>().color = new Color(prevColor.r,
													prevColor.g,
													prevColor.b,
													prevColor.a * (Timer / TimeToExist));
		}
		
		Timer -= Time.deltaTime;
		if (Timer <= 0)
			Destroy(this.gameObject);
	
	}
}
