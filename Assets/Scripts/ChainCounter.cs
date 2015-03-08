using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChainCounter : MonoBehaviour {

	public Text MainText;
	public Text SubText;
	
	public float TimeToReset = 3.0f;
	public uint curCounter = 0;
	public uint maxCounter = 0;
	
	public float FadeOutTime = 0.5f;
	public float TimeJittering = 0.2f;
	
	private float MainJitterTime = 0.0f;
	private Vector3 MainOriginalPosition;
	private Color MainOriginalColor;

	private float SubJitterTime = 0.0f;
	private Vector3 SubOriginalPosition;
	private Color SubOriginalColor;
			
	private float TimeUntilReset;
	
	void Start ()
	{
		MainOriginalPosition = MainText.gameObject.transform.localPosition;
		MainOriginalColor = MainText.color;
		
		SubOriginalPosition = SubText.gameObject.transform.localPosition;
		SubOriginalColor = SubText.color;
	}
	
	void Update () {
		MainText.text = curCounter.ToString() + " chain";
	
		//jitters the main text
		if (MainJitterTime > 0)
		{
			Vector2 jitter = Random.insideUnitSphere * 10;
			MainText.gameObject.GetComponent<RectTransform>().localPosition = new Vector3 ( MainOriginalPosition.x + jitter.x,
			                                                                               MainOriginalPosition.y + jitter.y,
			                                                                               MainOriginalPosition.z);
			MainJitterTime -= Time.deltaTime;
		}
		else
			MainText.gameObject.GetComponent<RectTransform>().localPosition = MainOriginalPosition;
		
		//jitters the subtext
		if (SubJitterTime > 0 && SubText != null)
		{
			Vector2 jitter = Random.insideUnitSphere * 10;
			SubText.gameObject.GetComponent<RectTransform>().localPosition = new Vector3 ( SubOriginalPosition.x + jitter.x,
			                                                                               SubOriginalPosition.y + jitter.y,
			                                                                               SubOriginalPosition.z);
       		SubJitterTime -= Time.deltaTime;
		}
		else
			SubText.gameObject.GetComponent<RectTransform>().localPosition = SubOriginalPosition;
			
		//resets the texts to 0 and slowly fades out
		if (TimeUntilReset - FadeOutTime <= 0)
		{
			curCounter = 0;
			MainText.color = new Color( MainOriginalColor.r,
			                           MainOriginalColor.g,
			                           MainOriginalColor.b,
			                           MainOriginalColor.a * (TimeUntilReset / FadeOutTime));
			                           
			SubText.color = new Color( MainOriginalColor.r,
				                        MainOriginalColor.g,
				                        MainOriginalColor.b,
				                        MainOriginalColor.a * (TimeUntilReset / FadeOutTime));
		}
		
		if (TimeUntilReset > 0)
			TimeUntilReset -= Time.deltaTime;
		
		//starts the subtext jittering if record broken
		if (curCounter > maxCounter && SubText != null)
		{
			SubText.text = "Best: " + curCounter.ToString();
			maxCounter = curCounter;
			SubJitterTime = TimeJittering;
			SubText.color = SubOriginalColor;
		}
			
	}
	
	public void AddHit()
	{
		MainJitterTime = TimeJittering;
		TimeUntilReset = TimeToReset + FadeOutTime;
		curCounter ++;
		MainText.color = MainOriginalColor;
	}
	
	public void Resetimmediately()
	{
		MainJitterTime = 0;
		TimeUntilReset = FadeOutTime;
	}
}
