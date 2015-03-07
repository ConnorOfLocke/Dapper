using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Entity AttachedEntity;
	public float TimeToChange = 0.5f;
	public bool FadeIfInactive = false;
	public float TimeToFade = 5.0f;

	private Color originalColor;

	private Vector3 originalPosition;
	private Vector3 originalScale;
	private float startHealth;
	
	private float AdjustedTimeToScale;
	private float CurTimeToFade;
	void Start ()
	{
		startHealth = AttachedEntity.Health;
		originalPosition = transform.localPosition;
		originalScale = transform.localScale;
		
		AdjustedTimeToScale = TimeToChange;
		originalColor = GetComponent<Image>().color;
		
		CurTimeToFade = 0;
	}
	
	void Update () {
		float healthScale = AttachedEntity.Health / startHealth;
		//compare scales w/ rouding errors in check
		if (Mathf.Round((transform.localScale.x) * 1000) != Mathf.Round(healthScale * originalScale.x * 1000))
		{
		
			Vector3 Velocity = Vector3.zero;
			//scales the health bar
			GetComponent<RectTransform>().localScale = Vector3.SmoothDamp( GetComponent<RectTransform>().localScale,
			                                                              new Vector3( healthScale * originalScale.x, originalScale.y , originalScale.z),
																		   ref Velocity,
			                                                               AdjustedTimeToScale);
			//speeds it up as it changes
			if (AdjustedTimeToScale > 0.01)
				AdjustedTimeToScale -= Time.deltaTime;
			else		
				AdjustedTimeToScale = 0.01f;		
				
			//resets the colour
			GetComponent<Image>().color = originalColor;
			
			//jitters a random amount
			Vector2 jitter = Random.insideUnitSphere * 10;
			GetComponent<RectTransform>().localPosition = new Vector3 ( originalPosition.x + jitter.x,
			                                                           originalPosition.y + jitter.y,
			                                                           originalPosition.z);
			//resets the alpha cuz somthing happened                                                        	
			if (FadeIfInactive)
				CurTimeToFade = TimeToFade;
		}
		else
		{
			if (healthScale < 0.2f)
			{
				//jitters the position and flashs red in below %20 health
				GetComponent<Image>().color = Color.Lerp(Color.red, originalColor, (Mathf.Sin(2 * Time.time) + 1.0f) * 0.5f);
				Vector2 jitter = Random.insideUnitSphere * 10;
				GetComponent<RectTransform>().localPosition = new Vector3 ( originalPosition.x + jitter.x,
				                                                           originalPosition.y + jitter.y,
				                                                        	originalPosition.z);
				//resets the alpha cuz somthing happened                                                        	
				if (FadeIfInactive)
					CurTimeToFade = TimeToFade;
				
			}
			else
			{
				//resets the color
				if (FadeIfInactive)
				{
					GetComponent<Image>().color = new Color(originalColor.r,
					                                        originalColor.g,
					                                        originalColor.b,
					                                        CurTimeToFade / TimeToFade);
					CurTimeToFade -= Time.deltaTime;
				}
				else
					GetComponent<Image>().color = originalColor;

				GetComponent<RectTransform>().localPosition = originalPosition;
				
			}
			AdjustedTimeToScale = TimeToChange;
		}
	}
}
