using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour {
	
	public Transform gate;
	public Transform tile;
	public float gateDelay = 4f;
	public float tileDelay = 1f;
	private float rightBorder;

	void Start () {
		StartCoroutine(generateGate());
		StartCoroutine(generateTerrain());
	}

	IEnumerator generateTerrain() {
		for( float timer = tileDelay; timer >= 0; timer -= Time.deltaTime)
			yield return 0;
		
		Vector3 position = Camera.main.transform.position;
		position.x = rightBorder;
		position.y = -2.12f;
		position.z = 1;
		
		Transform mTile = Instantiate(tile, position, Camera.main.transform.rotation) as Transform;
		mTile.parent = transform;
		
		StartCoroutine(generateTerrain());
	}

	IEnumerator generateGate() {
		for( float timer = gateDelay; timer >= 0; timer -= Time.deltaTime)
			yield return 0;

		Vector3 position = Camera.main.transform.position;
		position.x = rightBorder;
		position.y = .95f;
		position.z = 1;
		
		Transform mGate = Instantiate(gate, position, Camera.main.transform.rotation) as Transform;
		mGate.parent = transform;
		
		StartCoroutine(generateGate());
	}

	void Update () {
		rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, 0)
			).x;
	}
}
