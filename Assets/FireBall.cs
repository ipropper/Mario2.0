using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour {

	// Use this for initialization
	int direction = 1;
	void Start () {
		this.rigidbody2D.velocity = new Vector2(direction*12,0);
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0,0,0);
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Floor")
		{
			this.rigidbody2D.velocity = new Vector2 (direction*12,10);
		}
		if(other.tag == "Enemy" && (other.gameObject.layer== LayerMask.NameToLayer("AllCollisions" ))
		                            || (other.gameObject.layer== LayerMask.NameToLayer("Enemies")))
		{
			other.SendMessage("FlipDeath");
		}
	}
	void SetDirection(int sign)
	{
		direction = sign;
	}
}
