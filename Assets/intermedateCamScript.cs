using UnityEngine;
using System.Collections;

public class intermedateCamScript : MonoBehaviour {

	IEnumerator del(){
		yield return new WaitForSeconds (2);
		Application.LoadLevel (GuiValues.scene);
		camScript.camLock = false;
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("intermedate");
		StartCoroutine ("del");
		mario_move.Large = false;
	}
	
	// Update is called once per frame
	void Update () {
	}
}
