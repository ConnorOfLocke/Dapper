using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {

    public float Health = 1.0f;

	// Update is called once per frame
	void Update () {
		if (Health <= 0)
			Destroy (this.gameObject);
	}

}
