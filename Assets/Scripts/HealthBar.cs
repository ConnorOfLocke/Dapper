using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour {

	public Entity AttachedEntity;
	public float TimeToChange = 0.5f;

	private Color originalColor;

	private Vector3 originalPosition;
	private float startHealth;
	
	private float AdjustedTimeToChange;
	
	void Start ()
	{
		startHealth = AttachedEntity.Health;
		originalPosition = transform.localPosition;
		AdjustedTimeToChange = TimeToChange;
		originalColor = GetComponent<Image>().color;
	}
	
	void Update () {
		float healthScale = AttachedEntity.Health / startHealth;
		if (transform.localScale.x != healthScale)
		{
			Vector3 Velocity = Vector3.zero;
			GetComponent<RectTransform>().localScale = Vector3.SmoothDamp( GetComponent<RectTransform>().localScale,
																			new Vector3( AttachedEntity.Health / startHealth,1,1),
																			ref Velocity,
			                                                              	AdjustedTimeToChange);
			if (AdjustedTimeToChange > 0.01)
				AdjustedTimeToChange -= Time.deltaTime;
			else		
				AdjustedTimeToChange = 0.01f;		
			
			Vector2 jitter = Random.insideUnitSphere * 10;
			GetComponent<RectTransform>().localPosition = new Vector3 ( originalPosition.x + jitter.x,
			                                                           originalPosition.y + jitter.y,
			                                                           originalPosition.z);			
		}
		else
		{
			if (AttachedEntity.Health / startHealth < 0.2f)
			{
				GetComponent<Image>().color = Color.Lerp(Color.red, originalColor, (Mathf.Sin(2 * Time.time) + 1.0f) * 0.5f);
				Vector2 jitter = Random.insideUnitSphere * 10;
				GetComponent<RectTransform>().localPosition = new Vector3 ( originalPosition.x + jitter.x,
				                                                           originalPosition.y + jitter.y,
				                                                           originalPosition.z);			
			}
			else
			{
				GetComponent<Image>().color = originalColor;
				GetComponent<RectTransform>().localPosition = originalPosition;
			}
			AdjustedTimeToChange = TimeToChange;
		}
		
		
		
	}
}
