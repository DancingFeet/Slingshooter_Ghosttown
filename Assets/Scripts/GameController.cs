using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState {
	idle,
	playing,
	levelEnd
}

public class GameController : MonoBehaviour {

	public static GameController S;

	public GameObject[] castles;
	public Text gtLevel; 
	public Text gtScore; 
	public Vector3 castlePos; 
	
	public int level; 
	public int levelMax; 
	public int shotsTaken;
	public GameObject castle; 
	public GameState state = GameState.idle;
	public string showing = "Slingshot";
	
	void Start(){
		S = this;
		level = 0;
		levelMax = castles.Length;
		StartLevel();
	}

	void StartLevel() {
		if(castle != null) {
			Destroy (castle);
		}

		GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
		foreach(GameObject p in projectiles){
			Destroy(p);
		}

		castle = Instantiate (castles[level]) as GameObject;
		castle.transform.position = castlePos;
		shotsTaken = 0;

		SwitchView("Both");
		ProjectileLine.S.Clear();

		Goal.goalMet = false;
		UpdateGT();

		state = GameState.playing;
	
	}

	void UpdateGT() {
		gtLevel.text = "Level:" + (level+1) + " of " + levelMax;
		gtScore.text = "Shots Taken: " + shotsTaken;
	}

	void Update() {
		UpdateGT();

		if(state == GameState.playing && Goal.goalMet) {
			if(FollowCam.S.poi.tag == "Projectile" &&  FollowCam.S.poi.GetComponent<Rigidbody>().IsSleeping()) {
		
				state = GameState.levelEnd;
			
				SwitchView("Both");
		
				Invoke("NextLevel", 2f);
			}
		}
	}

	void NextLevel() {
		level++;
		if(level == levelMax){
			level = 0;
		}
		StartLevel();
	}
	
	public void SwitchView(string view) {
		S.showing = view;
		switch(S.showing){
		case "Slingshot":
			FollowCam.S.poi = null;
			break;
		case "Castle":
			FollowCam.S.poi = S.castle;
			break;
		case "Both":
			FollowCam.S.poi = GameObject.Find ("ViewBoth");
			break;
		}
	}

	public static void ShotFired(){
		S.shotsTaken++;
	}


}
