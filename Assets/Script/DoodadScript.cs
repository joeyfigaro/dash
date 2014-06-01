using UnityEngine;
using System.Collections;

public class DoodadScript : ColorObject {
	public float possibleUnderhang;
	public float possibleOverhang;
	public int rarity = 10;

	public bool foreground = false;

	void Start () {
		registerWithBeatsEngine();
	}

	void Update() {
		changeColor();
		destroyIfOffscreen();
	}
}
