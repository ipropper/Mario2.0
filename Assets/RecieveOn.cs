using UnityEngine;
using System.Collections;

public class RecieveOn : MonoBehaviour {

	// Use this for initialization
	public GameObject Question;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(TurnStuffOn.On == true)
		{
			StartCoroutine("Suprise");

		}
	}
	IEnumerator Suprise() 
	{
		yield return new WaitForSeconds(1.5f);
		Instantiate(Question,transform.position,transform.rotation);
		Destroy(this.gameObject);
	}
}
