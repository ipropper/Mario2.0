using UnityEngine;
using System.Collections;

public struct cameraMove{
	public Vector3 loc;
	public bool fixedPos;
	public cameraMove(Vector3 L, bool F){
		loc = L;
		fixedPos = F;
	}
}

public class camScript : MonoBehaviour {

	public GameObject Mario;

	public float maxXPos = 0;

	public GameObject respawn1;

	public static bool camLock=false;

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

		if(Mario.transform.position.x - maxXPos > 0 && !camLock){
			
			maxXPos = Mario.transform.position.x;
			transform.position = new Vector3(maxXPos,transform.position.y,transform.position.z);
			
		}
	}

	void respawn(){
		maxXPos = Mario.transform.position.x;
		transform.position = new Vector3(maxXPos,transform.position.y,transform.position.z);
	}

	void goThroughPipe(cameraMove C){
		maxXPos = 0;
		transform.position = C.loc;
		camLock = C.fixedPos;
		if(!camLock){
			maxXPos = -1000;
		}
	}
}
