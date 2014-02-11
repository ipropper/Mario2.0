using UnityEngine;
using System.Collections;

public class DestroySideFire : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.eulerAngles = new Vector3(0,0,0);

		if(Mathf.Abs(transform.position.x - Camera.main.transform.position.x) > 7.5f){
			GameObject.Find("Mario").SendMessage("resetShot");
			Destroy(this.transform.parent.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Enemy")
		{
			// do nothing
		}
		else if(other.tag == "Player")
		{
			//do nothing
		}
		else
		{
			GameObject.Find("Mario").SendMessage("resetShot");
			Destroy(this.transform.parent.gameObject);
		}
	}
}
