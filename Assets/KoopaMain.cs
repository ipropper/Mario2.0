using UnityEngine;
using System.Collections;

public class KoopaMain : MonoBehaviour {
	public float movement = 2.0f;
	// Use this for initialization

	//variables accessed by child object
	//

	Animator anim;
	public GameObject KoopaShell;
	float ChangeDirTime = 0.0f;

	public GameObject scoreText;
	
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
		if(other.tag != "Floor" && other.tag != "Player" && other.tag != "checkpoint")
		{
			//if(Mathf.Abs(transform.position.x - other.transform.position.x) > .5)
			//Debug.Log (other.name);
			movement *= -1;
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

		GuiValues.points += 100;
		GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
		temp.GetComponent<TextMesh>().text = "100";

		Destroy(this.gameObject,3.0f);
	}

	void StompDeath()
	{
		Destroy(this.gameObject);

		GuiValues.points += 100;
		GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
		temp.GetComponent<TextMesh>().text = "100";

		Instantiate(KoopaShell,this.transform.position,this.transform.rotation);
	}


	void spawned(){
		movement = -2;
	}
}
