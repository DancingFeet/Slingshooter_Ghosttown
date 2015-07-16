# Slingshooter_Ghosttown

Hallo Martin!

Mein Feature besteht aus Geistern, welche dem geworfenen Ball ein Hindernis darstellen.
Der Geist rotiert in der Luft. Wenn der Ball ihn trifft verschwindet der Geist und gleichzeitig prallt der Ball an ihm ab.


	public GameObject target;

// Hier rotiert der Geist um einen pivot Point
	void Update () 
	{
		transform.Rotate (4f, 0f, 0f);
		transform.Translate (0f, 0.4f, 0f);
	
	}
//Wenn ein Object mit dem Geist in Berührung kommt, wird dieser entfernt
	void OnTriggerEnter(Collider other) {
		Destroy (target);
	}
	
