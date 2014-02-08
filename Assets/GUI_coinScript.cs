using UnityEngine;
using System.Collections;

public class GUI_coinScript : MonoBehaviour {
		
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			GetComponent<TextMesh> ().text = GuiValues.coins.ToString ("D2");
			if(GuiValues.coins>=100){
				GuiValues.numLives += GuiValues.coins/100;
				GuiValues.coins = GuiValues.coins%100;
			}
		}
	}
