using UnityEngine;
using System.Collections;

public class GameHUD : MonoBehaviour {
	void OnGUI () {
		// Make a background box
		GUI.Box(new Rect(10,10,120,400), "Loader Menu");
		
		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if(GUI.Button(new Rect(20,40,100,100), "Red")) {
			GameObject dasher = GameObject.FindGameObjectWithTag("Player");
			if(dasher) {
				ColorChangerScript colorChanger = dasher.GetComponent<ColorChangerScript>();
				if(colorChanger) colorChanger.colorNew = 0;
			}
		}

		if(GUI.Button(new Rect(20,160,100,100), "Green")) {
			GameObject dasher = GameObject.FindGameObjectWithTag("Player");
			if(dasher) {
				ColorChangerScript colorChanger = dasher.GetComponent<ColorChangerScript>();
				if(colorChanger) colorChanger.colorNew = 1;
			}
		}

		if(GUI.Button(new Rect(20,280,100,100), "Blue")) {
			GameObject dasher = GameObject.FindGameObjectWithTag("Player");
			if(dasher) {
				ColorChangerScript colorChanger = dasher.GetComponent<ColorChangerScript>();
				if(colorChanger) colorChanger.colorNew = 2;
			}
		}
	}
}
