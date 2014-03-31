using UnityEngine;
using System.Collections;

public class GameHUD : MonoBehaviour {
	void OnGUI () {

		float height = Screen.currentResolution.height;
		float buttonSize = height / 8;

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(10,0,buttonSize,buttonSize), "Red")) {
			GameObject dasher = GameObject.FindGameObjectWithTag("Player");
			if(dasher) {
				ColorChangerScript colorChanger = dasher.GetComponent<ColorChangerScript>();
				if(colorChanger) colorChanger.colorNew = 0;
			}
		}

		if(GUI.Button(new Rect(10,buttonSize,buttonSize,buttonSize), "Green")) {
			GameObject dasher = GameObject.FindGameObjectWithTag("Player");
			if(dasher) {
				ColorChangerScript colorChanger = dasher.GetComponent<ColorChangerScript>();
				if(colorChanger) colorChanger.colorNew = 1;
			}
		}

		if(GUI.Button(new Rect(10,2 * buttonSize,buttonSize,buttonSize), "Blue")) {
			GameObject dasher = GameObject.FindGameObjectWithTag("Player");
			if(dasher) {
				ColorChangerScript colorChanger = dasher.GetComponent<ColorChangerScript>();
				if(colorChanger) colorChanger.colorNew = 2;
			}
		}
	}
}
