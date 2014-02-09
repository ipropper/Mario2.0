using UnityEngine;
using System.Collections;

public class spawnerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.x - Camera.main.transform.position.x <= 8.5){
			BroadcastMessage("spawned");
			transform.DetachChildren();
			Destroy(this.gameObject);
		}
	}



	void awaken(){
		BroadcastMessage("spawned");
	}
}
