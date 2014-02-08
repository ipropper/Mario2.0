using UnityEngine;
using System.Collections;


public class destroyCoiin : MonoBehaviour {
	Animator anim;
	float wait;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();  
		wait = anim.GetCurrentAnimatorStateInfo(0).length + Time.time + .5f;
	}
	
	// Update is called once per frame
	void Update () {
		if((wait - Time.time) < 0)
		{
			Destroy(this.gameObject);
		}
	}
}
