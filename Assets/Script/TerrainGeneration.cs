using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour {
	
	public GameObject gate;
	public GameObject tile;
	public float timeBetweenGates = 4f;
	public float startTerrainY = 0f;
	public float terrainGenerationSpacer = 1f;
	public float terrainGenerationOverdraw = 1f;
	public float terrainAngleMax = 30f;
	public float terrainDeviationMax = 3f;

	private float rightBorder;
	private float leftBorder;
	private float terrainX;
	private float terrainY;
	private float lastTerrainAngle = 0;
	private bool makeGate = false;

	void Start () {
		calculateBorders();

		terrainX = leftBorder;
		terrainY = startTerrainY;

		generateTerrain();
	}

	public void startGateGeneration() {
		StartCoroutine(generateGate());
	}

	private void generateTerrain() {
		Vector3 position;
		Quaternion rot;
		GameObject tileClone;
		GameObject gateClone;

		for( float tempTerrain = terrainX; tempTerrain <= rightBorder + terrainGenerationOverdraw; tempTerrain += terrainGenerationSpacer) {
			float angleChange = Random.Range (-1, 2);
			if(Mathf.Abs (lastTerrainAngle + angleChange) >= terrainAngleMax) angleChange = 0;

			float terrainChange = terrainGenerationSpacer * Mathf.Tan((lastTerrainAngle + angleChange) * Mathf.Deg2Rad);
			if(Mathf.Abs (terrainY + terrainChange) >= terrainDeviationMax) {
				if(terrainY > 0) angleChange = -1;
				else angleChange = 1;
				terrainChange = terrainGenerationSpacer * Mathf.Tan((lastTerrainAngle + angleChange) * Mathf.Deg2Rad);
			}

			lastTerrainAngle += angleChange;
			terrainY += terrainChange;

			rot = Quaternion.Euler(0, 0, lastTerrainAngle);
			position = new Vector3(tempTerrain, terrainY, 0f);

			if(makeGate) {
				gateClone = Instantiate(gate, new Vector3(rightBorder, terrainY + (gate.renderer.bounds.size.y / 2), 0), rot) as GameObject;
				gateClone.transform.parent = transform;
				makeGate = false;
			}

			tileClone = Instantiate(tile, position, rot) as GameObject;
			tileClone.transform.parent = transform;

			terrainX = tempTerrain;
		}
	}

	IEnumerator generateGate() {
		for( float timer = timeBetweenGates; timer >= 0; timer -= Time.deltaTime)
			yield return 0;

		makeGate = true;
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

	void Update() {
		calculateBorders();
		generateTerrain();
	}
}
