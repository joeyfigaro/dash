using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour {

	public GameObject gate;
	public GameObject tile;
	public GameObject background;
	public GameObject terrain;

	public float terrainDistanceFromCamera = 50f;
	public float backgroundDistanceFromCamera = 130f;

	public float timeBetweenDoodads = 2f;
	public float startTerrainY = 0f;
	public float terrainGenerationSpacer = 1f;
	public float terrainGenerationOverdraw = 1f;
	public float terrainAngleMax = 30f;
	public float terrainDeviationMax = 3f;

	private int doodadIndex = 0;
	private float rightBorder;
	private float leftBorder;
	private float rightBorderBackground;
	private float leftBorderBackground;
	private float terrainX;
	private float terrainY;
	private float lastTerrainAngle = 0;

	private GateScript gateScript;

	void Start () {
		calculateBorders();

		terrainX = leftBorder;
		terrainY = startTerrainY;

		gateScript = gate.GetComponent<GateScript>();

		generateTerrain();

		StartCoroutine(generateDoodad());
	}

	private void generateTerrain() {
		Vector3 position;
		Quaternion rot;
		GameObject tileClone;

		for( float tempTerrain = terrainX; tempTerrain <= rightBorder + terrainGenerationOverdraw; tempTerrain += terrainGenerationSpacer) {
			float angleChange = Random.Range (-1, 2);
			if(Mathf.Abs (lastTerrainAngle + angleChange) >= terrainAngleMax) angleChange = 0;

			float terrainChange = terrainGenerationSpacer * Mathf.Tan((lastTerrainAngle + angleChange) * Mathf.Deg2Rad);
//			if(Mathf.Abs (terrainY + terrainChange) >= (startTerrainY + terrainDeviationMax)) {
//				if(terrainY > 0) angleChange = -1;
//				else angleChange = 1;
//				terrainChange = terrainGenerationSpacer * Mathf.Tan((lastTerrainAngle + angleChange) * Mathf.Deg2Rad);
//			}

			lastTerrainAngle += angleChange;
			terrainY += terrainChange;

			rot = Quaternion.Euler(0, 0, lastTerrainAngle);
			position = new Vector3(tempTerrain, terrainY, terrainDistanceFromCamera);

			tileClone = Instantiate(tile, position, rot) as GameObject;
			tileClone.transform.parent = terrain.transform;

			terrainX = tempTerrain;
		}
	}

	public void generateGate(int color) {
		gateScript.color = color;
		GameObject gateClone = Instantiate(gate, new Vector3(
				rightBorder,
				terrainY + ((tile.renderer.bounds.size.y + gate.renderer.bounds.size.y) / 2f),
				terrainDistanceFromCamera
			), Quaternion.Euler(0, 0, lastTerrainAngle)) as GameObject;
		gateClone.transform.parent = terrain.transform;
	}
	
	IEnumerator generateDoodad() {
		for( float timer = timeBetweenDoodads; timer >= 0; timer -= Time.deltaTime)
			yield return 0;

		doodadIndex += Random.Range (1, Doodads.Instance.doodads.Length - 1);
		if(doodadIndex >= Doodads.Instance.doodads.Length) doodadIndex -= Doodads.Instance.doodads.Length - 1;
		
		float z = Doodads.Instance.getRandomOffset(doodadIndex);
		GameObject doodad = Doodads.Instance.doodads[doodadIndex];
		GameObject doodadClone = Instantiate(doodad, new Vector3(
				calculateRightBorderAtZ(z) + doodad.renderer.bounds.size.x, 
				doodad.renderer.bounds.size.y / 2f,//terrainY + ((tile.renderer.bounds.size.y + doodad.renderer.bounds.size.y) / 2f) + Doodads.Instance.getRandomOffset(doodadIndex),
				z
			), doodad.transform.rotation) as GameObject;
		doodadClone.transform.parent = background.transform;

		StartCoroutine(generateDoodad());
	}

	private float calculateRightBorderAtZ(float z) {
		return Camera.main.ViewportToWorldPoint(
				new Vector3(1, 0, z)
			).x;
	}

	private void calculateBorders() {
		leftBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(-1, 0, terrainDistanceFromCamera)
			).x;
		
		rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, terrainDistanceFromCamera)
			).x;
	}

	void Update() {
		calculateBorders();
		generateTerrain();
	}
}
