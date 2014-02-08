using UnityEngine;
using System.Collections;

public class STOP : MonoBehaviour {
	Animator blah;
	float uhg;
	float mug;
	bool meow = true;
	// Use this for initialization
	void Start () {
		uhg = Time.time;
		mug = Time.time;
		blah = this.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (uhg - Time.time < -2) {
			blah.SetBool("isPop",meow);
			uhg = Time.time;
			meow = false;
		}
		if (mug - Time.time < -3) {
			blah.SetBool("isPop",meow);
			mug = Time.time;
			meow = true;
		}
	}
}
