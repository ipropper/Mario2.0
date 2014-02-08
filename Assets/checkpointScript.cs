using UnityEngine;
using System.Collections;

public class checkpointScript : MonoBehaviour {

	bool hit=false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player" && !hit){
			hit = true;
			GuiValues.respawnPoint = transform.position;
		}
	}
}
