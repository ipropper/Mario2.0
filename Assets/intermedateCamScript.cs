using UnityEngine;
using System.Collections;

public class intermedateCamScript : MonoBehaviour {

	IEnumerator del(){
		yield return new WaitForSeconds (2);
		Application.LoadLevel (GuiValues.scene);
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("intermedate");
		StartCoroutine ("del");
	}
	
	// Update is called once per frame
	void Update () {
	}
}
