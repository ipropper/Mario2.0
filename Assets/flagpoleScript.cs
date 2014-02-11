using UnityEngine;
using System.Collections;

public class flagpoleScript : MonoBehaviour {

	public GameObject scoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){

		if(other.tag == "Player"){
			int poleHeight = (int)(16*(other.transform.position.y - transform.position.y));

			if      (poleHeight >= 154){
				GuiValues.numLives += 1;
				GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
				temp.GetComponent<TextMesh>().text = "1-UP";
			}else if (poleHeight >= 128){
				GuiValues.points += 5000;
				GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
				temp.GetComponent<TextMesh>().text = "5000";
			}else if (poleHeight >= 82) {
				GuiValues.points += 2000;
				GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
				temp.GetComponent<TextMesh>().text = "2000";
			}else if (poleHeight >= 58) {
				GuiValues.points += 800;
				GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
				temp.GetComponent<TextMesh>().text = "800";
			}else if (poleHeight >= 18) {
				GuiValues.points += 400;
				GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
				temp.GetComponent<TextMesh>().text = "400";
			}else {
				GuiValues.points += 200;
				GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
				temp.GetComponent<TextMesh>().text = "200";
			}
			GuiValues.points += GuiValues.timeLeft*50;

			if(GuiValues.timeLeft%10 == 1) GuiValues.points += 1*500;
			if(GuiValues.timeLeft%10 == 3) GuiValues.points += 3*500;
			if(GuiValues.timeLeft%10 == 6) GuiValues.points += 6*500;

			other.rigidbody2D.velocity = new Vector2(0, other.rigidbody2D.velocity.y);

			other.SendMessage("hitFlagPole");

			Camera.main.SendMessage("playflagpoleSound");
		}
	}
}
