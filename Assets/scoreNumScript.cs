using UnityEngine;
using System.Collections;

public class scoreNumScript : MonoBehaviour {

	public float velocity = 1f;

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 1f);
		transform.position = new Vector3 (transform.position.x + .5f, transform.position.y + 1f, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = new Vector3 (this.transform.position.x, this.transform.position.y + Time.deltaTime*velocity, this.transform.position.z);
	}
}
