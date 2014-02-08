using UnityEngine;
using System.Collections;

public class MushroomMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other){
		if(other.tag != "Player")
		{
			this.transform.parent.FindChild("MushroomTriggerZone").SendMessage("changeDirection");
		}
		
	}
}
