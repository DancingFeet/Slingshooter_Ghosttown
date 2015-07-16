using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {
	
	public static bool goalMet = false;

	void OnTriggerEnter(Collider other) {

		if(other.gameObject.tag == "Projectile") {
			goalMet = true;
			Color c = GetComponent<Renderer>().material.color;
			c.a = 1.0f;
			GetComponent<Renderer>().material.color = c;
		}
	}
}
