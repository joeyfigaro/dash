using UnityEngine;
using System.Collections;

public class ColorObject : MonoBehaviour {
	public int color = 0;

	public Color realColor() {
		return ColorDefinitions.colors[color];
	}

	protected void changeColor() {
		gameObject.renderer.material.color = realColor();
	}

	public void registerWithBeatsEngine() {
		BeatsEngine.Instance.registerTintable(this);
	}

	void OnDestroy() {
		BeatsEngine.Instance.unregisterTintable(this);
	}

	protected void destroyIfOffscreen() {
		if ((transform.position.x < (Camera.main.transform.position.x - 50)) && (renderer.IsVisibleFrom(Camera.main) == false))
		{
			Destroy(gameObject);
		}
	}
}
