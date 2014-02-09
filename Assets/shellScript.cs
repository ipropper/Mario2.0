using UnityEngine;
using System.Collections;

public class shellScript : MonoBehaviour {

	// Use this for initialization
	float speed = 0;
	float NoStrikeTime = 0f;
	float shiftTime = 0;

	float Reborn = 0f;
	float immune = 0.0f;
	bool comeback = true;

	Animator anim;

	public GameObject phoenixTurtle;

	void Start () {
		anim = this.GetComponentInChildren<Animator>();
		Reborn = Time.time + 5.2f;
		speed = 0;
		immune = Time.time  + .1f;
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - immune < 0)
		{
			speed = 0;
		}
		this.transform.position = new Vector3 (transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
		if(Time.time - Reborn > 0 && comeback)
		{
			Instantiate(phoenixTurtle,this.transform.position,this.transform.rotation);
			Destroy(this.gameObject);
		}
		anim.SetBool("ComeBack",comeback);

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			// makes the shell move when player first hits it
			if(speed == 0)
			{
				if(other.transform.position.x - transform.position.x < 0)
				{
					speed = 10;
				}
				else if(other.transform.position.x - transform.position.x >= 0)
				{
					speed = -10;
				}
				NoStrikeTime = Time.time + .2f;
				comeback = false;
			}
			//attacks player
			else if(Time.time - NoStrikeTime > 0)
			{
				other.SendMessage("hitByEnemy");
			}
		}
		else if(other.tag == "Enemy" && speed != 0)
		{
			other.SendMessage("FlipDeath");
			shiftTime = Time.time + .1f;
		}
		else if(other.tag != "Floor" && Time.time - shiftTime > 0)
		{
			//Debug.Log(other.tag);
			speed *= -1;
		}
	}
}
