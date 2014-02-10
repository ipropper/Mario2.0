using UnityEngine;
using System.Collections;

public class GUI_livesScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh> ().text = GuiValues.numLives.ToString ("D2");
	}
}
