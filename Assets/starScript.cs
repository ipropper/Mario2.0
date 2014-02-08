using UnityEngine;
using System.Collections;

public class starScript : MonoBehaviour {

	// Use this for initialization
	int LayerGround;
	float startTime;
	int height = 12;
	float length = 2.5f;
	Animator anim;

	void Start () {
		LayerGround = LayerMask.NameToLayer("Ground");
		anim = gameObject.GetComponent<Animator>();
		this.rigidbody2D.velocity = new Vector2(length, height);
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > 1.0f)
		{
			anim.enabled = false;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.transform.gameObject.layer == LayerGround && other.transform.position.y < transform.position.y)
		{
			this.rigidbody2D.velocity = new Vector2(length, height);
		}
		if(other.tag == "Player")
		{
			other.SendMessage("OnStar");
			Destroy(this.gameObject);
		}
	}

}
