using UnityEngine;
using System.Collections;

public class intermedateCamScript : MonoBehaviour {

	IEnumerator showLives(){
		yield return new WaitForSeconds (2);
		Application.LoadLevel (GuiValues.scene);
		camScript.camLock = false;
	}

	IEnumerator gameOver(){
		Camera.main.SendMessage ("playgameOverSound");
		yield return new WaitForSeconds (8);
		Application.LoadLevel (GuiValues.scene);
		camScript.camLock = false;
	}

	// Use this for initialization
	void Start () {
		//Debug.Log ("intermedate");

		if(GuiValues.numLives>0){
			StartCoroutine ("showLives");
		}
		else{
			this.transform.position = new Vector3(0,20,-10);
			StartCoroutine("gameOver");
		}
		mario_move.Large = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
