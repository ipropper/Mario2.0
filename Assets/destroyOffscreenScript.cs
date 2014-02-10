using UnityEngine;
using System.Collections;

public class destroyOffscreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if(transform.position.x - Camera.main.transform.position.x > 8.0){
			Destroy(this.transform.root.gameObject);
		}
	}
}
