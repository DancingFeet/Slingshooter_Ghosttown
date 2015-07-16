using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

	public GameObject target;

	void Update () 
	{
		transform.Rotate (4f, 0f, 0f);
		transform.Translate (0f, 0.4f, 0f);
	
	}

	void OnTriggerEnter(Collider other) {
		Destroy (target);
	}
	
}
