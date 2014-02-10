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

	public static int maxPoints = 0;

	public static string world;
	public static int scene;

	
	//for respawn
	public static Vector3 respawnPoint;
	public static bool firstSpawn=true;

	static bool isRespawning=false;

	void Start(){

		isRespawning=false;
		startTime = Time.time;

	}

	void Update(){

		if(Application.loadedLevel == 1){
			startTime = Time.time;
			timeLeft = 400;
		}
		else{
			timeLeft = (int)(totalTime + 3*(startTime - Time.time));
		}
		if(points > maxPoints){
			maxPoints = points;
		}

		if(GuiValues.timeLeft < 0){
			respawn();
			isRespawning = true;
		}
	}

	public static void respawn(){
		if(isRespawning){
			return;
		}

		isRespawning = true;
		numLives--;
		Debug.Log (numLives + "lives left");
		if(numLives<=0){
			points=0;
			coins=0;
			numLives=3;
			firstSpawn=true;
			Application.LoadLevel(0);
			return;


		}
		Application.LoadLevel(1);
	}

	public static void goToMenu(){
		points=0;
		coins=0;
		numLives=3;
		firstSpawn=true;
		Application.LoadLevel(0);
	}

}
