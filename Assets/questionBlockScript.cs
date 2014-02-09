using UnityEngine;
using System.Collections;

public enum spawnType{
	NONE,
	COIN,
	SUPERSHROOM,
	STAR
}


public class questionBlockScript : MonoBehaviour {

	public bool leftEdge = false;
	public bool rightEdge = false;
	public spawnType boxContents = spawnType.NONE;
	public int spawnNum;
	public GameObject Shroom;
	public GameObject Coin;
	public GameObject FireFlower;
	public GameObject PowerStar;

	public GameObject edgeCheckL;
	public GameObject edgeCheckR;


	bool hit = false;

	Animator jumpBox;
	Animator flashBox;

	float popWait=0;
	bool Full = true;


	// Use this for initialization
	void Start () {
		jumpBox = transform.parent.GetComponent<Animator> ();
		flashBox = GetComponent<Animator> ();

		Vector2 leftPos = new Vector2(edgeCheckL.transform.position.x, edgeCheckL.transform.position.y);
		Vector2 rightPos = new Vector2(edgeCheckR.transform.position.x, edgeCheckR.transform.position.y);
		Vector2 blockposL = new Vector2(transform.position.x-.6f, transform.position.y+.5f);
		Vector2 blockposR = new Vector2(transform.position.x+.6f, transform.position.y+.5f);
		RaycastHit2D toLeft = Physics2D.Linecast (blockposL, leftPos, 1 << LayerMask.NameToLayer ("Ground"));
		RaycastHit2D toRight = Physics2D.Linecast (blockposR, rightPos, 1 << LayerMask.NameToLayer ("Ground"));
		if(toLeft){
			//Debug.Log (toLeft.collider.tag);
			leftEdge=false;
		}
		else leftEdge=true;
		if(Physics2D.Linecast(blockposR , rightPos , 1 << LayerMask.NameToLayer("Ground"))){
			rightEdge=false;
		}
		else rightEdge=true;
	}
	
	// Update is called once per frame
	void Update () {
		
		flashBox.SetBool("isDead",!Full);
		if(popWait < Time.time){
			jumpBox.SetBool("isPop",false);
		}
	}

	IEnumerator disableAnim(){
		yield return new WaitForSeconds (1f);
		jumpBox.enabled = false;
		flashBox.enabled = false;
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="Player" && Full){

			//Debug.Log("box");

			if(!hit && other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) < .5f){

				popWait = Time.time+ .40f;

				//hit=true;

				//Debug.Log("successful block hit");

				jumpBox.SetBool("isPop",true);

				//TODO
				if(boxContents==spawnType.COIN){
					Instantiate(Coin, new Vector3(transform.position.x,transform.position.y+1,transform.position.z),transform.rotation);
					GuiValues.coins += 1;
					GuiValues.points+= 200;
				}
				else if(boxContents==spawnType.STAR)
				{
					Instantiate(PowerStar, new Vector3(transform.position.x,transform.position.y,transform.position.z),transform.rotation);
				}
				else if(boxContents==spawnType.SUPERSHROOM && mario_move.Large == false){
					Instantiate(Shroom, new Vector3(transform.position.x,transform.position.y,transform.position.z),transform.rotation);
				}
				else
				{
					Instantiate(FireFlower, new Vector3(transform.position.x,transform.position.y,transform.position.z),transform.rotation);
				}
				spawnNum--;
				if(spawnNum == 0)
				{
					Full = false;
					StartCoroutine("disableAnim");
				}
			}
			else if(other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) >= .5f 
			        && leftEdge && transform.position.x > other.transform.position.x)
			{
				other.transform.position = new Vector3 (transform.position.x - 1.0f,
				                                        other.transform.position.y,
				                                        other.transform.position.z);
			}
			else if(other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) >= .5f 
			        && rightEdge && transform.position.x < other.transform.position.x)
			{
				other.transform.position = new Vector3 (transform.position.x + 1.0f,
				                                        other.transform.position.y,
				                                        other.transform.position.z);
			}
		}
	}

	void onBlockDestroy(){
		Vector2 leftPos = new Vector2(edgeCheckL.transform.position.x, edgeCheckL.transform.position.y);
		Vector2 rightPos = new Vector2(edgeCheckR.transform.position.x, edgeCheckR.transform.position.y);
		Vector2 blockposL = new Vector2(transform.position.x-.6f, transform.position.y+.5f);
		Vector2 blockposR = new Vector2(transform.position.x+.6f, transform.position.y+.5f);
		RaycastHit2D toLeft = Physics2D.Linecast (blockposL, leftPos, 1 << LayerMask.NameToLayer ("Ground"));
		RaycastHit2D toRight = Physics2D.Linecast (blockposR, rightPos, 1 << LayerMask.NameToLayer ("Ground"));
		if(toLeft){
			//Debug.Log (toLeft.collider.tag);
			leftEdge=false;
		}
		else leftEdge=true;
		if(Physics2D.Linecast(blockposR , rightPos , 1 << LayerMask.NameToLayer("Ground"))){
			rightEdge=false;
		}
		else rightEdge=true;
	}
}
