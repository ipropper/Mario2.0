using UnityEngine;
using System.Collections;

public enum enterDir{
	TOP,
	BOTTOM,
	LEFT,
	RIGHT
};

public class pipeEntranceScript : MonoBehaviour {

	public enterDir direction;

	public Vector3 pipeExitLoc;
	public Vector3 pipeCameraExitLoc;
	public bool camLockedOnExit;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void goThrough(Collider2D other){
		other.SendMessage("teleport", pipeExitLoc);
		Camera.main.SendMessage("goThroughPipe", new cameraMove(pipeCameraExitLoc, camLockedOnExit));
	}

	void OnTriggerStay2D(Collider2D other){
		Debug.Log("in the trigger zone");
		if(other.tag == "Player"){
			if(direction==enterDir.BOTTOM){
				if(Input.GetKey(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)){
					goThrough(other);
				}
			}
			else{
				goThrough(other);
			}
		}
	}
}
