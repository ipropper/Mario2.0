using UnityEngine;
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
	
	
	public float JumpHeight = 3.0f;
	public float JumpMore = 0.5f;
	public float RightLeft = 2.0f;
	public float MaxRL = 2.0f;
	public float sprintMaxRL = 4.0f;
	public float MaxVelocitY = 3.0f;

	public float minJumpHeight = 2f;
	public float maxJumpHeight = 4f;
	public float jumpVel = 2.0f;

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
	public GameObject FireBall;
	bool canFire = false;
	int canShoot = 2;
	int MarioDir = 1;

	public static bool StarPower = false;
	float StarClock = -20.0f;
	float PrevStarTime = 0.0f;

	float invincibleTime = 2.0f;

	public static bool enlarged = false;

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
	
	// Update is called once per frame
	void Update () {

		if(GuiValues.timeLeft < 0){
			GuiValues.respawn();
		}
		
		/*if(temptime < Time.time){
			Time.timeScale = Random.Range (.1f, 5);
			temptime = Time.time+.5f;
		}*/

		/*if(Time.time > invincibleTime){
			Physics2D.IgnoreLayerCollision(10,11,false); 
		}*/

		//animates run + slide
		if(enlarged)
		{
			// do nothing
		}
		else
		{
			runAnim.SetInteger("Speed",(int) Mathf.Ceil(Mathf.Abs(rigidbody2D.velocity.x)));
			if(rigidbody2D.velocity.x < 0)
			{
				if(prevDirection == KeyCode.D)
				{
					runAnim.SetBool("ChangeDirection",false);
					canSlide = false;
					animTime = Time.time;
				}
			}
			else if((rigidbody2D.velocity.x > 0))
			{
				if(prevDirection == KeyCode.A)
				{
					runAnim.SetBool("ChangeDirection",false);
					canSlide = false;
					animTime = Time.time;
				}
			}

			if(Time.time - animTime > .50f)
			{
				canSlide = true;
			}

			// THIS IS TO ANIMATE MARIO SLIDE
			if(Input.GetKeyUp(KeyCode.A) && canSlide)
			{
				if(prevDirection == KeyCode.D)
				{
					runAnim.SetBool("ChangeDirection",true);
				}
				prevDirection = KeyCode.A;
			}
			else if(Input.GetKeyUp(KeyCode.D) && canSlide)
			{
				if(prevDirection == KeyCode.A)
				{
					runAnim.SetBool("ChangeDirection",true);
				}
				prevDirection = KeyCode.D;
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
				grounded = true;
				jumping = false;
			}


			if(Input.GetKeyDown(Jump) && grounded)
			{

				more_jump = true;
				grounded = false;
				jumpFromHeight = transform.position.y;

				jumping = true;

				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVel);
			}

			if(!Input.GetKey(Jump)){
				more_jump = false;
			}
			if (jumping && !grounded && rigidbody2D.velocity.y > 0 && 
							((Input.GetKey (Jump) && more_jump && (transform.position.y - jumpFromHeight) < maxJumpHeight) 
							|| (transform.position.y - jumpFromHeight) < minJumpHeight)) {

				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVel);
			}
			if(rigidbody2D.velocity.y < -jumpVel){

				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -jumpVel);
			}
			
			
			// movement left and right
			if(Input.GetButton("Horizontal"))
			{
				float axis = Input.GetAxis("Horizontal");
				// if both left and right are pressed mario does an awkward moon walk thing.
				if(axis == 0){
					rigidbody2D.velocity = new Vector2(.1f,rigidbody2D.velocity.y);
				}
				else
				{
					if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)){
						rigidbody2D.velocity = new Vector2((2*RightLeft * axis), rigidbody2D.velocity.y); 
						if(RightLeft <= sprintMaxRL)
						{
							RightLeft += .5f;
						}
					}else
					{
						rigidbody2D.velocity = new Vector2((RightLeft * axis), rigidbody2D.velocity.y); 
						if(RightLeft <= MaxRL)
						{
							RightLeft += .5f;
						}
					}
				}
			}
			
			//refresh rightleft when no longer walking
			if(Input.GetButtonUp("Horizontal"))
			{
				RightLeft = origin;
			}
			if(Input.GetKeyDown(FireKey) && canFire && canShoot >0)
			{
				GameObject Shot = Instantiate(FireBall, new Vector3(transform.position.x+1 ,transform.position.y + 1,transform.position.z),transform.rotation) as GameObject;
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

		for(int i=0; i < 2f/(2f*.2f); i++){
			temp.a=.0f;
			this.renderer.material.color = temp;
			yield return new WaitForSeconds(.2f);
			temp.a=.25f;
			this.renderer.material.color = temp;
			yield return new WaitForSeconds(.2f);
		}
		temp.a = 1;
		renderer.material.color = temp;

		Physics2D.IgnoreLayerCollision(10,11,false); 
	}

	void hitByEnemy(){

		if(Large == true){
			Physics2D.IgnoreLayerCollision(10,11,true);
			transform.localScale = new Vector2 (.70f, .45f);
			Large=false;
			canFire=false;
			renderer.material.color = Color.white;

			StartCoroutine("invincible");
		}
		else{
			//transform.position = respawnPos;
			GuiValues.respawn();
		}
	}

	void fallRespawn(){
		//transform.position = respawnPos;
		GuiValues.respawn ();

	}

	void bounceOnEnemy(){

		rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpVel);
		grounded = false;
	}

	void gotBigMushroom(){

		Large = true;
		enlarged = true;
		StartCoroutine("enlarge");
	}
	void fireFlower()
	{
		canFire = true;
		this.renderer.material.color = Color.red;
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

	public void teleport(Vector3 endPos){
		this.transform.position = endPos;
	}

}
