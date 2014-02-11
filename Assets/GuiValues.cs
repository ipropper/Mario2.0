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


	//sounds
	public AudioSource oneUpSound;
	public AudioSource breakBlockSound;
	public AudioSource bumpSound;
	public AudioSource coinSound;
	public AudioSource fireballSound;
	public AudioSource fireWorksSound;
	public AudioSource flagpoleSound;
	public AudioSource gameOverSound;
	public AudioSource jumpSound;
	public AudioSource bounceSound;
	public AudioSource kickSound;
	public AudioSource marioDieSound;
	public AudioSource pauseSound;
	public AudioSource pipeSound;
	public AudioSource powerUpSound;
	public AudioSource powerUpAppearsSound;
	public AudioSource stageClearSound;
	public AudioSource stompSound;
	public AudioSource warningSound;
	public AudioSource worldClearSound;
	

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
			numLives=0;
			firstSpawn=true;
			scene = 0;
			Application.LoadLevel(1);
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


	//sounds
	public void playoneUpSound(){
		oneUpSound.Play ();
	}
	public void playbreakBlockSound(){
		breakBlockSound.Play ();
	}
	public void playbumpSound(){
		bumpSound.Play ();
	}
	public void playcoinSound(){
		coinSound.Play ();
	}
	public void playfireballSound(){
		fireballSound.Play ();
	}
	public void playfireWorksSound(){
		fireWorksSound.Play ();
	}
	public void playflagpoleSound(){
		flagpoleSound.Play ();
	}
	public void playgameOverSound(){
		gameOverSound.Play ();
	}
	public void playjumpSound(){
		jumpSound.Play ();
	}
	public void playbounceSound(){
		bounceSound.Play ();
	}
	public void playkickSound(){
		kickSound.Play ();
	}
	public void playmarioDieSound(){
		marioDieSound.Play ();
	}
	public void playpauseSound(){
		pauseSound.Play ();
	}
	public void playpipeSound(){
		pipeSound.Play ();
	}
	public void playpowerUpSound(){
		powerUpSound.Play ();
	}
	public void playpowerUpAppearsSound(){
		powerUpAppearsSound.Play ();
	}
	public void playstageClearSound(){
		stageClearSound.Play ();
	}
	public void playstompSound(){
		stompSound.Play ();
	}
	public void playwarningSound(){
		warningSound.Play ();
	}
	public void playworldClearSound(){
		worldClearSound.Play ();
	}

}
