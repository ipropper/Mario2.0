using UnityEngine;
using System.Collections;

public class KoopaHitObject : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	/*void Update () {
	
	}*/
	void OnTriggerEnter2D(Collider2D other)
	{
		this.transform.root.SendMessage("Hit",other);
	}
}
