﻿using UnityEngine;
using System.Collections;

public class ColorObject : MonoBehaviour {
	private Color color = new Color();

	public void setColor(int colorInt) {
		color = ColorDefinitions.colors[colorInt];
	}

	public void setColor(Color colorV) {
		color = colorV;
	}

	public Color getColor() {
		return color;
	}

	protected void changeRenderedColor() {
		gameObject.renderer.material.SetColor("_SunTint", color);
	}

	public void registerWithTintSource() {
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
