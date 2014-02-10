using UnityEngine;
using System.Collections;

public class KillFire : MonoBehaviour {

	float startTime;
	LayerMask origin;
	float originGrav;
	bool canUse = false;

	public GameObject scoreText;
	
	void Start () {
		startTime = Time.time;
		origin = this.gameObject.layer;

		//Debug.Log("meow");
		this.gameObject.layer = LayerMask.NameToLayer("Death");
		foreach (Transform child in this.transform)
		{
			child.gameObject.layer = LayerMask.NameToLayer("Death");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - startTime > 1.0f)
		{
			this.gameObject.layer = origin;
			foreach (Transform child in this.transform)
			{
				child.gameObject.layer = origin;
			}
			canUse = true;
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && canUse)
		{
			other.SendMessage("fireFlower");
			GuiValues.points+=1000;
			GameObject temp = Instantiate(scoreText,this.transform.position,this.transform.rotation) as GameObject;
			temp.GetComponent<TextMesh>().text = "1000";
			Destroy(this.gameObject);
		}
	}
}
