using UnityEngine;
using System.Collections;

public class brickBlockScript : MonoBehaviour {
	
	bool hit = false;
	public bool leftEdge = false;
	public bool rightEdge = false;
	
	Animator jumpBox;
	
	float popWait=0;
	
	// Use this for initialization
	void Start () {
		jumpBox = transform.parent.GetComponent<Animator> ();
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
					StartCoroutine("Death");
				}

				else
				{
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
	}
}
