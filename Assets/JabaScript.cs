﻿using UnityEngine;
using System.Collections;

public class JabaScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(TurnStuffOn.On == true)
		{
			renderer.enabled = true;
		}
	}

}
