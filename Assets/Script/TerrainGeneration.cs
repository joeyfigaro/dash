using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour {
	
	public GameObject gate;
	public GameObject tile;
	public float gateDelay = 4f;
	public float tileDelay = 1f;
	private float rightBorder;
	private float leftBorder;

	void Start () {
		StartCoroutine(generateGate());
		StartCoroutine(generateTerrain());

		calculateBorders();

		for( float x = leftBorder; x <= rightBorder; x += .1f) {
			Vector3 position = new Vector3(x, -2.12f, 1f);
			GameObject tileClone = Instantiate(tile, position, Camera.main.transform.rotation) as GameObject;
			GameObject foreground = GameObject.Find("Terrain");
			tileClone.transform.parent = foreground.transform;
		}
	}

	IEnumerator generateTerrain() {
		for( float timer = tileDelay; timer >= 0; timer -= Time.deltaTime)
			yield return 0;
		
		Vector3 position = Camera.main.transform.position;
		position.x = rightBorder;
		position.y = -2.12f;
		position.z = 1;
		
		GameObject tileClone = Instantiate(tile, position, Camera.main.transform.rotation) as GameObject;
		GameObject foreground = GameObject.Find("Terrain");
		tileClone.transform.parent = foreground.transform;

		StartCoroutine(generateTerrain());
	}

	IEnumerator generateGate() {
		for( float timer = gateDelay; timer >= 0; timer -= Time.deltaTime)
			yield return 0;

		Vector3 position = Camera.main.transform.position;
		position.x = rightBorder;
		position.y = -.59f;
		position.z = 1;

		Quaternion rot = Quaternion.Euler(0, 0, 0);
		if(Random.Range(0, 5) == 4) {
			position.y = -1.2f;
			rot = Quaternion.Euler(0, 0, -55);
		}

		GameObject gateClone = Instantiate(gate, position, rot) as GameObject;
		GameObject foreground = GameObject.Find("Terrain");
		gateClone.transform.parent = foreground.transform;

		StartCoroutine(generateGate());
	}

	private void calculateBorders() {
		leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(-1, 0, 0)
			).x;
		
		rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, 0)
			).x;
	}

	void Update () {
		calculateBorders();
	}
}
