using UnityEngine;
using System.Collections;

public class KoopaMain : MonoBehaviour {
	public float movement = -2;
	// Use this for initialization

	//variables accessed by child object
	//

	Animator anim;
	public GameObject KoopaShell;
	float ChangeDirTime = 0.0f;
	
	void Start () {
		anim = this.GetComponentInChildren<Animator>();
	}

	// Update is called once per frame
	void Update () 
	{
		if(mario_move.enlarged)
		{
			//do nothing
		}
		else if(!anim.GetBool("Flip"))
		{
			this.transform.position = new Vector3 (transform.position.x + movement * Time.deltaTime, transform.position.y, transform.position.z);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag != "Floor" && other.tag != "Player" && Time.time - ChangeDirTime > 0)
		{
			//if(Mathf.Abs(transform.position.x - other.transform.position.x) > .5)
			movement *= -1;
			ChangeDirTime = Time.time + .05f;
			transform.localScale = new Vector2(this.transform.localScale.x * -1.0f,this.transform.localScale.y);
		}
		else if(other.tag == "Player")
		{
			if(other.rigidbody2D.velocity.y < 0 && other.transform.position.y > this.transform.position.y)
			{
				other.SendMessage("bounceOnEnemy");
				StompDeath();
			}
			else if(mario_move.StarPower)
			{
				FlipDeath();
			}
			else
			{
				other.SendMessage("hitByEnemy");
			}
		}
	}

	void FlipDeath()
	{
		this.gameObject.layer = LayerMask.NameToLayer("Death");
		foreach (Transform child in this.transform)
		{
			child.gameObject.layer = LayerMask.NameToLayer("Death");
		}
		anim.SetBool("Flip",true);
		Destroy(this.gameObject,3.0f);
	}

	void StompDeath()
	{
		Destroy(this.gameObject);
		Instantiate(KoopaShell,this.transform.position,this.transform.rotation);
	}

	void Hit(Collider2D other){

	}
}
