﻿using UnityEngine;
using System.Collections;

public class GUI_timeScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<TextMesh> ().text = GuiValues.timeLeft.ToString ("D3");
	}
}
