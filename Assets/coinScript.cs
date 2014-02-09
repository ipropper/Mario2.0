using UnityEngine;
using System.Collections;

public class coinScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		GuiValues.coins  += 1;
		GuiValues.points += 200;
		Destroy (this.gameObject);
	}
}
