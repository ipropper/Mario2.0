using UnityEngine;
using System.Collections;

public class menuSelector : MonoBehaviour {

	public bool selection = false;

	// Use this for initialization
	void Start () {
		GuiValues.numLives = 3;
		GuiValues.firstSpawn = true;
	}
	
	// Update is called once per frame
	void Update () {

		if(!selection) transform.position = new Vector2(-2, -1.35f);
		if( selection) transform.position = new Vector2(-2, -2.35f);

		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space)){
			if(!selection){
				GuiValues.world = "1-1";
				GuiValues.scene = 2;
				Application.LoadLevel(1);
			}
			if( selection){
				GuiValues.world = "?-¿";
				GuiValues.scene = 3;
				GuiValues.numLives = 99;
				Application.LoadLevel(1);
			}
		}

		if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
			selection = !selection;
		}
	}
}
