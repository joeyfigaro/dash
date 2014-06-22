using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGeneration : MonoBehaviour {

	public GameObject gate;
	public GameObject tile;
	public GameObject background;
	public GameObject terrain;
	public GameObject ground;
	public GameObject groundPlaceholder;

	public float timeBetweenDoodads = 2f;

	private int doodadIndex = 0;
	private float rightBorder;
//	private float leftBorder;
	private float rightBorderBackground;
	private float leftBorderBackground;
	private float terrainY;

	private float fieldDepth;
	private float fieldZMax;
	private float fieldStart;

	private float lastGroundX;
	private float lastTileX;

	private bool[,] terrainTrack;
	private int terrainTrackOffset;

	int groundTilesHeight;

	void Start () {
		fieldDepth = groundPlaceholder.renderer.bounds.size.z;
		fieldStart = groundPlaceholder.transform.position.z - (groundPlaceholder.renderer.bounds.size.z / 2);
		fieldZMax = groundPlaceholder.transform.position.z + (groundPlaceholder.renderer.bounds.size.z / 2);
		terrainY = groundPlaceholder.transform.position.y;
		Destroy(groundPlaceholder);

		calculateBorders();

		lastGroundX = leftBorderBackground - ground.renderer.bounds.size.x;
		lastTileX = leftBorderBackground - tile.renderer.bounds.size.x;

		groundTilesHeight = Mathf.FloorToInt(fieldDepth / ground.renderer.bounds.size.y - 1);
		terrainTrack = new bool[5, groundTilesHeight];

		generateTerrainLoop();
	}

	private void generateTerrainLoop() {
		while(lastGroundX <= rightBorderBackground) {

			Vector3 position = new Vector3(lastGroundX, terrainY, fieldStart + (ground.renderer.bounds.size.y / 2f));
			for(int groundTile = 0; groundTile < groundTilesHeight; groundTile++) {
				generateGround(position);
				generateDoodad(groundTile, ground.renderer.bounds.size.y * (groundTile + 1));

				position.z += ground.renderer.bounds.size.y;
			}
			generateTile();

			terrainTrackOffset %= terrainTrackOffset + 1;
			lastGroundX += ground.renderer.bounds.size.x;
		}
	}

	private void generateGround(Vector3 position) {
		GameObject groundClone = Instantiate(ground, position, Quaternion.Euler(-90, 0, 0)) as GameObject;
		groundClone.transform.parent = terrain.transform;
	}

	private void generateTile() {
		Vector3 position = new Vector3(lastTileX, terrainY - (tile.renderer.bounds.size.y / 2f), fieldStart);
		GameObject tileClone = Instantiate(tile, position, Quaternion.Euler(0, 0, 0)) as GameObject;
		tileClone.transform.parent = terrain.transform;
		lastTileX += tile.renderer.bounds.size.x;
	}

	public void generateGate() {
		GameObject gateClone = Instantiate(gate, new Vector3(
			rightBorder + (2 * gate.renderer.bounds.size.x),
			terrainY + (gate.renderer.bounds.size.y / 2f),
			fieldStart
		), Quaternion.Euler(0, 0, 0)) as GameObject;
		gateClone.transform.parent = terrain.transform;
	}
	
	private void generateDoodad(int groundTile, float z) {
		int availableHeight = 1;
		while((groundTile + availableHeight < groundTilesHeight) && !terrainTrack[terrainTrackOffset, groundTile + availableHeight]) availableHeight++;

		DoodadScript doodadScript = Doodads.Instance.randomDoodadScript();
		if(doodadScript != null) {
			float yOffset = 0;

			if(doodadScript.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.center.y != 0) yOffset = 0;
			if(doodadScript.sky) yOffset += 15;
			if(doodadScript.foreground) z = 0;

			GameObject doodadClone = Instantiate(doodadScript.gameObject, new Vector3(
				lastTileX + doodadScript.gameObject.renderer.bounds.size.x, 
				terrainY + yOffset,
				z
				), doodadScript.gameObject.transform.rotation) as GameObject;

//			doodadClone.transform.localScale *= 5;
			doodadClone.transform.parent = background.transform;
			if(doodadScript.gameObject.GetComponent<SpriteRenderer>().sprite.bounds.center.y == 0) doodadClone.transform.position += new Vector3(0, doodadScript.gameObject.renderer.bounds.size.y / 2f, 0);

			Vector2 size = new Vector2(5, availableHeight);
			for(int width = 0; width < 5; width++) {
				for(int height = 0; height < size.y; height++) {
					terrainTrack[(terrainTrackOffset + width) % 5, groundTile + height] = width < size.y;
				}
			}

		}
	}

	private void calculateBorders() {
		rightBorder = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, fieldStart)
			).x;
		leftBorderBackground = Camera.main.ViewportToWorldPoint(
			new Vector3(0, 0, fieldZMax)
			).x;
		rightBorderBackground = Camera.main.ViewportToWorldPoint(
			new Vector3(1, 0, fieldZMax)
			).x;
	}

	void Update() {
		calculateBorders();
	}

	void FixedUpdate() {
		generateTerrainLoop();
	}
}
