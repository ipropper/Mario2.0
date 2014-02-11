using UnityEngine;
using System.Collections;

public class TurnStuffOn : MonoBehaviour {

	// Use this for initialization
	public static bool On = false;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.tag == "Player")
		{
			On = true;
		}
	}
}
