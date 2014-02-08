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


	bool hit = false;

	Animator jumpBox;
	Animator flashBox;

	float popWait=0;
	bool Full = true;


	// Use this for initialization
	void Start () {
		jumpBox = transform.parent.GetComponent<Animator> ();
		flashBox = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		flashBox.SetBool("isDead",!Full);
		if(popWait < Time.time){
			jumpBox.SetBool("isPop",false);
		}
	}


	void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="Player" && Full){

			if(!hit && other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) < .5f){

				popWait = Time.time+ .40f;

				//hit=true;

				//Debug.Log("successful block hit");

				jumpBox.SetBool("isPop",true);

				//TODO
				if(boxContents==spawnType.COIN){
					Instantiate(Coin, new Vector3(transform.position.x,transform.position.y+1,transform.position.z),transform.rotation);
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
				}
			}
			else if(!hit && other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) >= .5f 
			        && leftEdge && transform.position.x > other.transform.position.x)
			{
				other.transform.position = new Vector3 (transform.position.x - 1.0f,
				                                        other.transform.position.y,
				                                        other.transform.position.z);
			}
			else if(!hit && other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) >= .5f 
			        && rightEdge && transform.position.x < other.transform.position.x)
			{
				other.transform.position = new Vector3 (transform.position.x + 1.0f,
				                                        other.transform.position.y,
				                                        other.transform.position.z);
			}
		}
	}
}
