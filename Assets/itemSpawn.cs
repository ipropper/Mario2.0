using UnityEngine;
using System.Collections;

public class itemSpawn : MonoBehaviour {

	// Use this for initialization
	float startTime;
	LayerMask origin;
	LayerMask childorigin;
	float originGrav;
	

	void Start () {
		startTime = Time.time;
		origin = this.gameObject.layer;



		this.gameObject.layer = LayerMask.NameToLayer("Death");
		foreach (Transform child in this.transform)
		{
			child.gameObject.layer = LayerMask.NameToLayer("Death");
		}
		if(this.rigidbody2D != null)
		{
			originGrav = rigidbody2D.gravityScale;
			rigidbody2D.gravityScale = 0.0f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > 1.0f)
		{
			this.gameObject.layer = origin;
			foreach (Transform child in this.transform)
			{
				child.gameObject.layer = LayerMask.NameToLayer("Default");
			}
			if(this.rigidbody2D != null)
			{
				rigidbody2D.gravityScale = originGrav;
			}
			this.GetComponent<Animator>().enabled = false;
			Destroy(this);
		}
	}
}
