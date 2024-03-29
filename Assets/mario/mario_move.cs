﻿using UnityEngine;
using System.Collections;

enum size{
	NORMAL,
	SMALL,
	LARGE
}

public class mario_move : MonoBehaviour {
	
	// Use this for initialization
	// new keys

	public GameObject cam;

	public KeyCode Jump;
	public KeyCode Jump2;
	
	
	public float JumpHeight = 3.0f;
	public float JumpMore = 0.5f;
	public float RightLeft = 2.0f;
	public float MaxRL = 6f;
	public float sprintMaxRL = 9f;
	public float MaxVelocitY = 3.0f;

	public float minJumpHeight = 2f;
	public float maxJumpHeight = 3.65f;
	public float jumpVel = 12f;

	public float bounceVal = 12f;

	Vector3 respawnPos;

	float jumpFromHeight=0;

	bool jumping = false;
	
	//used for line cast
	public GameObject GroundCheckC;
	public GameObject GroundCheckL;
	public GameObject GroundCheckR;
	
	public bool grounded = true;
	Vector2 groundPosC;
	Vector2 groundPosL;
	Vector2 groundPosR;
	Vector2 playePos;
	
	//used to change left/right movement
	int cap = 300;
	float origin;
	
	//used for holding jump
	bool more_jump = true;
	float originV;

	float temptime;

	Animator runAnim;
	KeyCode prevDirection;
	bool canSlide = false;
	float animTime;
	public static bool Large = false;

	public KeyCode FireKey;
	public KeyCode FireKey2;
	public GameObject FireBall;
	bool canFire = false;
	int canShoot = 2;
	int MarioDir = 1;

	public static bool StarPower = false;
	float StarClock = -20.0f;
	float PrevStarTime = 0.0f;

	float invincibleTime = 2.0f;

	public static bool enlarged = false;

	public bool lockedOut = false;

	public bool jumpSoundPlayed = false;

	public GameObject myDeath;
	bool Dead = false;

	void Start()
	{
		runAnim = this.GetComponent<Animator>();
		if(GuiValues.firstSpawn){
			GuiValues.respawnPoint = transform.position;
			GuiValues.firstSpawn=false;
		}
		else{
			transform.position = GuiValues.respawnPoint;
		}

		origin = RightLeft;

		temptime = Time.time + .5f;
		animTime = Time.time;


		//Time.timeScale = 2;
	}

	float gravAdjust(float altitude, float velocity){

		if(altitude>maxJumpHeight){
			if(velocity > 0){
				return 6f + .4f*altitude;
			}
			else{
				return 8f - .4f*altitude;
			}
		}
		return 8;
	}

	float velocityByAltitude(float altitude, float velocity){
		if(altitude >= maxJumpHeight+1){
			return -.1f;
		}

		if(velocity > 0){
			return jumpVel - 10.4f*.05f*altitude;
		}
		if(velocity <=0){
			return -jumpVel - 12.4f*.15f*altitude;
		}
		return 0;


	}

	void Update(){

		if(enlarged || lockedOut)
		{
			// do nothing
		}
		else
		{
			if((Input.GetKeyDown(Jump) || Input.GetKeyDown(Jump2)) && grounded)
			{
				
				more_jump = true;
				grounded = false;
				jumpFromHeight = transform.position.y;
				
				jumping = true;
				
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVel);
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		/*
		if(GuiValues.timeLeft < 0){
			GuiValues.respawn();
		}*/
		
		/*if(temptime < Time.time){
			Time.timeScale = Random.Range (.1f, 5);
			temptime = Time.time+.5f;
		}*/

		/*if(Time.time > invincibleTime){
			Physics2D.IgnoreLayerCollision(10,16,false); 
		}*/

		//animates run + slide
		if(enlarged || lockedOut)
		{
			// do nothing
		}
		else
		{
			runAnim.SetFloat("Vel", (Mathf.Abs(rigidbody2D.velocity.x)));

			//Mario slide code

			if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))){

			}

			else if((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && rigidbody2D.velocity.x > 0.0f)
			{
				rigidbody2D.velocity = new Vector2(Mathf.Max(0,rigidbody2D.velocity.x - .2f), rigidbody2D.velocity.y);
				runAnim.SetBool("Slide",true);
			}
			else if((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && rigidbody2D.velocity.x < 0.0f)
			{
				rigidbody2D.velocity = new Vector2(Mathf.Min(0,rigidbody2D.velocity.x + .2f), rigidbody2D.velocity.y);
				runAnim.SetBool("Slide",true);
			}
			else{
				runAnim.SetBool("Slide",false);
			}

			//END OF SLIDE CODE

			// animate jump
			runAnim.SetBool("isGrounded",grounded);


			if (Input.GetAxis("Horizontal") > 0) {
				transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x),transform.localScale.y);
				MarioDir = 1;
			}

