using UnityEngine;
using System.Collections;

public class GateScript : ColorObject {
	void Start() {
		BeatsEngine.Instance.registerTintable(this);
		updateColor();
	}

	void Update () {
		destroyIfOffscreen();
		updateColor();
	}
	
	private void updateColor() {
		changeRenderedColor();
		transform.GetChild(0).renderer.material.color = getColor();
	}
}
