using UnityEngine;
using System.Collections;

public class oneUpShroomScript: MonoBehaviour {
	
	public float movement = 3.0f;
	int Jump = 0;
	float origin;
	
	public GameObject scoreText;
	
	// Use this for initialization
	void Start () {
		origin = transform.parent.rigidbody2D.velocity.y;
		transform.parent.rigidbody2D.velocity = new Vector2(movement,transform.parent.rigidbody2D.velocity.y);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		
		if(other.tag == "Player"){
			Destroy(transform.parent.gameObject);
			//Debug.Log("1 up mushroom");
			//other.SendMessage("oneUpShroom");
			GuiValues.points+=1000;
			GuiValues.numLives+=1;
			GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
			temp.GetComponent<TextMesh>().text = "1UP";
			Camera.main.SendMessage("playoneUpSound");
		}
		/*else if(other.tag != "Floor" || ){
			//if(Mathf.Abs(transform.position.x - other.transform.position.x) > .5)
			movement*=-1;
		}*/
	}
	void changeDirection()
	{
		movement *= -1;
		transform.parent.rigidbody2D.velocity = new Vector2(movement,transform.parent.rigidbody2D.velocity.y);
	}
	void JumpHeight(int height)
	{
		transform.parent.rigidbody2D.velocity = new Vector2(movement,transform.parent.rigidbody2D.velocity.y + height);
	}
}
