using UnityEngine;
using System.Collections;

public class checkTopShroom : MonoBehaviour {
	bool can_change = false;
	Animator jumpBox;

	void Start () 
	{
		jumpBox = GetComponentInChildren<Animator>();
	}
	void Update ()
	{
		if(!jumpBox.GetBool("isPop"))
		{
			can_change = true;
		}
	}
	// Update is called once per frame
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "powerUp" && can_change && jumpBox.GetBool("isPop"))
		{
			//Debug.Log(other.rigidbody2D.velocity.x + "    " + (other.transform.position.x - this.transform.position.x));
			// checks to see the position of the mushroom to flip
			if(other.rigidbody2D.velocity.x > 0 && other.transform.position.x - this.transform.position.x < 0 ||
			   other.rigidbody2D.velocity.x < 0 && other.transform.position.x - this.transform.position.x > 0)
			{
				Debug.Log ("reversing mushroom");
				other.transform.FindChild("MushroomTriggerZone").SendMessage("changeDirection");
			}
			other.transform.FindChild("MushroomTriggerZone").SendMessage("JumpHeight",15);
			can_change = false;
		}
		if(other.tag == "Enemy" && can_change && jumpBox.GetBool("isPop"))
		{
			other.SendMessage("FlipDeath");
		}
	}
}
