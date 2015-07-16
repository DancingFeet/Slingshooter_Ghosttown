using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {
	
	public GameObject prefabProjectile;
	public float velocityMult = 4f;

	private GameObject launchPoint;
	private Vector3 launchPos;
	private GameObject projectile;
	private bool aimingMode;
	
	void Awake(){
		Transform launchPointTrans = transform.FindChild("LaunchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive(false);
		launchPos = launchPointTrans.position;
	}
	
	void OnMouseEnter() {
		launchPoint.SetActive(true);
	}
	
	void OnMouseExit() {
		if(!aimingMode) 
			launchPoint.SetActive(false);
	}
	
	void OnMouseDown(){
		aimingMode = true;

		projectile = Instantiate(prefabProjectile) as GameObject;

		projectile.transform.position = launchPos;

		projectile.GetComponent<Rigidbody>().isKinematic = true;
	}

	void Update() {
		if(!aimingMode) return;

		Vector3 mousePos = Input.mousePosition;
		mousePos.z = - Camera.main.transform.position.z;
		Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos);

		Vector3 mouseDelta = mousePos3D - launchPos;

		float maxMagnitude = GetComponent<SphereCollider>().radius;
		mouseDelta = Vector3.ClampMagnitude(mouseDelta, maxMagnitude);

		projectile.transform.position = launchPos + mouseDelta;
		
		if(Input.GetMouseButtonUp(0)) {
			aimingMode = false;
			projectile.GetComponent<Rigidbody>().isKinematic = false;
			projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMult;

			FollowCam.S.poi = projectile;

			projectile = null;

			GameController.ShotFired();
		}
		
	}
}
