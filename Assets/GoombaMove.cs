using UnityEngine;
using System.Collections;

public class GoombaMove : MonoBehaviour {
	public float movement = -2;
	bool stomp = false;
	// Use this for initialization
	Animator anim;

	void Start () {
		anim = this.GetComponentInChildren<Animator>();
	}
	// Update is called once per frame
	void Update () {
		if(stomp || mario_move.enlarged)
		{
			//do not move
		}
		else
		{
			this.transform.position = new Vector3 (transform.position.x + movement * Time.deltaTime, transform.position.y, transform.position.z);
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
	}

	void StompDeath()
	{
		stomp = true;
		this.gameObject.layer = LayerMask.NameToLayer("ButPlayer");
		foreach (Transform child in this.transform)
		{
			child.gameObject.layer = LayerMask.NameToLayer("ButPlayer");
		}
		anim.SetBool("Stomp",true);
		this.rigidbody2D.velocity = new Vector2 (0,0);
		Destroy(this.gameObject,.30f);

		GuiValues.points += 100;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag != "Floor" && other.tag != "Player")
		{
			//if(Mathf.Abs(transform.position.x - other.transform.position.x) > .5)
			movement *= -1;
		}
		else if(other.tag == "Player")
		{
			if(other.rigidbody2D.velocity.y < 0 && other.gameObject.transform.position.y > this.transform.position.y + .5f)
			{
				other.gameObject.SendMessage("bounceOnEnemy");
				stomp = true;
				StompDeath();
				//Debug.Log("goomba killed");
			}
			else if(mario_move.StarPower)
			{
				FlipDeath();
			}
			else if (!stomp)
			{
				other.gameObject.SendMessage("hitByEnemy");
			}
		}
	}
}
