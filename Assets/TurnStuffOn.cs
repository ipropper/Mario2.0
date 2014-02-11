using UnityEngine;
using System.Collections;

public class TurnStuffOn : MonoBehaviour {

	// Use this for initialization
	public static bool On = false;
	bool triggered = false;
	public AudioSource sound;

	public GameObject textA1;
	public GameObject textA2;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter2D(Collider2D other) 
	{
		if(other.tag == "Player" && !triggered)
		{
			triggered = true;
			StartCoroutine("ending");


		}
	}

	IEnumerator ending(){
		textA1.GetComponent<TextMesh> ().text = "One second";
		yield return new WaitForSeconds (.5f);
		textA2.GetComponent<TextMesh> ().text = "I'll be right out";

		sound.Play();
		Camera.main.GetComponent<AudioSource>().mute = true;

		yield return new WaitForSeconds (4);
		textA1.GetComponent<TextMesh> ().text = "";
		textA2.GetComponent<TextMesh> ().text = "";

		On = true;

		yield return new WaitForSeconds (.25f);
		textA2.GetComponent<TextMesh> ().text = "Would you like cake???";

		yield return new WaitForSeconds(5);
		GuiValues.goToMenu ();
	}
}
