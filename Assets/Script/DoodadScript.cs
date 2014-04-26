using UnityEngine;
using System.Collections;

public class DoodadScript : MonoBehaviour {
	public Sprite[] doodads;

	void Start () {
		int sprite = Random.Range(0, doodads.Length);
		SpriteRenderer sprRenderer = (SpriteRenderer) renderer;
		sprRenderer.sprite = doodads[sprite];
	}
}
