using UnityEngine;
using System.Collections;

public class GateScript : ColorObject {
	void Start() {
	}

	void Update () {
		if (renderer.IsVisibleFrom(Camera.main) == false)
		{
			Destroy(gameObject);
		}
		updateColor();
	}
	
	private void updateColor() {
		gameObject.renderer.material.color = realColor();
		transform.GetChild(0).renderer.material.color = realColor();
	}
}
