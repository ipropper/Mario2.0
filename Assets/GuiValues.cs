using UnityEngine;
using System.Collections;


public class GuiValues : MonoBehaviour{

	public static int points=0;
	public static int coins=0;
	//public static int world=0;
	//public static int stage=0;
	static int totalTime=400;
	static float startTime;
	public static int numLives=3;
	public static int timeLeft;

	
	//for respawn
	public static Vector3 respawnPoint;
	public static bool firstSpawn=true;

	static bool isRespawning=false;

	void Start(){

		isRespawning=false;
		startTime = Time.time;

	}

	void Update(){

		timeLeft = (int)(totalTime + (startTime - Time.time));
	}

	public static void respawn(){
		if(isRespawning){
			return;
		}

		isRespawning = true;
		numLives--;
		Debug.Log (numLives + "lives left");
		if(numLives<0){
			points=0;
			coins=0;
			numLives=3;
			firstSpawn=true;
		}
		Application.LoadLevel(Application.loadedLevel);
	}
}
