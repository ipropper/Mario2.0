using UnityEngine;
using System.Collections;

public class coinScript : MonoBehaviour {

	public GameObject scoreText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		GuiValues.coins  += 1;
		GuiValues.points += 200;

		GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
		temp.GetComponent<TextMesh>().text = "200";

		Camera.main.SendMessage("playcoinSound");
		Destroy (this.gameObject);
	}
}
