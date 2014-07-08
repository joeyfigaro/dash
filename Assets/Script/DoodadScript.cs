using UnityEngine;
using System.Collections;

public class DoodadScript : ColorObject {
	public float possibleUnderhang;
	public float possibleOverhang;
	public int rarity = 10;
	public float minScale = 0;
	public float maxScale = 0;

	public bool foreground = false;
	public bool sky = false;
	public bool ground = false;
	public bool force = false;

	void Start () {
		registerWithTintSource();
		gameObject.renderer.castShadows = true;
		gameObject.renderer.receiveShadows = true;
	}

	void Update() {
		changeRenderedColor();
		destroyIfOffscreen();
	}
}