			if (Input.GetAxis("Horizontal") < 0) {
				transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x),transform.localScale.y);
				MarioDir = -1;
			}



			// creates 3 line casts between player and empty object, if "Ground" layer comes in contact with line allows jump 
			groundPosC = new Vector2(GroundCheckC.transform.position.x, GroundCheckC.transform.position.y);
			groundPosL = new Vector2(GroundCheckL.transform.position.x, GroundCheckL.transform.position.y);
			groundPosR = new Vector2(GroundCheckR.transform.position.x, GroundCheckR.transform.position.y);
			playePos = new Vector2(transform.position.x, transform.position.y);
			grounded = Physics2D.Linecast(playePos , groundPosC , 1 << LayerMask.NameToLayer("Ground"))
						|| Physics2D.Linecast(playePos , groundPosL , 1 << LayerMask.NameToLayer("Ground"))
						|| Physics2D.Linecast(playePos , groundPosR , 1 << LayerMask.NameToLayer("Ground"));
			
				
				
				//y is flawed but funtionally works way better...
			if(rigidbody2D.velocity.y == 0)
			{
				if(jumping && transform.position.y - jumpFromHeight > 1 && !jumpSoundPlayed){
					Camera.main.SendMessage("playjumpSound");
				}

				grounded = true;
				jumping = false;
				jumpSoundPlayed = false;
				rigidbody2D.gravityScale = 8;
			}





			if(!(Input.GetKey(Jump) || Input.GetKey(Jump2)) && more_jump && !jumpSoundPlayed && !grounded){
				Camera.main.SendMessage("playjumpSound");
				jumpSoundPlayed = true;
			}

			if(!(Input.GetKey(Jump) || (Input.GetKey(Jump2)))){
				more_jump = false;
			}

			if (jumping && !grounded && rigidbody2D.velocity.y > 0 && 
							(((Input.GetKey(Jump) || Input.GetKey(Jump2)) && more_jump && (transform.position.y - jumpFromHeight) < maxJumpHeight) 
							|| (transform.position.y - jumpFromHeight) < minJumpHeight)) {

				//rigidbody2D.gravityScale = gravAdjust(transform.position.y - jumpFromHeight, rigidbody2D.velocity.y);

				//rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVel);
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, velocityByAltitude(transform.position.y - jumpFromHeight, rigidbody2D.velocity.y));
			}
			else if(jumping && !grounded && !jumpSoundPlayed){
				Camera.main.SendMessage("playjumpSound");
				jumpSoundPlayed = true;
			}
			if(rigidbody2D.velocity.y < -jumpVel){

				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -jumpVel);
			}
			
			
			// movement left and right


			if(Input.GetButton("Horizontal"))
			{
				float axis = Input.GetAxis("Horizontal");
				//Debug.Log(axis);
				// if both left and right are pressed mario does an awkward moon walk thing.
				if(axis == 0){
					float vel = rigidbody2D.velocity.x;
					if (vel > 0){
						rigidbody2D.velocity = new Vector2(Mathf.Max(0,vel - .2f), rigidbody2D.velocity.y);
					}
					if (vel < 0){
						rigidbody2D.velocity = new Vector2(Mathf.Min(0,vel + .2f), rigidbody2D.velocity.y);
					}
				}
				else
				{
					if(axis > 0){
						if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
							rigidbody2D.velocity = new Vector2(Mathf.Min(sprintMaxRL,rigidbody2D.velocity.x+.4f), rigidbody2D.velocity.y);
						}
						else{
							rigidbody2D.velocity = new Vector2(Mathf.Min(MaxRL,rigidbody2D.velocity.x+.2f), rigidbody2D.velocity.y);
						}
					}
					if(axis < 0){
						if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
							rigidbody2D.velocity = new Vector2(Mathf.Max(-sprintMaxRL,rigidbody2D.velocity.x-.4f), rigidbody2D.velocity.y);
						}
						else{
							rigidbody2D.velocity = new Vector2(Mathf.Max(-MaxRL,rigidbody2D.velocity.x-.2f), rigidbody2D.velocity.y);
						}
					}

					/*if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
						rigidbody2D.velocity = new Vector2((2*RightLeft * axis), rigidbody2D.velocity.y); 
						if(RightLeft <= sprintMaxRL)
						{
							RightLeft += .2f;
						}
					}else
					{
						rigidbody2D.velocity = new Vector2((RightLeft * axis), rigidbody2D.velocity.y); 
						if(RightLeft <= MaxRL)
						{
							RightLeft += .2f;
						}
					}*/
				}
			}
			else if (!jumping){
				float vel = rigidbody2D.velocity.x;

				if(Mathf.Abs(rigidbody2D.velocity.x) < .3f) rigidbody2D.velocity = new Vector2(0,rigidbody2D.velocity.y);
				else if (vel > 0){
					rigidbody2D.velocity = new Vector2(Mathf.Max(0,vel - .05f), rigidbody2D.velocity.y);
				}
				else if (vel < 0){
					rigidbody2D.velocity = new Vector2(Mathf.Min(0,vel + .05f), rigidbody2D.velocity.y);
				}
			}
				
				/*//refresh rightleft when no longer walking
			if(Input.GetButtonUp("Horizontal"))
			{
				RightLeft -= .25f;
			}*/
				if((Input.GetKeyDown(FireKey) || Input.GetKeyDown(FireKey2))  && canFire && canShoot >0)
			{
				GameObject Shot = Instantiate(FireBall, new Vector3(transform.position.x+MarioDir ,transform.position.y + 1,transform.position.z),transform.rotation) as GameObject;
				Shot.SendMessage("SetDirection", MarioDir);
				canShoot--;
			}

			if(StarClock - Time.time <= 0.0f)
			{
				StarPower = false;
			}
			else if(StarClock - Time.time <= .10f)
			{
				if(canFire)
				{
					renderer.material.color = Color.red;
				}
				else
				{
					renderer.material.color = Color.white;
				}
			}
			else if(StarClock - Time.time <= 5.0f && StarClock - Time.time > .4f)
			{
				if(Time.time - PrevStarTime > .3f)
				{
					Color randomC = new Color (Random.Range(.0f,1.0f),Random.Range(.0f,1.0f),Random.Range(.0f,1.0f));
					renderer.material.color = randomC;
					PrevStarTime = Time.time;
				}
			}
			else
			{
				Color randomC = new Color (Random.Range(.0f,1.0f),Random.Range(.0f,1.0f),Random.Range(.0f,1.0f));
				renderer.material.color = randomC;
				PrevStarTime = Time.time;
			}
		}
	}

	IEnumerator invincible(){
		 

		Color temp = this.renderer.material.color;

		for(int i=0; i < (2.6f)/(2f*.2f); i++){
			temp.a=.0f;
			this.renderer.material.color = temp;
			yield return new WaitForSeconds(.2f);
			temp.a=.25f;
			this.renderer.material.color = temp;
			yield return new WaitForSeconds(.2f);
		}
		temp.a = 1;
		renderer.material.color = temp;

		Physics2D.IgnoreLayerCollision(10,16,false); 
		Physics2D.IgnoreLayerCollision(10,0,false);
	}

	void hitByEnemy(){

		if(Large == true){
			Physics2D.IgnoreLayerCollision(10,16,true);
			Physics2D.IgnoreLayerCollision(10,0,true);
			//transform.localScale = new Vector2 (.70f, .45f);
			Large = false;
			enlarged = true;
			StartCoroutine("ensmall");
			canFire=false;
			renderer.material.color = Color.white;

			StartCoroutine("invincible");
		}
		else{
			//transform.position = respawnPos;
			StartCoroutine("Death",true);
		}
	}

	void fallRespawn(){
		//transform.position = respawnPos;
		StartCoroutine("Death",false);

	}

	void bounceOnEnemy(){

		jumping = false;
		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, bounceVal);
		grounded = false;

		Camera.main.SendMessage("playstompSound");
	}

	void gotBigMushroom(){

		Large = true;
		enlarged = true;
		StartCoroutine("enlarge");
	}
	void fireFlower()
	{
		if(Large){
			canFire = true;
			this.renderer.material.color = Color.red;
		}
		else{
			gotBigMushroom();
		}
	}
	void resetShot()
	{
		canShoot++;
	}

	void OnStar()
	{
		StarPower = true;
		StarClock = Time.time + 12.0f;
	}

	IEnumerator enlarge()
	{
		Camera.main.SendMessage ("playpowerUpSound");
		rigidbody2D.velocity = new Vector2(0,0);
		rigidbody2D.gravityScale = 0;
		
		//change heights
		transform.localScale = new Vector2 (.75f, .45f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .6f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .45f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .8f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .6f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .8f);
		yield return new WaitForSeconds(.10f);
		
		rigidbody2D.gravityScale = 8;
		enlarged = false;
	}
	IEnumerator ensmall()
	{
		Camera.main.SendMessage ("playpipeSound");
		rigidbody2D.velocity = new Vector2(0,0);
		rigidbody2D.gravityScale = 0;
		
		//change heights
		transform.localScale = new Vector2 (.75f, .8f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .6f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .8f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .45f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .6f);
		yield return new WaitForSeconds(.10f);
		transform.localScale = new Vector2 (.75f, .45f);
		yield return new WaitForSeconds(.10f);
		
		rigidbody2D.gravityScale = 8;
		
		enlarged = false;
	}
	IEnumerator Death(bool anim)
	{
		if(!Dead){
			rigidbody2D.velocity = new Vector2(0,0);
			Dead = true;
			enlarged = true;
			renderer.enabled = false;
			if(anim)
			{
				Instantiate(myDeath,transform.position,transform.rotation);
			}
			cam.SendMessage("playmarioDieSound");
			cam.GetComponent<AudioSource>().mute = true;

			yield return new WaitForSeconds(3.0f);

			cam.GetComponent<AudioSource>().mute = false;
			enlarged = false;
			GuiValues.respawn();
		}
	}
	

	public void teleport(Vector3 endPos){
		this.transform.position = endPos;
	}

	IEnumerator moveToEnding(){
		camScript.camLock = true;
		yield return new WaitForSeconds (3);

		GuiValues.goToMenu();
	}


	public void hitFlagPole(){
		lockedOut = true;
		StartCoroutine ("moveToEnding");
	}
}
