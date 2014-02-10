using UnityEngine;
using System.Collections;

public class brickBlockScript : MonoBehaviour {
	
	bool hit = false;
	public bool leftEdge = false;
	public bool rightEdge = false;
	
	Animator jumpBox;

	public GameObject edgeCheckL;
	public GameObject edgeCheckR;
	
	float popWait=0;
	
	// Use this for initialization
	void Start () {
		jumpBox = transform.parent.GetComponent<Animator> ();

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
		
		
		if(popWait < Time.time){
			jumpBox.SetBool("isPop",false);
		}
	}
	
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag=="Player")
		{
			
			if(!hit && other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) < .5f)
			{
				if(mario_move.Large == true)
				{
					Camera.main.SendMessage("playbreakBlockSound");
					StartCoroutine("Death");
				}

				else
				{
					Camera.main.SendMessage("playbumpSound");
					popWait = Time.time + .15f;
					
					//Debug.Log("successful block hit");
					
					jumpBox.SetBool("isPop",true);
				}
			}
			if(!hit && other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) >= .5f 
			        && leftEdge && transform.position.x > other.transform.position.x)
			{
				other.transform.position = new Vector3 (transform.position.x - 1.0f,
				                                        other.transform.position.y,
				                                        other.transform.position.z);
			}
			if(!hit && other.rigidbody2D.velocity.y > 8 && Mathf.Abs(transform.position.x - other.transform.position.x) >= .5f 
			        && rightEdge && transform.position.x < other.transform.position.x)
			{
				other.transform.position = new Vector3 (transform.position.x + 1.0f,
				                                        other.transform.position.y,
				                                        other.transform.position.z);
			}
		}
	}

	IEnumerator Death()
	{
		jumpBox.SetBool("isPop",true);
		yield return new WaitForSeconds(.1f);
		Destroy(this.gameObject);

		Vector2 leftPos = new Vector2(edgeCheckL.transform.position.x, edgeCheckL.transform.position.y);
		Vector2 rightPos = new Vector2(edgeCheckR.transform.position.x, edgeCheckR.transform.position.y);
		Vector2 blockposL = new Vector2(transform.position.x-.6f, transform.position.y+.5f);
		Vector2 blockposR = new Vector2(transform.position.x+.6f, transform.position.y+.5f);
		RaycastHit2D toLeft = Physics2D.Linecast (blockposL, leftPos, 1 << LayerMask.NameToLayer ("Ground"));
		RaycastHit2D toRight = Physics2D.Linecast (blockposR, rightPos, 1 << LayerMask.NameToLayer ("Ground"));
		if(toLeft){
			//Debug.Log (toLeft.collider.tag);
			toLeft.collider.SendMessage("onBlockDestroy");
		}
		if(Physics2D.Linecast(blockposR , rightPos , 1 << LayerMask.NameToLayer("Ground"))){
			toRight.collider.SendMessage("onBlockDestroy");
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
