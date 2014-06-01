using UnityEngine;
using System.Collections;

public class GateScript : ColorObject {
	void Start() {
		updateColor();
	}

	void Update () {
		destroyIfOffscreen();
		updateColor();
	}
	
	private void updateColor() {
		gameObject.renderer.material.color = realColor();
		transform.GetChild(0).renderer.material.color = realColor();
	}
}
