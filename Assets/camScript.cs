using UnityEngine;
using System.Collections;

public class camScript : MonoBehaviour {

	public GameObject Mario;

	public float maxXPos = 0;

	public GameObject respawn1;

	// Use this for initialization
	void Start () {
		maxXPos = transform.position.x;
		if(Mario.transform.position.x - maxXPos > 0){

			maxXPos = Mario.transform.position.x;
			transform.position = new Vector3(maxXPos,transform.position.y,transform.position.z);

		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Mario.transform.position.x - maxXPos > 0){
			
			maxXPos = Mario.transform.position.x;
			transform.position = new Vector3(maxXPos,transform.position.y,transform.position.z);
			
		}
	}

	void respawn(){
		maxXPos = Mario.transform.position.x;
		transform.position = new Vector3(maxXPos,transform.position.y,transform.position.z);
	}

	void goThroughPipe(Vector2 newLoc){
		//TODO
	}
}
