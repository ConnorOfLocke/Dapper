using UnityEngine;
using System.Collections;

public class UVCycler : MonoBehaviour {

	public float CycleTime = 1.0f;
	public bool CycleX = false;
	public bool CycleY = true;


	private Renderer AttachedRenderer;
	// Use this for initialization
	void Start () {
		AttachedRenderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 curOffset = AttachedRenderer.material.mainTextureOffset;
		if (CycleX)
			curOffset.x += Time.deltaTime * CycleTime;
		if (CycleY)
			curOffset.y += Time.deltaTime * CycleTime;

		AttachedRenderer.material.mainTextureOffset = curOffset;
	}
}
