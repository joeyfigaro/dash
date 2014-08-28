using UnityEngine;
using System.Collections;

public class Doodad : ColorObject {
	public float possibleUnderhang;
	public float possibleOverhang;
	public int rarity = 10;
	public float minScale = 0;
	public float maxScale = 0;

	public bool foreground = false;
	public bool sky = false;
	public bool force = false;
	public bool underground = false;

	public Vector3 size;
	
	private float scale;

	void Start () {
		if(!avoidRegistration) registerWithTintSource();
		initialize();
	}

	public void initialize() {
		if(!avoidRegistration) setSize(Vector3.Scale(((SpriteRenderer) renderer).sprite.bounds.size, transform.localScale));
	}
	
	public void setSize(Vector3 newSize) {
		size = newSize;
	}

	void Update() {
		if(!avoidRegistration) changeRenderedColor();
		destroyIfOffscreen();
	}

	public Vector3 getSizeAtScale(float scale) {
		float sizeX = Mathf.Round((size * scale).x);
  		float sizeY = Mathf.Round((size * scale).y);
		return new Vector3(sizeX, sizeY, 0);
	}
	
	public float getRandomScale() {
		return Random.Range(minScale, maxScale);
	}

	public GameObject instantiateAt(Vector3 pos, float scale) {
		// TODO: This is bad
		if(foreground) pos += new Vector3(0, -1, -2);
		if(sky) pos += new Vector3(0, Random.Range(25, 120), 0);

		GameObject doodadClone = Instantiate(gameObject, pos, transform.rotation) as GameObject;

		doodadClone.transform.rotation = gameObject.transform.rotation;
		doodadClone.transform.localScale = new Vector3(scale, scale, scale);
		doodadClone.transform.position += new Vector3(size.x * scale, 0, 0);

		return doodadClone;
	}
}
